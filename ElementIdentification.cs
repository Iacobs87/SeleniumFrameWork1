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


namespace ElementInteraction
{
    [TestClass]
    [TestCategory("Identification And Manipulation")]
    public class ElementIdentification
    {
        static IWebDriver driver;
        private IWebElement element;
        private By locator;

        [TestInitialize]
        public void TestSetup()
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            driver = new ChromeDriver(outPutDirectory);
            
        }
        [TestMethod]
        [TestCategory("Navigation")]
        public void SeleniumNavigationTest()
        {
            //Go here and assert for title - "https://www.ultimateqa.com"
            driver.Navigate().GoToUrl("https://www.ultimateqa.com");
            Assert.AreEqual("Home - Ultimate QA", driver.Title);

            //Go here and assert for title - "https://www.ultimateqa.com/automation"
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/automation");
            Assert.AreEqual("Automation Practice - Ultimate QA", driver.Title);

            //Click link with href - /complicated-page
            driver.FindElement(By.XPath("//*[@href='../complicated-page']")).Click();

            //assert page title 'Complicated Page - Ultimate QA'
            Assert.AreEqual("Complicated Page - Ultimate QA", driver.Title);

            //Go back
            driver.Navigate().Back();

            //assert page title equals - 'Automation Practice - Ultimate QA'
            Assert.AreEqual("Automation Practice - Ultimate QA", driver.Title);
        }

        [TestMethod]
        [TestCategory("Manipulation")]

        public void Manipulation()
        {
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/filling-out-forms/");

            //find the name field
            var nameField = driver.FindElement(By.Id("et_pb_contact_name_0"));
            nameField.Clear();
            nameField.SendKeys("test");
            //clear the field
            //type into the field

            //find the text field
            var textBox = driver.FindElement(By.Id("et_pb_contact_message_0"));
            //clear the field
            textBox.Clear();
            //type into the field
            textBox.SendKeys("testing");
            //submit
            var submitButton = driver.FindElements(By.ClassName("et_contact_bottom_container"));
            submitButton[0].Submit();
            Thread.Sleep(2000);

            var formMessage = driver.FindElement(By.ClassName("et-pb-contact-message"));
            Assert.AreEqual(formMessage.Text, "Form filled out successfully");
        }

        [TestMethod]
        [TestCategory("Manipulation")]
        public void ManipulationTest()
        {
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/filling-out-forms/");
            var nameField2 = driver.FindElement(By.Id("et_pb_contact_name_1"));
            nameField2.Clear();
            nameField2.SendKeys("Andreea");

            var textBox2 = driver.FindElement(By.Id("et_pb_contact_message_1"));
            textBox2.Clear();
            textBox2.SendKeys("E cea mai frumoasa femeie din univers");

            var captcha = driver.FindElement(By.ClassName("et_pb_contact_captcha_question"));

            string temp = captcha.Text;
            string captchaSum = temp.Replace(" ", "");
            Int32 sum = captchaSum.Split(new char[] { '+' }).Select(n => Int32.Parse(n)).Sum();

            var captchaTextBox = driver.FindElement(By.XPath("//*[@class='input et_pb_contact_captcha']"));
            captchaTextBox.SendKeys(sum.ToString());

            var submitButton = driver.FindElements(By.ClassName("et_contact_bottom_container"));
            submitButton[1].Submit();
            Thread.Sleep(2000);
          
            var formMessage = driver.FindElement(By.XPath("//p[contains(text(), 'Success')]"));
            Assert.AreEqual(formMessage.Text, "Success");
        }

        [TestMethod]
        [TestCategory("Driver Interogation")]
        public void DriverLevelInterogation()
        {
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/automation");
            var x = driver.CurrentWindowHandle;
            var y = driver.WindowHandles;
            x = driver.PageSource;
            x = driver.Title;
            x = driver.Url;
        }

        [TestMethod]
        [TestCategory("Element Interrogation")]
        public void ElementInterogation()
        {
            driver.Navigate().GoToUrl("https://www.ultimateqa.com/automation");
            var myElement = driver.FindElement(By.XPath("//*[@href='http://courses.ultimateqa.com/users/sign_in']"));
        }

        [TestMethod]
        [TestCategory("Element Interrogation")]

        public void ElementInterogationTest()
        {
            driver.Url = "https://www.ultimateqa.com/simple-html-elements-for-automation/";
            driver.Manage().Window.Maximize();
            //1. find button by Id
            //2. GetAttribute("type") and assert that it equals the right value
            //3. GetCssValue("letter-spacing") and assert that it equals the correct value
            //4. Assert that it's Displayed
            //5. Assert that it's Enabled
            //6. Assert that it's NOT selected
            //7. Assert that the Text is correct
            //8. Assert that the TagName is correct
            //9. Assert that the size height is 21
            //10. Assert that the location is x=190, y=330

            var myElement = driver.FindElement(By.Id("button1"));
            Assert.AreEqual("submit", myElement.GetAttribute("type"));
            Assert.AreEqual("normal", myElement.GetCssValue("letter-spacing"));
            Assert.IsTrue(myElement.Displayed);
            Assert.IsTrue(myElement.Enabled);
            Assert.IsFalse(myElement.Selected);
            Assert.AreEqual(myElement.Text, "Click Me!");
            Assert.AreEqual("button", myElement.TagName);
            Assert.AreEqual(24, myElement.Size.Height);
           // Assert.AreEqual(341, myElement.Location.X);
           // Assert.AreEqual(214, myElement.Location.Y);
    
        }

        





        [TestCleanup]
        public void CleanUp()
        {
            Thread.Sleep(4000);
            driver.Close();
            driver.Quit();
        }


    }
}
