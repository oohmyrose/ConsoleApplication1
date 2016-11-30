using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class ProgramM
    {
        static void Main(string[] args)
        {
            string fbURL = "http://new.star.avabet.com/en-gb/sports/football";
            string cashaccount = "mariermb";
            OpenChrome(fbURL, cashaccount, "123456a");
        }


        private static void OpenChrome(string URL, string acount, string password)
        {
            IWebDriver cm = new ChromeDriver();

            cm.Manage().Window.Maximize();
            cm.Navigate().GoToUrl(URL);
            cm.FindElement(By.Name("username")).Clear();
            cm.FindElement(By.Name("username")).SendKeys(acount);
            cm.FindElement(By.Name("password")).Clear();
            cm.FindElement(By.Name("password")).SendKeys(password);
            cm.FindElement(By.XPath("//button[@type='submit']")).Click();

            Thread.Sleep(3000);
            cm.SwitchTo().Frame(cm.FindElement(By.TagName("iframe")));
            
            Thread.Sleep(8000);
            cm.FindElement(By.ClassName("odds")).Click();

            Thread.Sleep(3000);
            cm.FindElement(By.XPath("//input[contains(@id,'stake')]")).Click();
            cm.FindElement(By.XPath("//input[contains(@id,'stake')]")).SendKeys("10");
            cm.FindElement(By.Id("btnPlaceBet_BS")).Click();
            cm.FindElement(By.Id("btnConfirm_BS")).Click();
            cm.FindElement(By.Id("btnOk_BS")).Click();
            cm.FindElement(By.Id("tabMyBet")).Click();
            cm.FindElement(By.XPath("//input[contains(@id,'stake')]")).Click();
            //cm.Quit();
        }
    }
}