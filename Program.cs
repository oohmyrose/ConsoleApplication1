using ConsoleApplication1.TestCases;
using System;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                GMM.Login(Enviromment.QAT);
                GMM.CreateEvent();    
                string eventid = GMM.GoToMMPage().Trim();
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

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Automation has succeeded");
                Console.ResetColor();

            }
            catch (Exception e)
            {
                GMM.KeepEvent();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Automation has failed");
                Console.ResetColor();
            }
        }
                 
    }


}


