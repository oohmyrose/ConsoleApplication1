using ConsoleApplication1.PageObjects;
using ConsoleApplication1.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApplication1.TestCases
{
    class WL
    {
        public static IWebDriver driver = new ChromeDriver();
      

        public static void Main(TheEnvironment environment)
        {
            driver.Manage().Window.Maximize();
            Login(environment);
            ChooseEventToBet(EventDay.Yesterday);
            //ChooseEventToBet(EventDay.Today);
            //ChooseEventToBet(EventDay.Tomorrow);
        }



        public static void Login(TheEnvironment environment)
        {
            var wlLogin = new WLLogin(driver);

            switch (environment)
            {
                case TheEnvironment.UAT:
                    driver.Url = System.Configuration.ConfigurationManager.AppSettings["uatWLURL"];
                    ElementVerify.Exist(driver, wlLogin.UserName).EnterText("WL UserName", System.Configuration.ConfigurationManager.AppSettings["uatWLUser"]);
                    ElementVerify.Exist(driver, wlLogin.Password).EnterText("WL Password", System.Configuration.ConfigurationManager.AppSettings["uatWLPwd"]);
                    ElementVerify.Exist(driver, wlLogin.LoginButton).ClickOnIt("LoginButton");
                    break;
                case TheEnvironment.QAT:
                    driver.Url = System.Configuration.ConfigurationManager.AppSettings["qatWLURL"];
                    ElementVerify.Exist(driver, wlLogin.QATUserName).EnterText("WL UserName", System.Configuration.ConfigurationManager.AppSettings["qatWLUser"]);
                    ElementVerify.Exist(driver, wlLogin.QATUserName).SendKeys(Keys.Enter);
                    break;
            }

            Thread.Sleep(20000);
        }
        private static void ChooseEventToBet(EventDay day)
        {
            var wlLeft = new WLLeft(driver);
            SwitchFrame(Frame.LeftPanel);
      
            switch (day)
            {
                case EventDay.Yesterday:
                    ElementVerify.Visable(driver, wlLeft.InplayTab).ClickOnIt("InplayTab");
                    ElementVerify.Visable(driver, wlLeft.InplayCheckboxAll).ClickOnIt("All Competition Checkbox");
                    break;
                case EventDay.Today:
                    ElementVerify.Visable(driver, wlLeft.TodayTab).ClickOnIt("TodayTab");
                    ElementVerify.Visable(driver, wlLeft.Today1stAHOU).ClickOnIt("Today's First sport AHOU");
                    break;
                case EventDay.Tomorrow:
                    ElementVerify.Visable(driver, wlLeft.TomorrowTab).ClickOnIt("TomorrowTab");
                    ElementVerify.Visable(driver, wlLeft.Tomorrow1stAHOU).ClickOnIt("Tomorrow's First sport AHOU");
                    break;
            }
            PlaceBet(day);
        }
        private static void PlaceBet(EventDay day)
        {
            IAlert alert;
            var wlRight = new WLRight(driver);
            var wlLeft = new WLLeft(driver);
            string eventid = String.Empty;
            
            switch (day)
            {
                case EventDay.Yesterday:
                    eventid = "709369";
                    break;
                case EventDay.Today:
                    eventid = "";
                    break;
                case EventDay.Tomorrow:
                    eventid = "";
                    break;
            }

            By MoreBetButton = By.Id("mbb_"+ eventid +"");
            By MoreBetExpand = By.XPath("//*[@id='eventContainer'][@eid='" + eventid + "']");
            By Odds = By.XPath("//*[@id='eventContainer']/div[2]/table/tbody/tr[2]/td[3]/div/a");


            // Add selection to bet slip
            SwitchFrame(Frame.RightPanel);
            Thread.Sleep(1000);
            ElementVerify.PresenceAll(driver, MoreBetButton).ClickOnIt("Morebet");
            if (ElementVerify.Visable(driver, MoreBetExpand) != null)
                ElementVerify.Visable(driver, Odds).ClickOnIt("Odds");


            // Swtich to Bet Slip
            SwitchFrame(Frame.LeftPanel);
            ElementVerify.Visable(driver, wlLeft.BetSlipStake).EnterText("Stake", "20000");
            ElementVerify.Visable(driver, wlLeft.PlaceBetButton).ClickOnIt("PlaceBetButton");
            alert = driver.SwitchTo().Alert();
            alert.Accept();
            if (ElementVerify.Visable(driver, wlLeft.BetConfirm) != null)
                WriteConsole.Green("Place bet successfully");
            //string[] wagers = ElementVerify.Visable(driver, wlLeft.WagerNumber).Text;

           
        }
        private static void SwitchFrame(Frame frame)
        {
            var wlFrame = new WLFrame(driver);

            driver.SwitchTo().DefaultContent();
            if (ElementVerify.Exist(driver, wlFrame.IFrame,3) != null)
                driver.SwitchTo().Frame(driver.FindElement(wlFrame.IFrame));
            else if (ElementVerify.Exist(driver, wlFrame.QATIFrame, 3) != null)
                driver.SwitchTo().Frame(driver.FindElement(wlFrame.QATIFrame));

            switch (frame)
            {
                //Sport Menu
                case Frame.LeftPanel:     
                    ElementVerify.Exist(driver, wlFrame.LeftFrame);
                    driver.SwitchTo().Frame(driver.FindElement(wlFrame.LeftFrame));
                    break;
                //Main Odds Page
                case Frame.RightPanel:   
                    ElementVerify.Exist(driver, wlFrame.RightFrame);
                    driver.SwitchTo().Frame(driver.FindElement(wlFrame.RightFrame));
                    break;
        }
        
                
        

    }
        private enum Frame
        {
            LeftPanel,
            RightPanel
        }
        private enum EventDay
        {
            Yesterday,
            Today,
            Tomorrow
        }
    }
}
