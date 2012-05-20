﻿using CoGet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CoApp.Toolkit.Engine.Client;
using System.Collections.Generic;
using System.Linq;

namespace Core.Test
{
    
    
    /// <summary>
    ///This is a test class for ProxyTest and is intended
    ///to contain all ProxyTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProxyTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for InstallPackage
        ///</summary>
        [TestMethod()]
        public void InstallPackageTest()
        {
            IEnumerable<Package> packages = CoAppProxy.GetPackages(new string[] { "lua-dev[vc10]" });

            Package package = packages.First();
            CoAppProxy.InstallPackage(package);
            Assert.IsTrue(true, "pass");
        }

        /// <summary>
        ///A test for UninstallPackage
        ///</summary>
        [TestMethod()]
        public void UninstallPackageTest()
        {
            IEnumerable<Package> packages = CoAppProxy.GetPackages(new string[] { "lua-dev[vc10]" });

            Package package = packages.First();
            CoAppProxy.UninstallPackage(package);
            Assert.IsTrue(true, "pass");
        }
    }
}
