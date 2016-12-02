using ConsoleApplication1.Extensions;
using ConsoleApplication1.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApplication1.TestCases
{
    class GMM
    {
        public static IWebDriver driver = new ChromeDriver();
        public static WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
        public const string sport = "Football";
        public const string competition = "(Love FB)";  
        public const string home = "Marie Yeh";         
        public const string away = "Sean Li";           
        public const string gametype = "Inning";
        public static string eventid = String.Empty;

        public static void Login(TheEnvironment environment)
        {
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


            if (ElementVerify.Wait(driver, gmmLogin.SiteMenuAfterLogin) != null)
            {
                WriteConsole.Green("You successfully Login to GMM");
            }
            else
            {
                WriteConsole.Red(String.Format(ElementVerify.Wait(driver, gmmLogin.LoginErrorMessage).Text));
                WriteConsole.Green("Suggested Solution: You can go to GMM Server, and restart DNS Client Sevice");
                throw new Exception("GMM Login Failed");
            }


        }
             
        public static string CreateEvent()
        {
            DateTime inplaytime = DateTime.Now.AddHours(-14);         //2 hours before NOW  ---- Local(GMT+8),Server(GMT-4)
 
            #region (1) Go to EventCreate Page
            //Nabigate Menu
            var gmmMenu = new GMMMenu(driver);
            var gmmCreateEvent = new GMMCreateEvent(driver);
            gmmMenu.Sportsbook.ClickOnIt("Sportsbook");                                       
            ElementVerify.Wait(driver, By.LinkText("Event"));
            gmmMenu.Event.MouseOver(driver, "Event");                                          
            ElementVerify.Wait(driver, By.LinkText("Fixture Event"));
            gmmMenu.FixtureEvent.ClickOnIt("Fixture Event"); 
            
            //Click on Create                    
            ElementVerify.Wait(driver, By.LinkText("Create"));
            gmmCreateEvent.Create.ClickOnIt("Button_Create"); 
            #endregion

            #region (2) Select Sport
            wait.Until(ExpectedConditions.ElementExists(By.Id("mainContent_SportDropDownList")));
            driver.FindElement(By.Id("mainContent_SportDropDownList")).SelectByText("CreateEventPage Sport", sport); 
            #endregion
            
            #region (3) Select Competition in dynamic dropdownlist
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("#mainContent_CompetitionDropDownList > option[title='" + competition + "']")));
            driver.FindElement(By.Id("mainContent_CompetitionDropDownList")).SelectByText("CreateEventPage Competition", competition); 
            #endregion

            #region (4) Select Home Runner / Away Runner
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("#mainContent_HomeRunnerDropDownList > option[title='" + home + "']")));
            driver.FindElement(By.Id("mainContent_HomeRunnerDropDownList")).SelectByText("CreateEventPage Home", home);
            driver.FindElement(By.Id("mainContent_AwayRunnerDropDownList")).SelectByText("CreateEventPage Away", away); 
            #endregion
            
            #region (5) Tick In-Play
            driver.FindElement(By.Id("mainContent_InPlayCheckBox")).ClickOnIt("IsInPlay"); 
            #endregion

            #region (6) Select Event DateTime
            driver.FindElement(By.Id("mainContent_EventDatePicker")).EnterText("CreateEventPage Date", inplaytime.ToString("dd/MM/yyyy"));
            driver.FindElement(By.Id("mainContent_EventDateHourDropDown")).SelectByText("CreateEventPage Hour", inplaytime.ToString("HH"));
            driver.FindElement(By.Id("mainContent_EventDateMinuteDropDown")).SelectByText("CreateEventPage Minute", inplaytime.ToString("mm")); 
            #endregion

            #region (7) Select Ground Type = Neutral
            driver.FindElement(By.Id("mainContent_GroundTypeDropDownList")).SelectByValue("CreateEventPage GroundType", "2"); 
            #endregion

            #region (8) Select GameType
            try
            {
                driver.FindElement(By.Id("mainContent_periodTypeDdl")).SelectByText("CreateEventPage GameType", gametype);
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("This sport has no GameType");
            }
            #endregion

            #region (9) Save and Get Eevnt ID
            wait.Until(ExpectedConditions.ElementExists(By.LinkText("Save")));
            driver.FindElement(By.LinkText("Save")).ClickOnIt("Button_Save");
            ElementVerify.Wait(driver, gmmCreateEvent.ParentEventId);
            eventid = driver.FindElement(gmmCreateEvent.ParentEventId).GetAttribute("Value").Trim();
            #endregion

            #region (10) Verify succeed or not
            //Verify succeed or not
            if (ElementVerify.Wait(driver, By.ClassName("savesuccess")) != null)
            {
                Console.WriteLine(String.Format("Create Event Successfully"));
                Console.WriteLine(String.Format("Event id : {0} ({1})", eventid, sport));
                return eventid;
            }
            else {
                throw new Exception("GMM CreateEvent Failed");
            }
            #endregion

            #region (11) Sleep
            Thread.Sleep(2000);
            #endregion
        }

        public static void GoToMMPage(string eventid)
        {

            #region (1) Go to MM Page
            driver.FindElement(By.LinkText("Market Management")).ClickOnIt("Market Management");
            wait.Until(ExpectedConditions.ElementExists(By.LinkText("Market Management (Normal)")));
            driver.FindElement(By.LinkText("Market Management (Normal)")).ClickOnIt("Market Management (Normal)");
            Thread.Sleep(1000);
            wait.Until(ExpectedConditions.ElementExists(By.Id("SearchBarSportCriteriaButton")));
            driver.FindElement(By.Id("SearchBarSportCriteriaButton")).ClickOnIt("Sport"); 
            #endregion

            #region (2) Select Sport
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@id='simple-popup-box'][contains(@style,'display: block')]")));
            wait.Until(ExpectedConditions.ElementExists(By.Id("sportCriteria")));
            driver.FindElement(By.XPath("//label[contains(text() ,'" + sport + "')]")).ClickOnIt(sport);
            driver.FindElement(By.Id("sportCriteriaSubmit")).ClickOnIt("SubmitSport"); 
            #endregion

            #region (3) Select In-Play Page
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector("#SearchBarOddsPageComboBox > option[value='All Market (In Play)']")));
            Thread.Sleep(2000);
            driver.FindElement(By.Id("SearchBarOddsPageComboBox")).SelectByText("All Market (In Play)", "All Market (In Play)");  
            #endregion

            #region (4) Filter Competition
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("SearchBarSearchButton")));
            driver.FindElement(By.Id("SearchBarCompetitionCriteriaButton")).ClickOnIt("Competition Filter");
            wait.Until(ExpectedConditions.ElementExists(By.Id("compCriteriaSelectAll")));
            driver.FindElement(By.XPath("//label[contains(text() , 'Select All Competitions')]")).ClickOnIt("Deselect All");
            driver.FindElement(By.XPath("//label[contains(text() , '" + competition + "')]")).ClickOnIt(competition);
            driver.FindElement(By.Id("CompetetionCriteriaSubmit")).ClickOnIt("Confirm Competition"); 
            #endregion

            #region (5) Filter Event  
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("SearchBarSearchButton")));
            driver.FindElement(By.Id("SearchBarEventCriteriaButton")).ClickOnIt("Event Filter");
            wait.Until(ExpectedConditions.ElementExists(By.Id("eventCriteriaSelectAll"))); 
            // Get Event ID
            //eventid = driver.FindElement(By.XPath("//label[contains(text() , '" + home + " vs " + away + "')]")).GetAttribute("for");
            // Enter Event ID in textbox, then submit
            driver.FindElement(By.Id("eventSearchTextBox")).EnterText("EventID Textbox", eventid);
            driver.FindElement(By.XPath("//label[contains(text() , 'Select All Events')]")).ClickOnIt("Deselect All");
            driver.FindElement(By.Id("" + eventid + "")).ClickOnIt("Filter one event we created");
            //driver.FindElement(By.XPath("//label[contains(text() , '" + home + " vs " + away + "')]")).ClickOnIt("Event Name");
            driver.FindElement(By.Id("EventCriteriaSubmit")).ClickOnIt("Confirm Event"); 
            #endregion

            #region (6) Filter Status (Skip for now)

            // Filter Status (Skip for now)
            
            #endregion

            #region (7) Filter Date Range (Skip for now)
            //Filter Date Range (Skip for now)
            // (1) Inplay events are no need to define range
            // (2) Pre Start events 
            #endregion

            #region (8) Base on filters to search the event
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("SearchBarSearchButton")));
            driver.FindElement(By.Id("SearchBarSearchButton")).ClickOnIt("SearchEvent");
            Console.WriteLine(String.Format("Event id : {0} ({1})", eventid, sport));
            #endregion
 
        }

        public static void UpdateOdds()
        {
            #region (1) Fetch every cell in 
            wait.Until(ExpectedConditions.ElementExists(By.Id("evt"+eventid)));
            IWebElement eventTable = driver.FindElement(By.XPath("//*[@id='evt" + eventid + "']"));
            ICollection<IWebElement> cells = eventTable.FindElements(By.TagName("td"));
            #endregion

            int cellcount = 1;
            String odds = String.Empty;
            
            foreach (var cell in cells)
            {
                if (cell.FindElement(By.XPath("div")).GetAttribute("class") == "odds")
                {
                    #region If Odds = Without Spread
                    if (cell.GetAttribute("wsml") == "false")
                    {
                        odds = "1.02";

                        cell.FindElement(By.XPath("div/a")).ClickOnIt("oddsbox");
                        wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@class='oddUpdateInput']")));
                        ICollection<IWebElement> oddsEuros = driver.FindElements(By.XPath("//*[@class='oddUpdateInput']"));
                        foreach (var oddsEuro in oddsEuros)
                        {
                            oddsEuro.EnterText("Euro Odds", odds);
                            double oddsNum = Convert.ToDouble(odds);
                            oddsNum += 0.68;
                            odds = Convert.ToString(oddsNum);
                            Console.WriteLine(String.Format("Cell Number {0} : you key in odds {1}", cellcount, odds));
                        }

                        // --- Save 
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("saveButton")));
                        driver.FindElement(By.Id("saveButton")).ClickOnIt("Save Button");

                        // --- Cancel 
                        //wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("cancelButton")));
                        //driver.FindElement(By.Id("cancelButton")).ClickOnIt("Cancel Button");
                    } 
                    #endregion
                    #region If Odds = With Spread
                    else
                    {
                        odds = "0.68";
                        string handicap = "2.5";

                        // with spread odds and handicap
                        cell.FindElement(By.XPath("div/a")).ClickOnIt("oddsbox");
                        wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@class='formInput']")));
                        driver.FindElement(By.XPath("//*[@class='formInput']")).EnterText("Home Odds", odds);
                        wait.Until(ExpectedConditions.ElementExists(By.XPath("//*[@class='formInputDrop']")));
                        driver.FindElement(By.XPath("//*[@class='formInputDrop']")).SelectByText("handicap Line", handicap);

                        // --- Save 
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("saveButton")));
                        driver.FindElement(By.Id("saveButton")).ClickOnIt("Save Button");

                        // --- Cancel 
                        //wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("cancelButton")));
                        //driver.FindElement(By.Id("cancelButton")).ClickOnIt("Cancel Button");
                        Console.WriteLine(String.Format("Cell Number {0} : you key in odds {1}", cellcount, odds));
                    } 
                    #endregion
                }
                else
                {
                    Console.WriteLine("Cell Number {0} : This is not the odds link", cellcount);
                }
                cellcount++;
            }
        }

        public static void OpenMarket()
        {
            //Open Main markets
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("button2" + eventid)));
            driver.FindElement(By.Id("button2" + eventid)).ClickOnIt("Pause Button");
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("openCurrentPageMlButton")));
            driver.FindElement(By.Id("openCurrentPageMlButton")).ClickOnIt("Open Markettline");
            Thread.Sleep(3000);
        }

        public static void KeepEvent()
        {
            
            // wait for action button
            wait.Until(ExpectedConditions.ElementExists(By.Id("button2" + eventid)));
            IWebElement pauseButton = driver.FindElement(By.Id("button2" + eventid));
            IAlert alert;

            if (driver.FindElement(By.Id("amlc_"+eventid)).Displayed) //football
            {
                // Pause
                pauseButton.ClickOnIt("Status would be Pasue");
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//*[@id='excla"+eventid+"'][contains(@style, 'display: inline-block')]")));
                Console.WriteLine("Marketline Status : Open to Pause");
                Thread.Sleep(2000);
     
                // click on pause again to perform close
                pauseButton.ClickOnIt("Pause Button");
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.XPath("//*[@id='button2" + eventid + "'][contains(@class, 'btn_refresh btn_refresh_black')]")));
                driver.FindElement(By.Id("closeCurrentPageMlButton")).ClickOnIt("Close Markettline");


                
                // Sent for resulting
                driver.FindElement(By.Id("pbet" + eventid)).ClickOnIt("Statistic Icon");
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("mkt_stats2")));
                driver.FindElement(By.Id("mkt_stats2")).ClickOnIt("Resulting Icon");
                wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("SendAllForResulting")));
                driver.FindElement(By.Id("SendAllForResulting")).ClickOnIt("Send Resulting (Event)");

                // 1st Alert to confrim doing resulting 
                wait.Until(ExpectedConditions.AlertIsPresent());
                alert = driver.SwitchTo().Alert();
                alert.Accept();

                // 2nd Alert to inform resulting is finished
                wait.Until(ExpectedConditions.AlertIsPresent());
                alert.Accept();

            }
            else //non football
            {
                
            }


            Thread.Sleep(2000);
        }

        public static void ResultAndSettleEvent(String eventid)
        {
            IAlert alert;
            var settlementPage = new SettlementPage(driver, eventid);
            DateTime eventDayFrom = DateTime.Now.AddDays(-2);


            #region (1) Go to Result Process & Settlemenet Page
            driver.FindElement(By.LinkText("Result Process & Settlement")).ClickOnIt("Result Process & Settlement");
            if (ElementVerify.Wait(driver, By.LinkText("Result Process")) != null)
                driver.FindElement(By.LinkText("Result Process")).MouseOver(driver, "Result Process");
            if (ElementVerify.Wait(driver, By.XPath("//a[contains(@href,'/DSA/ResultProcessing/ResultProcessing.aspx')]")) != null)
            {
                driver.FindElement(By.XPath("//a[contains(@href,'/DSA/ResultProcessing/ResultProcessing.aspx')]")).MouseOver(driver, "Sub Result Process");
                driver.FindElement(By.XPath("//a[contains(@href,'/DSA/ResultProcessing/ResultProcessing.aspx')]")).ClickOnIt("Result Process Page");
            }
            #endregion

            #region (2) Event ID
            if (ElementVerify.Wait(driver, settlementPage.EventId) != null)
                driver.FindElement(settlementPage.EventId).EnterText("Event ID", eventid);
            #endregion

            #region (3) Pick Event Date From
            if (ElementVerify.Wait(driver, By.Id("txtFromDate")) != null)
            {
                //Click on calendar icon
                settlementPage.StartDate.ClickOnIt("Event Date From");

                //Find the calendar
                ElementVerify.Wait(driver, By.ClassName("ui-datepicker-calendar"));
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
            if (ElementVerify.Wait(driver, By.Id("btnSubmit")) != null)
                settlementPage.SearchButton.ClickOnIt("Search");
            #endregion

            #region (5) Find number of market actions item
            // Wait until loading complete
            ElementVerify.Wait(driver, settlementPage.LoadingBar);
            // Wait for the event
            ElementVerify.Wait(driver, settlementPage.RefreshButton);
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
                    ElementVerify.Wait(driver, settlementPage.ActionDropdown);
                    driver.FindElement(By.LinkText("Enter Result")).ClickOnIt("Enter Result");
                    ElementVerify.Wait(driver, settlementPage.ResultPopup);

                    //Key Result for scoreline
                    if (driver.FindElement(settlementPage.ScorelineResult).Displayed)
                    {
                        ElementVerify.Wait(driver, settlementPage.ScoreHome);
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
                        ElementVerify.Wait(driver, By.XPath("//*[@id='" + marketlist[i] + "']/span[2]/ul[2]/li/table[2]/tbody/tr/td[2]/select"));
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
            ElementVerify.Wait(driver, settlementPage.CheckAll);
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
        }

        public static void CheckReport(string BetNo) 
        {
            DateTime eventDayFrom = DateTime.Now.AddDays(-2);

            #region (1) Access Wager Enquiry
            var gmmMenu = new GMMMenu(driver);
            driver.FindElement(gmmMenu.Report).ClickOnIt("Report");
            ElementVerify.Wait(driver, gmmMenu.WagerEnquiry);
            driver.FindElement(gmmMenu.WagerEnquiry).ClickOnIt("Wager Enquiry");
            #endregion

            #region (2) Switch to iframe and Select Wager No.
            //Switch to iframe
            driver.SwitchTo().Frame(driver.FindElement(By.TagName("iframe")));
            //Select Wager No.
            var wagerEnquiry = new WagerEnquiry(driver);
            ElementVerify.Wait(driver, wagerEnquiry.UserWagerOption);
            driver.FindElement(wagerEnquiry.UserWagerOption).ClickOnIt("User Code or Wager No.");
            driver.FindElement(wagerEnquiry.UserWagerOption).SelectByText("select wager no", "Wager No.");
            #endregion

            #region (3) Pick Event Date From
            if (ElementVerify.Wait(driver, wagerEnquiry.StartDate) != null)
            {
                //Click on calendar icon
                driver.FindElement(wagerEnquiry.StartDate).ClickOnIt("Event Date From");

                //Find the calendar
                ElementVerify.Wait(driver, wagerEnquiry.CalendarPicker);
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
                driver.FindElement(wagerEnquiry.UserWagerValue).EnterText("Wager No.", wager);
                driver.FindElement(wagerEnquiry.SearchButton).ClickOnIt("Search");
                
                // Wait for data displayed, then fetch wager number
                if (ElementVerify.Wait(driver, wagerEnquiry.WagerToVerify) == null)
                    continue;
               
                if (wager == driver.FindElement(wagerEnquiry.WagerToVerify).Text)
                    WriteConsole.Green("Wager Verified");
                else
                    WriteConsole.DarkRed("Unable to find the wager");
            }

            #endregion

        }

    }

    public enum TheEnvironment
    {
        QAT,
        UAT
    }

}
