﻿using System;
using CoApp.Toolkit.Extensions;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace CoApp.VisualStudio.VsCore
{
    public static class MessageHelper
    {
        public static void ShowWarningMessage(string message, string title)
        {
            VsShellUtilities.ShowMessageBox(
                ServiceLocator.GetInstance<IServiceProvider>(),
                message,
                title,
                OLEMSGICON.OLEMSGICON_WARNING,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }

        public static void ShowInfoMessage(string message, string title)
        {
            VsShellUtilities.ShowMessageBox(
                ServiceLocator.GetInstance<IServiceProvider>(),
                message,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }

        public static void ShowErrorMessage(Exception exception, string title)
        {
            ShowErrorMessage(exception.Unwrap().Message, title);
        }

        public static void ShowErrorMessage(string message, string title)
        {
            VsShellUtilities.ShowMessageBox(
                ServiceLocator.GetInstance<IServiceProvider>(),
                message,
                title,
                OLEMSGICON.OLEMSGICON_CRITICAL,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }

        public static bool? ShowQueryMessage(string message, string title, bool showCancelButton)
        {
            int result = VsShellUtilities.ShowMessageBox(
                ServiceLocator.GetInstance<IServiceProvider>(),
                message,
                title,
                OLEMSGICON.OLEMSGICON_QUERY,
                showCancelButton ? OLEMSGBUTTON.OLEMSGBUTTON_YESNOCANCEL : OLEMSGBUTTON.OLEMSGBUTTON_YESNO,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

            if (result == NativeMethods.IDCANCEL)
            {
                return null;
            }

            return (result == NativeMethods.IDYES);
        }
    }

    internal class NativeMethods
    {
        public const int IDCANCEL = 2;
        public const int IDYES = 6;
        public const int IDNO = 7;
    }
}