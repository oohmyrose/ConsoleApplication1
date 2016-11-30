using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1.TestCases
{
    public static class iOWB
    {
        public static WebDriverWait wait = new WebDriverWait(new ChromeDriver(), TimeSpan.FromSeconds(60));

        private static void LoginAndSearchWager(IWebDriver cm, string URL, string iowbaccount, string password, string account, string StakeAmout)
        {
            //login iOWB
            cm.Navigate().GoToUrl("http://iowb.qat/");
            cm.FindElement(By.Id("UserCode")).Clear();
            cm.FindElement(By.Id("UserCode")).SendKeys(iowbaccount);
            cm.FindElement(By.Id("btnLogin")).Click();
            cm.FindElement(By.Id("Password")).Clear();
            cm.FindElement(By.Id("Password")).SendKeys(password);
            cm.FindElement(By.Id("btnLogin")).Click();

            //member manegement
            new WebDriverWait(cm, TimeSpan.FromSeconds(45)).Until(ExpectedConditions.ElementExists((By.XPath("//div/div/ul/li/a/span"))));
            cm.FindElement(By.XPath("//div/div/ul/li/a/span")).Click();
            new WebDriverWait(cm, TimeSpan.FromSeconds(45)).Until(ExpectedConditions.ElementExists((By.LinkText("Member Listing"))));
            cm.FindElement(By.LinkText("Member Listing")).Click();


            //search member code
            Thread.Sleep(5000);
            cm.FindElement(By.Id("DateCreatedStart")).Click();
            cm.FindElement(By.Id("DateCreatedStart")).Clear();
            cm.FindElement(By.Id("DateCreatedStart")).SendKeys("11/11/1997 00:00");
            Thread.Sleep(3000);
            cm.FindElement(By.XPath("//*[@id='ui-datepicker-div']/div[3]/button[2]")).Click();
            Thread.Sleep(1000);
            cm.FindElement(By.Id("UserCode")).Clear();
            cm.FindElement(By.Id("UserCode")).SendKeys(account);
            cm.FindElement(By.Id("btnSearch")).Click();
            Thread.Sleep(2000);
            cm.FindElement(By.LinkText(account)).Click();

            //SBK Statement tab
            Thread.Sleep(5000);
            cm.FindElement(By.LinkText("SBK Statement")).Click();
            Thread.Sleep(5000);
            cm.FindElement(By.Id("txtSBSFromDate")).Click();
            cm.FindElement(By.Id("txtSBSFromDate")).Clear();
            cm.FindElement(By.Id("txtSBSFromDate")).SendKeys("11/11/2015 00:00");
            cm.FindElement(By.Id("btnSBSSearch")).Click();
            Thread.Sleep(5000);

            //verify stake amount
            String NewStake = cm.FindElement(By.XPath("//*[@id='SBStatementListTable']/tbody/tr[1]/td[10]")).Text.Substring(5);

            if (NewStake == StakeAmout)
            {
                Console.WriteLine("Stake = " + NewStake + "--Result: Pass");
            }
            else
            {
                Console.WriteLine("Stake = " + NewStake + "--Result: Fail");
            }
            cm.Quit();
        }
    }
}
