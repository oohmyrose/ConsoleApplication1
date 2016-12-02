using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleApplication1.Extensions;

namespace ConsoleApplication1.TestCases
{
    class TouTou
    {
        public class MemberSite
        {
            public static IWebDriver driver = new ChromeDriver();
            public static WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            public const string qatURL = "http://www.ttqat.com/en-gb/sportsbook/asiaindex";
            public const string uatURL = "http://www.ttuat.com/en-gb/sportsbook/asiaindex";


            // Element of LoginUser / LoginPassword 
            [FindsBy(How = How.Id, Using = "LoginName")]
            IWebElement UserName { get; set; }

            [FindsBy(How = How.Id, Using = "Password")]
            IWebElement Password { get; set; }

            [FindsBy(How = How.XPath, Using = "//button[contains(text(),'LOG IN')]")]
            IWebElement Submit { get; set; }

            public static void Login()
            {
                driver.Manage().Window.Maximize();
                driver.Url = uatURL;

                var toutou = new MemberSite();
                PageFactory.InitElements(driver, toutou);
                Thread.Sleep(2000);
                toutou.UserName.EnterText("TT Login User", "lindatestrmb8");
                toutou.Password.EnterText("TT Login Password", "123456a");
                Thread.Sleep(2000);
                toutou.Submit.ClickOnIt("TT Login Button");

                //Wait for balance to verify if login or not
                // --- (still need to handle popup message before login) ---
                wait.Until(ExpectedConditions.ElementExists(By.Id("header_balance")));
                Console.WriteLine("Login Success");


            }

            public static void GoToSportsbook()
            {
                //Navigate Sportsbook
                driver.FindElement(By.LinkText("SPORTS")).ClickOnIt("Sportsbook");

            }
            public static string PlaceBet(string eventid)
            {
                String BetNo = String.Empty;
                try
                {
                    #region 1. Select in-play football event
                    Console.WriteLine("Select In-Play Football Event");
                    Thread.Sleep(2000);
                    wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='sbk-frame']")));
                    driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='sbk-frame']")));
                    wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='LeftFrame']")));
                    driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='LeftFrame']")));
                    wait.Until(ExpectedConditions.ElementExists(By.CssSelector("#sp1 > input.chk-box")));
                    driver.FindElement(By.CssSelector("#sp1 > input.chk-box")).Click();
                    Thread.Sleep(2000);
                    Console.WriteLine("Click Filter button to filter the test event");
                    #endregion

                    #region 2. Filter the test event
                    driver.SwitchTo().DefaultContent();
                    wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='sbk-frame']")));
                    driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='sbk-frame']")));
                    wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='MainFrame']")));
                    driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='MainFrame']")));
                    wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='center-panel']/div[2]/div[6]/a")));
                    driver.FindElement(By.XPath("//*[@id='center-panel']/div[2]/div[6]/a")).Click();
                    Thread.Sleep(2000);
                    wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='mCSB_1']/div[1]/div/div[3]")));
                    Thread.Sleep(2000);
                    IWebElement competitionTable = driver.FindElement(By.XPath("//*[@id='mCSB_1']/div[1]/div/div[3]"));
                    Thread.Sleep(2000);
                    wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@class='comp-txt']")));
                    Thread.Sleep(2000);
                    ICollection<IWebElement> cells = competitionTable.FindElements(By.XPath("//*[@class='comp-txt']"));
                    Thread.Sleep(2000);
                    String comptxt = String.Empty;
                    foreach (var cell in cells)
                    {
                        if (cell.Text.Contains("Love Football") == true)
                        {
                            cell.Click();

                        }
                    }
                    Thread.Sleep(2000);
                    wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='mCSB_1']/div[1]/div/div[2]/span[2]")));
                    driver.FindElement(By.XPath("//*[@id='mCSB_1']/div[1]/div/div[2]/span[2]")).Click();
                    Thread.Sleep(5000);
                    Console.WriteLine("Filter out the competition complete");
                    #endregion

                    #region 3. Add bet to bet slip

                    driver.SwitchTo().DefaultContent();
                    wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='sbk-frame']")));
                    driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='sbk-frame']")));
                    wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='MainFrame']")));
                    driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='MainFrame']")));
                    wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='e" + eventid + "']")));
                    IWebElement eventTable = driver.FindElement(By.XPath("//*[@id='e" + eventid + "']"));
                    Thread.Sleep(2000);
                    ICollection<IWebElement> selections = eventTable.FindElements(By.XPath("//*[@class='odds']"));
                    Thread.Sleep(2000);
                    
                    foreach (var cell in selections)
                    {
                        Console.WriteLine("Begin add bet to bet slip");
                        cell.Click();
                        driver.SwitchTo().DefaultContent();
                        wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='sbk-frame']")));
                        driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='sbk-frame']")));
                        wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='LeftFrame']")));
                        driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='LeftFrame']")));
                        wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='stakeAmt_0']")));
                        driver.FindElement(By.XPath("//*[@id='stakeAmt_0']")).SendKeys("10");
                        wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='btnPlaceBet_BS']")));
                        driver.FindElement(By.XPath("//*[@id='btnPlaceBet_BS']")).Click();
                        wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='btnConfirm_BS']")));
                        driver.FindElement(By.XPath("//*[@id='btnConfirm_BS']")).Click();
                        if (wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='left-panel']/div[1]/div[3]/div[3]/div[3]/span[2]"))).Displayed == true)
                        {
                            BetNo += driver.FindElement(By.XPath("//*[@id='left-panel']/div[1]/div[3]/div[3]/div[3]/span[2]")).Text + ",";
                            Console.WriteLine("Bet No: '" + driver.FindElement(By.XPath("//*[@id='left-panel']/div[1]/div[3]/div[3]/div[3]/span[2]")).Text + "' Placed complete");
                        }
                        else
                        {
                            Console.WriteLine("Place Bet Fail!!");
                        }
                        driver.SwitchTo().DefaultContent();
                        wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='sbk-frame']")));
                        driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='sbk-frame']")));
                        wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='MainFrame']")));
                        driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='MainFrame']")));

                    }
                    return BetNo;
                    #endregion


                }
                catch (Exception e)
                {
                    Console.WriteLine(String.Format("Place Bet fail : {0}", e.Message.ToString()));
                    return BetNo;
                }
            }

        }


    }
}
