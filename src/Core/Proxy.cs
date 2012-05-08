﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using CoApp.Toolkit.Engine.Client;
using CoApp.Toolkit.Exceptions;
using CoApp.Toolkit.Extensions;
using CoApp.Toolkit.Win32;
using System.Security.Principal;

namespace CoGet
{
    public static class ProgressManager
    {

        private static readonly IProgressProvider _progressProvider = new ProgressProvider();

        public static IProgressProvider ProgressProvider
        {
            get
            {
                return _progressProvider;
            }
        }

        public static void UpdateProgress(this string message, long progress)
        {
            ProgressProvider.OnProgressAvailable(message, (int)progress);
        }
    }

    public class Proxy
    {
        private static FourPartVersion? _minVersion = null;
        private static FourPartVersion? _maxVersion = null;

        private static bool? _installed = null;
        private static bool? _active = null;
        private static bool? _required = null;
        private static bool? _blocked = null;
        private static bool? _latest = null;
        private static bool? _force = null;

        private static string _location = null;
        private static bool? _dependencies = null;
        private static bool? _download = null;
        private static bool? _pretend = null;
        private static bool? _autoUpgrade = null;

        private static bool? _x64 = null;
        private static bool? _x86 = null;
        private static bool? _cpuany = null;

        private static bool IsFiltering { get { return (true == _x64) || (true == _x86) || (true == _cpuany); } }

        private static readonly List<Task> preCommandTasks = new List<Task>();

        private static List<string> activeDownloads = new List<string>();

        private static IEnumerable<Package> allPackages, updateablePackages, installedPackages, subPackages;
        private static DateTime allRetrievalTime, updateableRetrievalTime, installedRetrievalTime, subRetrievalTime;

        private static CancellationTokenSource _cts;

        private readonly static EasyPackageManager _easyPackageManager = new EasyPackageManager((itemUri, localLocation, progress) =>
        {
            if (!activeDownloads.Contains(itemUri))
            {
                activeDownloads.Add(itemUri);
            }
            "Downloading {0}".format(itemUri.UrlDecode()).UpdateProgress(progress);
        }, (itemUrl, localLocation) =>
        {
            if (activeDownloads.Contains(itemUrl))
            {
                Console.WriteLine();
                activeDownloads.Remove(itemUrl);
            }
            
        });

        public static IEnumerable<Feed> ListFeeds()
        {
            Console.WriteLine("Fetching feed list...");

            IEnumerable<Feed> fds = null;

            Task task = preCommandTasks.Continue(() => _easyPackageManager.Feeds.Continue(feeds => fds = feeds));

            ContinueTask(task);

            return fds;
        }

        public static IEnumerable<Package> GetPackagesOfType(string type)
        {
            IEnumerable<Package> pkgs = null;
            switch (type)
            {
                case "all":
                    {
                        pkgs = GetAllPackages();
                        break;
                    }
                case "installed":
                    {
                        pkgs = GetInstalledPackages();
                        break;
                    }
                case "updateable":
                    {
                        pkgs = GetUpdateablePackages();
                        break;
                    }
            }
            return pkgs;
        }

        public static IEnumerable<Package> Search(string type, string searchText)
        {
            IEnumerable<Package> pkgs = null;
            switch (type)
            {
                case "all":
                    {
                        pkgs = GetAllPackages();
                        break;
                    }
                case "installed":
                    {
                        pkgs = GetInstalledPackages();
                        break;
                    }
                case "updateable":
                    {
                        pkgs = GetUpdateablePackages();
                        break;
                    }
            }
            return pkgs.AsQueryable().Find(searchText);
        }
        
        public static IEnumerable<Package> GetAllPackages()
        {
            if (allPackages == null || allPackages.IsEmpty() || DateTime.Compare(DateTime.Now, allRetrievalTime.AddSeconds(30)) > 0)
            {
                allPackages = ListPackages(new string[] { "*" });
                allRetrievalTime = DateTime.Now;
            }

            List<Package> pl = allPackages.ToList();
            for (int i = 10; i < pl.Count; i++)
            {
                pl[i] = GetDetailedPackage(pl[i]);
            }

            return allPackages;
        }

        public static IEnumerable<Package> GetPackages(string[] parameters)
        {
            if (subPackages == null || subPackages.IsEmpty() || DateTime.Compare(DateTime.Now, subRetrievalTime.AddSeconds(30)) > 0)
            {
                subPackages = ListPackages(parameters);
                subRetrievalTime = DateTime.Now;
            }

            return subPackages;
        }

        public static IEnumerable<Package> GetUpdateablePackages()
        {
            if (updateablePackages == null || updateablePackages.IsEmpty() || DateTime.Compare(DateTime.Now, updateableRetrievalTime.AddSeconds(30)) > 0)
            {
                updateablePackages = ListUpdateablePackages(new string[] { "*" });
                updateableRetrievalTime = DateTime.Now;
            }

            return updateablePackages;
        }

