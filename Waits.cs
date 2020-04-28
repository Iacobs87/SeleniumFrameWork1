using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace ImplicitExplicitWaits
{
    [TestClass]
    public class ImplicitExplicitWait
    {
        static IWebDriver driver;

        [TestInitialize]
        public void TestSetup()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            driver = new ChromeDriver(outPutDirectory);

        }

        [TestCleanup]
        public void CleanUp()
        {
            Thread.Sleep(4000);
            driver.Close();
            driver.Quit();
        }


        [TestMethod]
        [TestCategory("ExplicitWaits")]
        public void HowToCorectlySync()
        {
            driver.Navigate().GoToUrl("https://ultimateqa.com/");
            driver.Manage().Window.Maximize();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            //var firstSyncElement = By.XPath("//*[@id='et-builder-module-design-58-cached-inline-styles']");
            // wait.Until(ExpectedConditions.ElementIsVisible(firstSyncElement));
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//*[@id='menu-item-587']/a")).Click();

            driver.FindElement(By.LinkText("Big page with many elements")).Click();



        }
    }
}
