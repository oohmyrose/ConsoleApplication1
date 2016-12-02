using ConsoleApplication1.TestCases;
using ConsoleApplication1.Extensions;
using System;
using System.Windows.Input;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            TheEnvironment envi;

            for (int i = 0; i < args.Length; i++)
            {
                try
                {
                    Enum.TryParse(args[i], out envi);
                    GMM.Login(envi);
                    string eventid = GMM.CreateEvent();
                    GMM.GoToMMPage(eventid);
                    GMM.UpdateOdds();
                    GMM.OpenMarket();


                    TouTou.MemberSite.Login();
                    string BetNo = TouTou.MemberSite.PlaceBet(eventid).Trim();


                    TTBO.TTBOLogin();
                    TTBO.FindMember();
                    TTBO.FindWager(BetNo);

                    GMM.KeepEvent();
                    GMM.ResultAndSettleEvent(eventid);
                    GMM.CheckReport(BetNo);


                    WriteConsole.Green("Automation has succeeded");
                }
                catch (Exception e)
                {
                    WriteConsole.Red(String.Format("Automation has failed : {0} ", e.Message.ToString()));

                    switch (e.Message)
                    {
                        case "GMM Login Failed":
                            // Excute other test cases       
                            break;
                        case "GMM CreateEvent Failed":
                            // 
                            break;
                        default:
                            break;
                    }
                }
                finally
                {
                    WriteConsole.Yellow("Test Automation Completed");
                }
            }
 
        
        
        }         
    }
}


