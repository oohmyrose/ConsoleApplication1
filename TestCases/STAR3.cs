using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.TestCases
{
    public static class STAR3
    {

        public static WebDriverWait wait = new WebDriverWait(new ChromeDriver(), TimeSpan.FromSeconds(60));

        public static void Login(IWebDriver cm, string URL, string account, string password)
        {
            //login 188BET
            cm.Navigate().GoToUrl(URL);
            cm.FindElement(By.Name("username")).Clear();
            cm.FindElement(By.Name("username")).SendKeys(account);
            cm.FindElement(By.Name("password")).Clear();
            cm.FindElement(By.Name("password")).SendKeys(password);
            cm.FindElement(By.XPath("//button[@type='submit']")).Click();
            new WebDriverWait(cm, TimeSpan.FromSeconds(45)).Until(ExpectedConditions.ElementExists((By.Id("toggle-banking"))));
        }

        public static void PlaceBet(IWebDriver cm, string StakeAmout)
        {
            //add selection
            cm.SwitchTo().Frame(cm.FindElement(By.ClassName("rsiframe")));
            new WebDriverWait(cm, TimeSpan.FromSeconds(45)).Until(ExpectedConditions.ElementExists((By.ClassName("odds"))));
            cm.FindElement(By.ClassName("odds")).Click();

            //bet slip
            new WebDriverWait(cm, TimeSpan.FromSeconds(45)).Until(ExpectedConditions.ElementExists((By.XPath("//input[contains(@id,'stake')]"))));
            cm.FindElement(By.XPath("//input[contains(@id,'stake')]")).Click();
            cm.FindElement(By.XPath("//input[contains(@id,'stake')]")).SendKeys(StakeAmout);

            //Confirm Bet
            new WebDriverWait(cm, TimeSpan.FromSeconds(45)).Until(ExpectedConditions.ElementExists((By.Id("btnPlaceBet_BS"))));
            cm.FindElement(By.Id("btnPlaceBet_BS")).Click();
            new WebDriverWait(cm, TimeSpan.FromSeconds(45)).Until(ExpectedConditions.ElementExists((By.Id("btnConfirm_BS"))));
            cm.FindElement(By.Id("btnConfirm_BS")).Click();
            new WebDriverWait(cm, TimeSpan.FromSeconds(45)).Until(ExpectedConditions.ElementExists((By.Id("bsGeneralMsg"))));
            Console.WriteLine("Place bet successfully");
            cm.SwitchTo().DefaultContent();
            cm.Quit();
        }
    }
}
