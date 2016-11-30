using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleApplication1.Extensions;

namespace ConsoleApplication1.TestCases
{
    class TTBO
    {

        public static IWebDriver driver = new ChromeDriver();
        public static WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        public const string ttBOAccount = "lindatestuat";
        public const string ttBOPassword = "pass123";
        public const string ttMember = "lindatestrmb8";


        #region (1) Log in TTBO
        public static void TTBOLogin()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(@"http://bo.ttuat.com/en-gb/Home");
            driver.FindElement(By.Id("UserName"));
            driver.FindElement(By.Id("UserName")).SendKeys(ttBOAccount);
            driver.FindElement(By.Id("Password"));
            driver.FindElement(By.Id("Password")).SendKeys(ttBOPassword);
            driver.FindElement(By.XPath("/html/body/div[3]/form/div[1]/div[4]/input")).Click();

        }
        #endregion

        #region (2) Find Member
        public static void FindMember()
        {

            driver.FindElement(By.LinkText("Member Listing")).Click();
            Thread.Sleep(2000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='MainFrame']")));
            Thread.Sleep(2000);
            driver.FindElement(By.Id("LoginName"));
            driver.FindElement(By.Id("LoginName")).SendKeys(ttMember);
            Thread.Sleep(2000);
            driver.FindElement(By.Id("searchBtn")).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.LinkText(ttMember)).Click();
            Thread.Sleep(5000);
        }
        #endregion

        #region (3) Find Wager
        public static void FindWager(string BetNo)
        {
            DateTime eventDayFrom = DateTime.Now.AddDays(-2);

            driver.FindElement(By.LinkText("SBK Wager Enquiry")).Click();
            Thread.Sleep(2000);

            // Select event start time in calendar
            //ElementVerify.IsElementExists(driver, By.XPath("//*[@id='someMember']"));
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='MainFrame']")));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='someMember']")));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='imageUploadFrame']")));
            ElementVerify.IsElementExists(driver, By.Id("txtDateFrom"));

            //Click on calendar icon
            driver.FindElement(By.Id("txtDateFrom")).ClickOnIt("Event Date From");

            //Find the calendar
            ElementVerify.IsElementExists(driver, By.Id("ui-datepicker-div"));
            ICollection<IWebElement> columns = driver.FindElement(By.Id("ui-datepicker-div")).FindElements(By.TagName("td"));

            //Click on the number if cell = today's date-2
            foreach (IWebElement cell in columns)
            {
                if (cell.Text.Equals(eventDayFrom.ToString("dd")))
                {
                    cell.ClickOnIt("date");
                    break;
                }
            }

            //Wager No Textbox
            ElementVerify.IsElementExists(driver, By.Id("txtWagerNo"));

            #region (4) Fetch All Bet Number from TouTou

            char[] delimiterChars = { ',' };
            BetNo = BetNo.Remove(BetNo.Length - 1);        // Reomove comma
            string[] wagers = BetNo.Split(delimiterChars);
            foreach (string wager in wagers)
            {
                Thread.Sleep(2000);
                driver.FindElement(By.Id("txtWagerNo")).EnterText("Wager No.", wager);
                driver.FindElement(By.Id("btnSearch")).ClickOnIt("Search");
                ElementVerify.IsElementExists(driver, By.ClassName("acct-no"));
                if (wager == driver.FindElement(By.ClassName("acct-no")).Text)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Wager Verified");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unable to find the wager");
                    Console.ResetColor();
                }
            }

            #endregion





        }
        #endregion
    }
}
