using ConsoleApplication1.TestCases;
using ConsoleApplication1.Extensions;
using System;
using System.Windows.Input;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //TheEnvironment envi;

            for (int i = 0; i < args.Length; i++)
            {
                try
                {
                    //Enum.TryParse(args[i], out envi);
                    //GMM.Login(envi);
                    GMM.Login(TheEnvironment.UAT);
                    string eventid = GMM.CreateEvent();
                    GMM.GoToMMPage(eventid);
                    GMM.UpdateOdds(eventid);
                    GMM.OpenMarket(eventid);


                    //TouTou.MemberSite.Login();
                    //string BetNo = TouTou.MemberSite.PlaceBet(eventid).Trim();


                    //TTBO.TTBOLogin();
                    //TTBO.FindMember();
                    //TTBO.FindWager(BetNo);

                    GMM.KeepEvent(eventid);
                    GMM.ResultAndSettleEvent(eventid);

                    //GMM.CheckReport(BetNo);

                    //WL.Main(TheEnvironment.UAT);
                }
                catch (Exception e)
                {
                    WriteConsole.Red(String.Format("Exception has happened : {0} ", e.Message.ToString()));

                    switch (e.Message)
                    {
                        case "GMM Login Failed":
                            Report.Log(LogStatus.Fail, "Login GMM", e.Message);   
                            break;
                        case "GMM CreateEvent Failed":
                            break;
                        case "GMM GoToMMPage Failed":
                            break;
                        case "GMM UpdateOdds Failed":
                            break;
                        case "GMM OpenMarket Failed":
                            break;
                        case "GMM KeepEvent Failed":
                            break;
                        case "GMM ResultAndSettleEvent Failed":
                            break;
                        case "GMM CheckReport Failed":
                            break;
                        case "Toutou Login Failed":
                            break;
                        case "Toutou PlaceBet Failed":
                            break;
                        case "TTBO Login Failed":
                            break;
                        case "TTBO FindMember Failed":
                            break;
                        case "TTBO FindWager Failed":
                            break;
                        default:
                            break;
                    }
                }
                finally
                {
                    WriteConsole.Yellow("Test Automation Completed");              
                    Report.EndAutomation();
                }
            }
 
        
        
        }         
    }
}