        public static IEnumerable<Package> GetInstalledPackages()
        {
            if (installedPackages == null || installedPackages.IsEmpty() || DateTime.Compare(DateTime.Now, installedRetrievalTime.AddSeconds(30)) > 0)
            {
                _installed = true;
                installedPackages = ListPackages(new string[] { "*" });
                installedRetrievalTime = DateTime.Now;
                _installed = null;
            }

            List<Package> pl = installedPackages.ToList();
            for (int i = 10; i < pl.Count; i++)
            {
                pl[i] = GetDetailedPackage(pl[i]);
            }

            return installedPackages;
        }

        private static IEnumerable<Package> ListUpdateablePackages(string[] parameters)
        {
            Console.WriteLine("Fetching package list...");

            IEnumerable<Package> pkgs = null;

            if (_cts.IsCancellationRequested)
                return new List<Package>();

            Task task = preCommandTasks.Continue(() => _easyPackageManager.GetUpdatablePackages(parameters)).Continue(p => pkgs = p);

            if (_cts.IsCancellationRequested)
                return new List<Package>();

            ContinueTask(task);
            return pkgs;
        }

        private static IEnumerable<Package> ListPackages(string[] parameters)
        {
            if (!parameters.Any() || parameters[0] == "*")
            {
                _latest = true;
            }

            Console.WriteLine("Fetching package list...");

            IEnumerable<Package> pkgs = null;

            if (_cts != null && _cts.IsCancellationRequested)
                return new List<Package>();

            Task task = preCommandTasks.Continue(() => _easyPackageManager.GetPackages(parameters, _minVersion, _maxVersion, false, _installed, _active, null, _blocked, _latest, _location)
                .Continue(p => pkgs = p));

            if (_cts != null && _cts.IsCancellationRequested)
                return new List<Package>();

            ContinueTask(task);

            List<Package> pl = pkgs.ToList();
            for (int i = 0; i < pl.Count && i < 10; i++)
            {
                pl[i] = GetDetailedPackage(pl[i]);
            }

            if (_cts != null && _cts.IsCancellationRequested)
                return new List<Package>();

            _latest = null;

            return pl;
        }

        public static Package GetDetailedPackage(Package package)
        {
            Task task = _easyPackageManager.GetPackageDetails(package.CanonicalName).Continue(detailedPackage =>
            {
                package = detailedPackage;
            });

            ContinueTask(task);

            return package;
        }

        public static IEnumerable<Package> GetDetailedPackages(IEnumerable<Package> packages)
        {
            IEnumerable<Package> pkgs = null;

            packages.Select(package => _easyPackageManager.GetPackageDetails(package.CanonicalName)).ToArray().Continue(detailedPackages =>
            {
                if (pkgs == null)
                    pkgs = detailedPackages;
                else
                    pkgs = pkgs.Concat(detailedPackages);
            });

            return packages;
        }

        public static void UninstallPackage(Package package, bool removeDependencies = false)
        {
            try
            {
                IEnumerable<string> pkgs = new List<string>() { package.CanonicalName };
                if (removeDependencies)
                {
                    pkgs = pkgs.Concat(package.Dependencies.Where(name => !name.Contains("coapp.toolkit")));
                }
                Task task = preCommandTasks.Continue(() => RemovePackages(pkgs));
                ContinueTask(task);
                
                installedPackages = null;
                updateablePackages = null;
                allPackages = null;
            }
            catch (Exception e)
            {
                _cts.Cancel();
            }
            
        }

        public static void InstallPackage(Package package)
        {
            bool isAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent())
                    .IsInRole(WindowsBuiltInRole.Administrator) ? true : false;

            if (isAdmin)
            {
                Console.WriteLine("you are an administrator");
            }
            else
            {
                Console.WriteLine("You are not an administrator");
            }

            try
            {
                Task task = preCommandTasks.Continue(() => InstallPackage(package.CanonicalName));
                ContinueTask(task);
                installedPackages = null;
                allPackages = null;
            }
            catch (Exception e)
            {
                _cts.Cancel();
            }

        }

        private static Task InstallPackage(string canonicalName)
        {
            Console.WriteLine("Installing...");
            return _easyPackageManager.InstallPackage(canonicalName, _force == true, (name, progress, val) => ProgressManager.UpdateProgress(name, progress));
        }

        private static Task RemovePackages(IEnumerable<string> parameters)
        {
            Console.WriteLine("Uninstalling...");
            return _easyPackageManager.RemovePackages(parameters, true, (name, progress) => ProgressManager.UpdateProgress(name, progress));
        }

        public static IEnumerable<Package> GetDependents(Package package)
        {
            return GetInstalledPackages().Where(pkg => pkg.Dependencies.Contains(package.CanonicalName));
        }


        public static void SetCancellationTokenSource(CancellationTokenSource cts)
        {
            _cts = cts;
        }

        public static int ContinueTask(Task task)
        {
            task.ContinueOnCanceled(() =>
            {
                // the task was cancelled, and presumably dealt with.
                Console.WriteLine("Operation Canceled.");
            });

            task.ContinueOnFail((exception) =>
            {
                exception = exception.Unwrap();
                if (!(exception is OperationCanceledException))
                {
                    Console.WriteLine("Error (???): {0}\r\n\r\n{1}", exception.Message, exception.StackTrace);
                }
                // it's all been handled then.
            });

            task.Continue(() =>
            {
                Console.WriteLine("Done.");
            }).Wait();

            return 0;
        }

    }
}
