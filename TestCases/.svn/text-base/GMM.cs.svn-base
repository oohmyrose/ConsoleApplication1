using ConsoleApplication1.Extensions;
using ConsoleApplication1.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using RelevantCodes.ExtentReports;

namespace ConsoleApplication1.TestCases
{
    class GMM
    {
        public static IWebDriver driver = new ChromeDriver();
        public static WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        public const string sport = "Football";
        public const string competition = "(Love FB)";  
        public const string home = "Jeff Duan";         
        public const string away = "Neil Tsai";           
        public const string gametype = "Inning";
        public static string eventid = String.Empty;
       

        public static void Login(TheEnvironment environment)
        {
            Report.Case("GMM Login", "Login to GMM");
            string URL = String.Empty;
            string username = String.Empty;
            string password = String.Empty;
            var gmmLogin = new GMMLogin(driver);

            // Assign URL/user/pwd based on environment 
            switch (environment)
            {
                case TheEnvironment.QAT:
                    URL = System.Configuration.ConfigurationManager.AppSettings["qatGMMURL"];
                    username = System.Configuration.ConfigurationManager.AppSettings["qatGMMUser"];
                    password = System.Configuration.ConfigurationManager.AppSettings["qatGMMPwd"];
                    break;
                case TheEnvironment.UAT:
                    URL = System.Configuration.ConfigurationManager.AppSettings["uatGMMURL"];
                    username = System.Configuration.ConfigurationManager.AppSettings["uatGMMUser"];
                    password = System.Configuration.ConfigurationManager.AppSettings["uatGMMPwd"];
                    break;
                
            }
           
            driver.Manage().Window.Maximize();
            driver.Url = URL + "Login.aspx";
            gmmLogin.UserName.EnterText("GMM Login User Textbox", username);
            gmmLogin.Password.EnterText("GMM Login Password Textbox", password);
            gmmLogin.Submit.ClickOnIt("GMM Login Button");

            
            if (ElementVerify.Exist(driver, gmmLogin.SiteMenuAfterLogin) != null)
            {
                WriteConsole.Green("You successfully Login to GMM");
                Report.Log(LogStatus.Pass,"Login GMM", "You successfully Login to GMM");
                Report.PrintScreen(driver);
            }
            else
            {
                WriteConsole.Green("You successfully Login to GMM");
                WriteConsole.Red(String.Format(ElementVerify.Exist(driver, gmmLogin.LoginErrorMessage).Text));
                WriteConsole.Green("Suggested Solution: You can go to GMM Server, and restart DNS Client Sevice");
                Report.Log(LogStatus.Fail, "Login GMM", String.Format(ElementVerify.Exist(driver, gmmLogin.LoginErrorMessage).Text));
                Report.PrintScreen(driver);
                throw new Exception("GMM Login Failed");
            }
        }           
        public static string CreateEvent()
        {
            Report.Case("GMM Create Event", "GMM Creates a fixture event and fetch the event id");
            DateTime inplaytime = DateTime.Now.AddHours(-14);         //2 hours before NOW  ---- Local(GMT+8),Server(GMT-4)
            var gmmMenu = new GMMMenu(driver);
            var gmmCreateEvent = new GMMCreateEvent(driver);
 
            #region (1) Go to EventCreate Page  
            ElementVerify.Exist(driver, gmmMenu.Sportsbook).ClickOnIt("Sportsbook");                                       
            ElementVerify.Exist(driver, gmmMenu.Event).MouseOver(driver, "Event");
            ElementVerify.Exist(driver, gmmMenu.FixtureEvent).ClickOnIt("Fixture Event");                   
            ElementVerify.Exist(driver, gmmCreateEvent.Create).ClickOnIt("Create Button"); 
            #endregion
            #region (2) Select sport / competition / participant ..  
            // Sport
            ElementVerify.Exist(driver, gmmCreateEvent.SportDropdown).SelectByText("CreateEventPage Sport", sport);  
            
            // Competition in dynamic dropdownlist
            ElementVerify.Exist(driver, By.CssSelector("#mainContent_CompetitionDropDownList > option[title='" + competition + "']"));
            ElementVerify.Exist(driver, gmmCreateEvent.CompetitionDropdown).SelectByText("CreateEventPage Competition", competition); 

            // Home Away 
            ElementVerify.Exist(driver, By.CssSelector("#mainContent_HomeRunnerDropDownList > option[title='" + home + "']"));
            ElementVerify.Exist(driver, gmmCreateEvent.HomeDropdown).SelectByText("CreateEventPage Home", home);
            ElementVerify.Exist(driver, gmmCreateEvent.AwayDropdown).SelectByText("CreateEventPage Away", away); 
            
            // In-Play
            ElementVerify.Exist(driver, gmmCreateEvent.InPlayCheckbox).ClickOnIt("IsInPlay"); 
            
            // Event DateTime
            ElementVerify.Exist(driver, gmmCreateEvent.EventDatePicker).EnterText("CreateEventPage Date", inplaytime.ToString("dd/MM/yyyy"));
            ElementVerify.Exist(driver, gmmCreateEvent.EventHourDropdown).SelectByText("CreateEventPage Hour", inplaytime.ToString("HH"));
            ElementVerify.Exist(driver, gmmCreateEvent.EventMinuteDropdown).SelectByText("CreateEventPage Minute", inplaytime.ToString("mm")); 

            // Ground Type = Neutral(2)
            ElementVerify.Exist(driver, gmmCreateEvent.GroundTypeDropdown).SelectByValue("CreateEventPage GroundType", "2"); 

            // GameType
            try
            {
                ElementVerify.Exist(driver, gmmCreateEvent.GroundTypeDropdown).SelectByText("CreateEventPage GameType", gametype);
            }
            catch (NoSuchElementException)
            {
                WriteConsole.Yellow("This sport has no GameType");
            }
            #endregion
            #region (3) Save and Get Eevnt ID 
            ElementVerify.Exist(driver, gmmCreateEvent.SaveButton).ClickOnIt("Button_Save");
            if (ElementVerify.Exist(driver, gmmCreateEvent.CreateFailMsg, 60).GetAttribute("class") != "hide")
            {
                WriteConsole.DarkRed(ElementVerify.Exist(driver, gmmCreateEvent.CreateFailMsg).Text);
                Report.Log(LogStatus.Fail,"Get Event id",ElementVerify.Exist(driver, gmmCreateEvent.CreateFailMsg).Text);
                Report.PrintScreen(driver);
                throw new Exception("GMM CreateEvent Failed");
            }


            if (ElementVerify.Exist(driver, gmmCreateEvent.CreateSuccessMsg, 60) != null)
            {
                WriteConsole.Green(String.Format("Create Event Successfully! Event id : {0} ({1})", eventid, sport));
                Report.Log(LogStatus.Pass, "Get event id", String.Format("Create Event Successfully! Event id : {0} ({1})",  ElementVerify.Exist(driver, gmmCreateEvent.ParentEventId, 60).GetAttribute("Value").Trim() , sport));
                Report.PrintScreen(driver);
                return ElementVerify.Exist(driver, gmmCreateEvent.ParentEventId, 60).GetAttribute("Value").Trim();     //eventid            
            }
            else
            {
                Report.Log(LogStatus.Fail, "Get event id", "Cannot find eventid");
                Report.PrintScreen(driver);
                throw new Exception("GMM CreateEvent Failed");
            }


            Thread.Sleep(2000);
            #endregion
        }
        public static void GoToMMPage(string eventid)
        {
            Report.Case("GMM MMPage","Go to MM Page and search the event we created");
            var gmmMenu = new GMMMenu(driver);
            var gmmMMPage = new GMMMMPage(driver);
            #region (1) Go to MM Page
            ElementVerify.Exist(driver, gmmMenu.MarketManagement).ClickOnIt("Market Management");
            ElementVerify.Exist(driver, gmmMenu.MarketManagementNormal).ClickOnIt("Market Management (Normal)");
            Thread.Sleep(1000);     
            #endregion
            #region (2) Choose Sport / Competition / Event ...    
            // Sport
            ElementVerify.Exist(driver, gmmMMPage.SportCriteria).ClickOnIt("Sport");
            ElementVerify.Exist(driver, gmmMMPage.SportPopup);
            ElementVerify.Exist(driver, gmmMMPage.SportList);
            ElementVerify.Exist(driver, By.XPath("//label[contains(text() ,'" + sport + "')]")).ClickOnIt(sport);
            ElementVerify.Exist(driver, gmmMMPage.SportSubmit).ClickOnIt("SubmitSport"); 
            
            // Odds Page (In-Play)
            ElementVerify.Exist(driver, gmmMMPage.OddsPageInPlay);
            Thread.Sleep(2000);
            ElementVerify.Exist(driver, gmmMMPage.OddsPageDropdown).SelectByText("All Market (In Play)", "All Market (In Play)");  
            
            // Competition
            wait.Until(ExpectedConditions.ElementToBeClickable(gmmMMPage.SearchButton));
            ElementVerify.Exist(driver, gmmMMPage.CompetitionCriteria).ClickOnIt("Competition Filter");
            ElementVerify.Exist(driver, gmmMMPage.ComptitionSelectAll).ClickOnIt("Deselect All");
            ElementVerify.Exist(driver, By.XPath("//label[contains(text() , '" + competition + "')]")).ClickOnIt(competition);
            ElementVerify.Exist(driver, gmmMMPage.CompetitionSubmit).ClickOnIt("Confirm Competition"); 
            
            // Event  
            wait.Until(ExpectedConditions.ElementToBeClickable(gmmMMPage.SearchButton));
            ElementVerify.Exist(driver, gmmMMPage.EventCriteria).ClickOnIt("Event Filter");
            ElementVerify.Exist(driver, gmmMMPage.EventTextbox).EnterText("EventID Textbox", eventid);
            ElementVerify.Exist(driver, gmmMMPage.EventSelectAll).ClickOnIt("Deselect All");           
            ElementVerify.Exist(driver, By.Id("" + eventid + "")).ClickOnIt("Filter one event we created");
            ElementVerify.Exist(driver, gmmMMPage.EventSubmit).ClickOnIt("Confirm Event"); 

            // Filter Status (Skip for now)
            

            // Filter Date Range (Skip for now)
            // (1) Inplay events are no need to define range
            // (2) Pre Start events 
            #endregion
            #region (3) Base on filters to search the event  
            wait.Until(ExpectedConditions.ElementToBeClickable(gmmMMPage.SearchButton));
            ElementVerify.Exist(driver,gmmMMPage.SearchButton).ClickOnIt("SearchEvent");
            WriteConsole.Green(String.Format("Search Event id : {0} ({1})", eventid, sport));
            #endregion
            ElementVerify.Exist(driver, By.Id("evt" + eventid));
            Report.PrintScreen(driver);
        }
        public static void UpdateOdds(string eventid)
        {
            Report.Case("GMM Update Odds", "GMM updates odds of the event we created");
            var gmmMMPage = new GMMMMPage(driver);
            Random random = new Random();

            #region (1) Fetch every cells of the event  
            ElementVerify.Exist(driver, By.Id("evt"+eventid));
            IWebElement eventTable = ElementVerify.Exist(driver, By.XPath("//*[@id='evt" + eventid + "']"));
            ICollection<IWebElement> cells = eventTable.FindElements(By.TagName("td"));
            #endregion

            #region (2) Key odds
            int cellcount = 1;
            foreach (var cell in cells)
            {
                if (cell.FindElement(By.XPath("div")).GetAttribute("class") == "odds")
                {
                    // Without Spread
                    if (cell.GetAttribute("wsml") == "false")
                    {

                        cell.FindElement(By.XPath("div/a")).ClickOnIt("oddsbox");
                        ElementVerify.Exist(driver,gmmMMPage.NoHandicapOdds);
                        ICollection<IWebElement> oddsEuros = driver.FindElements(gmmMMPage.NoHandicapOdds);
                        foreach (var oddsEuro in oddsEuros)
                        {
                            oddsEuro.EnterText("Euro Odds", random.Next(2,15).ToString());
                            WriteConsole.Cyan(String.Format("Cell Number {0} : you key in odds {1}", cellcount, random));
                            Report.Log(LogStatus.Pass, "Update odds",String.Format("Cell Number {0} : you key in odds {1}", cellcount, random));
                        }

                        ElementVerify.Exist(driver, gmmMMPage.OddsSaveButton).ClickOnIt("Save Button");
                   
                    } 
                    // With Spread
                    else
                    {      
                        double handicap = random.Next(0, 15) + 0.5;

                        cell.FindElement(By.XPath("div/a")).ClickOnIt("oddsbox");
                        if (ElementVerify.Exist(driver, gmmMMPage.HandicapOdds,3) != null)
                        {
                            if (ElementVerify.PresenceAll(driver, gmmMMPage.HandicapDropdown) != null)
                                ElementVerify.PresenceAll(driver, gmmMMPage.HandicapDropdown).SelectByText("handicap Line", handicap.ToString());
                            else if (ElementVerify.Exist(driver, gmmMMPage.HandicapTextbox, 3) != null)
                                ElementVerify.Exist(driver, gmmMMPage.HandicapTextbox).EnterText("handicap Line", handicap.ToString());
                            ElementVerify.Exist(driver, gmmMMPage.HandicapOdds).EnterText("Home Odds", random.NextDouble().ToString("0.##"));
                            ElementVerify.Exist(driver, gmmMMPage.HandicapOdds).SendKeys(Keys.Enter);
                        }
                        else if (ElementVerify.Exist(driver, gmmMMPage.NoHandicapOdds, 3) != null)
                        {
                            ElementVerify.Exist(driver, gmmMMPage.NoHandicapOdds).EnterText("Home Odds", random.NextDouble().ToString("0.##"));
                            ElementVerify.Exist(driver, gmmMMPage.NoHandicapOdds).SendKeys(Keys.Enter);
                        }
                        
                        WriteConsole.Cyan(String.Format("Cell Number {0} : you key in odds {1}", cellcount, random));
                        Report.Log(LogStatus.Pass, "Update odds", String.Format("Cell Number {0} : you key in odds {1}", cellcount, random));
                    } 
                }
                else
                {
                    WriteConsole.Cyan(String.Format("Cell Number {0} : This is not the odds link", cellcount));
                }
                cellcount++;
            }
            #endregion
            Thread.Sleep(1000);
            Report.PrintScreen(driver);
        }
        public static void OpenMarket(string eventid)
        {
            Report.Case("GMM Open Markets", "Open market lines of the event we created");
            var gmmMMPage = new GMMMMPage(driver);
            //Open Main markets
            ElementVerify.Exist(driver, By.Id("button2" + eventid)).ClickOnIt("Pause Button");
            ElementVerify.Exist(driver, gmmMMPage.OpenMarketline).ClickOnIt("Open Markettline");
            Thread.Sleep(3000);
            Report.PrintScreen(driver);
        }
        public static void KeepEvent(string eventid)
        {
            Report.Case("GMM Keep Markets", "Send market lines for resulting");
            // wait for action button
            //ElementVerify.Wait(driver, By.Id("button2" + eventid)));
            IWebElement pauseButton = ElementVerify.Exist(driver, By.Id("button2" + eventid));
            IAlert alert;

            if (driver.FindElement(By.Id("amlc_"+eventid)).Displayed) //football
            {
                // Pause
                pauseButton.ClickOnIt("Status would be Pasue");
                ElementVerify.PresenceAll(driver, By.XPath("//*[@id='excla"+eventid+"'][contains(@style, 'display: inline-block')]"));
                Console.WriteLine("Marketline Status : Open to Pause");
                Thread.Sleep(2000);
     
                // click on pause again to perform close
                pauseButton.ClickOnIt("Pause Button");
                ElementVerify.PresenceAll(driver, By.XPath("//*[@id='button2" + eventid + "'][contains(@class, 'btn_refresh btn_refresh_black')]"));
                ElementVerify.Visable(driver, By.Id("closeCurrentPageMlButton")).ClickOnIt("Close Markettline");


                
                // Sent for resulting
                ElementVerify.Visable(driver, By.Id("pbet" + eventid)).ClickOnIt("Statistic Icon");
                ElementVerify.Clickable(driver, By.Id("mkt_stats2")).ClickOnIt("Resulting Icon");
                ElementVerify.Clickable(driver, By.Id("SendAllForResulting")).ClickOnIt("Send Resulting (Event)");

                // 1st Alert to confrim doing resulting 
                //wait.Until(ExpectedConditions.AlertIsPresent());
                ElementVerify.Alert(driver, 20);
                alert = driver.SwitchTo().Alert();
                alert.Accept();

                // 2nd Alert to inform resulting is finished
                //wait.Until(ExpectedConditions.AlertIsPresent());
                ElementVerify.Alert(driver, 20);
                alert.Accept();

            }
            else //non football
            {
                
            }

            Thread.Sleep(2000);
            Report.PrintScreen(driver);
        }
        public static void ResultAndSettleEvent(String eventid)
        {
            Report.Case("GMM Result Process", "Enter result and do settlement by event");
            DateTime eventDayFrom = DateTime.Now.AddDays(-2);
            IAlert alert;
            var settlementPage = new SettlementPage(driver, eventid);


            #region (1) Go to Result Process & Settlemenet Page
            driver.FindElement(By.LinkText("Result Process & Settlement")).ClickOnIt("Result Process & Settlement");
            if (ElementVerify.Exist(driver, By.LinkText("Result Process")) != null)
                driver.FindElement(By.LinkText("Result Process")).MouseOver(driver, "Result Process");
            if (ElementVerify.Exist(driver, By.XPath("//a[contains(@href,'/DSA/ResultProcessing/ResultProcessing.aspx')]")) != null)
            {
                driver.FindElement(By.XPath("//a[contains(@href,'/DSA/ResultProcessing/ResultProcessing.aspx')]")).MouseOver(driver, "Sub Result Process");
                driver.FindElement(By.XPath("//a[contains(@href,'/DSA/ResultProcessing/ResultProcessing.aspx')]")).ClickOnIt("Result Process Page");
            }
            #endregion

            #region (2) Event ID
            if (ElementVerify.Exist(driver, settlementPage.EventId) != null)
                driver.FindElement(settlementPage.EventId).EnterText("Event ID", eventid);
            #endregion

            #region (3) Pick Event Date From
            if (ElementVerify.Exist(driver, By.Id("txtFromDate")) != null)
            {
                //Click on calendar icon
                settlementPage.StartDate.ClickOnIt("Event Date From");

                //Find the calendar
                ElementVerify.Exist(driver, By.ClassName("ui-datepicker-calendar"));
                ICollection<IWebElement> columns = settlementPage.CalendarPicker.FindElements(By.TagName("td"));

                //Click on the number if cell = today's date-2
                foreach (IWebElement cell in columns)
                {
                    if (cell.Text.Equals(eventDayFrom.ToString("dd")))
                    {
                        cell.ClickOnIt("date");
                        break;
                    }
                }
            }
            #endregion

            #region (4) Search the event
            if (ElementVerify.Exist(driver, By.Id("btnSubmit")) != null)
                settlementPage.SearchButton.ClickOnIt("Search");
            #endregion

            #region (5) Find number of market actions item
            // Wait until loading complete
            ElementVerify.Exist(driver, settlementPage.LoadingBar);
            // Wait for the event
            ElementVerify.Exist(driver, settlementPage.RefreshButton);
            // Count items
            ICollection<IWebElement> marketItems = driver.FindElements(settlementPage.MarketLine);
            Console.WriteLine(String.Format("Number of market line : {0}", marketItems.Count));
            #endregion

            #region (6) Enter Result
            List<string> marketlist = new List<string>();
            foreach (IWebElement marketItem in marketItems)
            {
                //Get marketline result action id
                marketlist.Add(marketItem.GetAttribute("id"));
                Console.WriteLine(marketItem.GetAttribute("id"));
            }


            for (int i = 0; i < marketlist.Count; i++)
            {
                if (driver.FindElement(By.XPath("//*[@id='" + marketlist[i] + "']/span[3]")).Text == "Not Resulted")
                {
                    //Click on Action Button
                    driver.FindElement(By.XPath("//*[@id='" + marketlist[i] + "']/span[2]")).ClickOnIt("Marketline Action");
                    Thread.Sleep(2000);
                    ElementVerify.Exist(driver, settlementPage.ActionDropdown);
                    driver.FindElement(By.LinkText("Enter Result")).ClickOnIt("Enter Result");
                    ElementVerify.Exist(driver, settlementPage.ResultPopup);

                    //Key Result for scoreline
                    if (driver.FindElement(settlementPage.ScorelineResult).Displayed)
                    {
                        ElementVerify.Exist(driver, settlementPage.ScoreHome);
                        driver.FindElement(settlementPage.ScoreHome).EnterText("Score Home", "2");
                        driver.FindElement(settlementPage.ScoreAway).EnterText("Score Away", "0");
                        Thread.Sleep(2000);
                        driver.FindElement(settlementPage.SaveButton).MouseOverThenClick(driver, "Save");

                        // Alert message
                        wait.Until(ExpectedConditions.AlertIsPresent());
                        alert = driver.SwitchTo().Alert();
                        alert.Accept();
                    }
                    else if (driver.FindElement(By.XPath("//*[@id='" + marketlist[i] + "']/span[2]/ul[2]/li/table[2]")).Displayed) //Key Result for SPWOS
                    {
                        ElementVerify.Exist(driver, By.XPath("//*[@id='" + marketlist[i] + "']/span[2]/ul[2]/li/table[2]/tbody/tr/td[2]/select"));
                        Thread.Sleep(2000);
                        driver.FindElement(By.XPath("//*[@id='" + marketlist[i] + "']/span[2]/ul[2]/li/table[2]/tbody/tr/td[2]/select")).ClickOnIt("Win Selection");
                        driver.FindElement(By.XPath("//*[@id='" + marketlist[i] + "']/span[2]/ul[2]/li/table[2]/tbody/tr/td[2]/select")).SelectByText("Result Option","WIN");
                        Thread.Sleep(2000);
                        driver.FindElement(settlementPage.SaveButtonProp).MouseOverThenClick(driver, "Save");

                        // Alert message
                        //Thread.Sleep(2000);
                        wait.Until(ExpectedConditions.AlertIsPresent());
                        alert = driver.SwitchTo().Alert();
                        alert.Accept();
                        Thread.Sleep(2000);
                    }
                }
                else 
                {
                    Console.WriteLine("This marketline has resulted already");
                }
            }
            #endregion

            #region (7) Void Result
            #endregion

            #region (8) Settle All BU
            ElementVerify.Exist(driver, settlementPage.CheckAll);
            driver.FindElement(settlementPage.CheckAll).ClickOnIt("Check All BU");
            driver.FindElement(settlementPage.SettleButton).ClickOnIt("Do Settlement");
            // Alert message (Confirm do Settlement?)
            wait.Until(ExpectedConditions.AlertIsPresent());
            alert = driver.SwitchTo().Alert();
            alert.Accept();
            // Alert message (Settlement successfull!)
            wait.Until(ExpectedConditions.AlertIsPresent());
            alert = driver.SwitchTo().Alert();
            alert.Accept();
            #endregion

            #region (9) Unsettle All BU
            /*
            ElementVerify.IsElementExists(driver, settlementPage.CheckAll);
            driver.FindElement(settlementPage.CheckAll).ClickOnIt("Check All BU");
            driver.FindElement(settlementPage.UnSettleButton).ClickOnIt("Do UnSettlement");
            // Alert message (Confirm do UnSettlement?)
            wait.Until(ExpectedConditions.AlertIsPresent());
            alert = driver.SwitchTo().Alert();
            alert.Accept();
            // Alert message (UnSettlement successfull!)
            wait.Until(ExpectedConditions.AlertIsPresent());
            alert = driver.SwitchTo().Alert();
            alert.Accept();
            */
            #endregion

            Report.PrintScreen(driver);
        }
        public static void CheckReport(string BetNo) 
        {
            Report.Case("GMM Wager Enquiry", "Verify wager numbers in Wager Enquiry");
            DateTime eventDayFrom = DateTime.Now.AddDays(-2);
            var gmmMenu = new GMMMenu(driver);
            var wagerEnquiry = new WagerEnquiry(driver);

            #region (1) Access Wager Enquiry
            ElementVerify.Exist(driver, gmmMenu.Report).ClickOnIt("Report");
            ElementVerify.Exist(driver, gmmMenu.WagerEnquiry).ClickOnIt("Wager Enquiry");
            #endregion

            #region (2) Switch to iframe and Select Wager No.
            //Switch to iframe
            driver.SwitchTo().Frame(driver.FindElement(By.TagName("iframe")));
            //Select Wager No.
            ElementVerify.Exist(driver, wagerEnquiry.UserWagerOption).ClickOnIt("User Code or Wager No.");
            ElementVerify.Exist(driver, wagerEnquiry.UserWagerOption).SelectByText("select wager no", "Wager No.");
            #endregion

            #region (3) Pick Event Date From
            if (ElementVerify.Exist(driver, wagerEnquiry.StartDate) != null)
            {
                //Click on calendar icon
                ElementVerify.Exist(driver, wagerEnquiry.StartDate).ClickOnIt("Event Date From");
                ElementVerify.Exist(driver, wagerEnquiry.CalendarPicker);
                ICollection<IWebElement> columns = driver.FindElement(wagerEnquiry.CalendarPicker).FindElements(By.TagName("td"));

                //Click on the number if cell = today's date-2
                foreach (IWebElement cell in columns)
                {
                    if (cell.Text.Equals(eventDayFrom.ToString("dd")))
                    {
                        cell.ClickOnIt("date");
                        break;
                    }
                }
            }
            #endregion

            #region (4) Fetch All Bet Number from TouTou
            
            char[] delimiterChars = {','};
            BetNo = BetNo.Remove(BetNo.Length - 1);        // Reomove the last comma (,)
            string[] wagers = BetNo.Split(delimiterChars);
            foreach (string wager in wagers)
            {
                // Key in wager number in textbox
                Thread.Sleep(2000);
                ElementVerify.Exist(driver, wagerEnquiry.UserWagerValue).EnterText("Wager No.", wager);
                ElementVerify.Exist(driver, wagerEnquiry.SearchButton).ClickOnIt("Search");
                
                // Wait for data displayed, then fetch wager number
                if (ElementVerify.Exist(driver, wagerEnquiry.WagerToVerify) == null)
                {
                    WriteConsole.DarkRed("Unable to find the wager");
                    continue;
                }
               
                if (wager == driver.FindElement(wagerEnquiry.WagerToVerify).Text)
                    WriteConsole.Green("Wager Verified");
                else
                    WriteConsole.DarkRed("Unable to find the wager");
            }

            #endregion
            Report.PrintScreen(driver);
        }

    }

    public enum TheEnvironment
    {
        QAT,
        UAT
    }

}
