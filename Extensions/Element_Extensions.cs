using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using RelevantCodes.ExtentReports;

namespace ConsoleApplication1.Extensions
{
    public static class ElementVerify
    {
        // Will wait for seconds by default 
        public static IWebElement Exist(this IWebDriver driver, By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            IWebElement element;

            try
            {
                wait.Until(ExpectedConditions.ElementExists(by));
                element = driver.FindElement(by);
                WriteConsole.DarkCyan(String.Format("Element found --> {0}", by.ToString()));
                Report.Log(LogStatus.Pass, "Check Element Exist", String.Format("Element found --> {0}", by.ToString()));
                return element;
            }
            catch (Exception e)
            {
                WriteConsole.DarkMagenta(String.Format("Exception of Wait : {0} [{1}]", by.ToString(), e.Message.ToString()));
                Report.Log(LogStatus.Warning, "Check Element Exist", String.Format("Exception of Wait : {0} [{1}]", by.ToString(), e.Message.ToString()));
                return null;
            }

        }
        public static IWebElement Exist(this IWebDriver driver, By by, int seconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            IWebElement element;

            try
            {
                wait.Until(ExpectedConditions.ElementExists(by));
                element = driver.FindElement(by);
                WriteConsole.DarkCyan(String.Format("Element found --> {0}", by.ToString()));
                Report.Log(LogStatus.Pass, "Check Element Exist", String.Format("Element found --> {0}", by.ToString()));
                return element;
            }
            catch (Exception e)
            {
                WriteConsole.DarkMagenta(String.Format("Exception of Wait : {0} [{1}]", by.ToString(), e.Message.ToString()));
                Report.Log(LogStatus.Warning, "Check Element Exist", String.Format("Exception of Wait : {0} [{1}]", by.ToString(), e.Message.ToString()));
                return null;
            }

        }
        public static IWebElement Clickable(this IWebDriver driver, By by, int seconds = 30)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            IWebElement element;

            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(by));
                element = driver.FindElement(by);
                WriteConsole.DarkCyan(String.Format("Element found --> {0}", by.ToString()));
                Report.Log(LogStatus.Pass, "Check Element Clickable", String.Format("Element found --> {0}", by.ToString()));
                return element;
            }
            catch (Exception e)
            {
                WriteConsole.DarkMagenta(String.Format("Exception of checking Clickable : {0} [{1}]", by.ToString(), e.Message.ToString()));
                Report.Log(LogStatus.Warning, "Check Element Clickable", String.Format("Exception of Wait : {0} [{1}]", by.ToString(), e.Message.ToString()));
                return null;
            }

        }
        public static IWebElement Visable(this IWebDriver driver, By by, int seconds = 20)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            IWebElement element;

            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(by));
                element = driver.FindElement(by);
                WriteConsole.DarkCyan(String.Format("Element found --> {0}", by.ToString()));
                Report.Log(LogStatus.Pass, "Check Element Visable", String.Format("Element found --> {0}", by.ToString()));
                return element;
            }
            catch (Exception e)
            {
                WriteConsole.DarkMagenta(String.Format("Exception of checking visable : {0} [{1}]", by.ToString(), e.Message.ToString()));
                Report.Log(LogStatus.Warning, "Check Element Visable", String.Format("Exception of Wait : {0} [{1}]", by.ToString(), e.Message.ToString()));
                return null;
            }
        }
        public static IWebElement PresenceAll(this IWebDriver driver, By by, int seconds = 20)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            IWebElement element;

            try
            {
                wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
                element = driver.FindElement(by);
                WriteConsole.DarkCyan(String.Format("Element found --> {0}", by.ToString()));
                Report.Log(LogStatus.Pass, "Check Element PresenceAll", String.Format("Element found --> {0}", by.ToString()));
                return element;
            }
            catch (Exception e)
            {
                WriteConsole.DarkMagenta(String.Format("Exception of checking visable : {0} [{1}]", by.ToString(), e.Message.ToString()));
                Report.Log(LogStatus.Warning, "Check Element PresenceAll", String.Format("Exception of Wait : {0} [{1}]", by.ToString(), e.Message.ToString()));
                return null;
            }
        }
        public static bool Alert(this IWebDriver driver, int seconds = 20)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
           
            try
            {
                wait.Until(ExpectedConditions.AlertIsPresent());
                //element = driver.FindElement(by);
                WriteConsole.DarkCyan(String.Format("Element found --> {0}"));
                Report.Log(LogStatus.Pass, "Check Alert Box", String.Format("Element found --> {0}"));  
                return true;
            }
            catch (Exception e)
            {
                WriteConsole.DarkMagenta(String.Format("Exception of checking visable : {0} [{1}]", "alert" , e.Message.ToString()));
                Report.Log(LogStatus.Warning, "Check Alert Box", String.Format("Exception of checking visable : {0} [{1}]", "alert", e.Message.ToString()));
                return false;
            }
        }
    }

    public static class ElementExtensions
    {
        public static void EnterText         (this IWebElement element, string elementName, string text)
        {
            if (elementName == null)
                Report.Log(LogStatus.Fail, "EnterText", String.Format("[{0}] : key in [{1}]", elementName, text));
            else
            {
                element.Clear();
                element.SendKeys(text);
                WriteConsole.Cyan(String.Format("[{0}] : key in [{1}]", elementName, text));
                Report.Log(LogStatus.Pass, "EnterText", String.Format("[{0}] : key in [{1}]", elementName, text));
            }
        }
        public static void MouseOver         (this IWebElement element, IWebDriver driver, string elementName)
        {
            if (elementName == null)
                Report.Log(LogStatus.Fail, "MouseOver", String.Format("[{0}] : Hovered", elementName));
            else
            {
                Actions action = new Actions(driver);
                //Hover Element
                action.MoveToElement(element).Perform();
                WriteConsole.Cyan(String.Format("[{0}] : Hovered", elementName));
                Report.Log(LogStatus.Pass, "MouseOver", String.Format("[{0}] : Hovered", elementName));
            }
        }
        public static void MouseOverThenClick(this IWebElement element, IWebDriver driver, string elementName)
        {
            if (elementName == null)
                Report.Log(LogStatus.Fail, "MouseOverThenClick", String.Format("[{0}] : Hovered then click", elementName));
            else
            {
                Actions action = new Actions(driver);
                //Hover Element
                action.MoveToElement(element).Click().Perform();     //action.MoveToElement(element).Click().Perform(); //hover then click
                WriteConsole.Cyan(String.Format("[{0}] : Hovered then click", elementName));
                Report.Log(LogStatus.Pass, "MouseOverThenClick", String.Format("[{0}] : Hovered then click", elementName));
            }
        }
        public static bool IsDisplayed       (this IWebElement element, string elementName)
        {
            bool result;
            try
            {
                result = element.Displayed;
                WriteConsole.Cyan(String.Format("[{0}] : Displayed", elementName));
                Report.Log(LogStatus.Pass, "IsDisplayed", String.Format("[{0}] : Displayed", elementName));
            }
            catch(Exception)
            {
                result = false;
                WriteConsole.Cyan(String.Format("[{0}] : not Displayed", elementName));
                Report.Log(LogStatus.Fail, "IsDisplayed", String.Format("[{0}] : not Displayed", elementName));
            }
 
            return result;            
        }
        public static void ClickOnIt         (this IWebElement element, string elementName)
        {
            if (elementName == null)
                Report.Log(LogStatus.Fail, "Click", String.Format("[{0}] : clicked", elementName));
            else
            {
                element.Click();
                WriteConsole.Cyan(String.Format("[{0}] : clicked", elementName));
                Report.Log(LogStatus.Pass, "Click", String.Format("[{0}] : clicked", elementName));
            }
        }
        public static void SelectByText      (this IWebElement element, string elementName, string text)
        {
            if (elementName == null)
                Report.Log(LogStatus.Fail, "Select Dropdown", String.Format("[{0}] : select [{1}]", elementName, text));
            else
            {
                SelectElement oSelect = new SelectElement(element);
                oSelect.SelectByText(text);
                WriteConsole.Cyan(String.Format("[{0}] : select [{1}]", elementName, text));
                Report.Log(LogStatus.Pass, "Select Dropdown", String.Format("[{0}] : select [{1}]", elementName, text));
            }
        }
        public static void SelectByIndex     (this IWebElement element, string elementName, int index)
        {
            if (elementName == null)
                Report.Log(LogStatus.Fail, "Select Dropdown", String.Format("[{0}] : select [{1}]", elementName, index));
            else
            {
                SelectElement oSelect = new SelectElement(element);
                oSelect.SelectByIndex(index);
                WriteConsole.Cyan(String.Format("{0} : select [{1}]", elementName, index));
                Report.Log(LogStatus.Pass, "Select Dropdown", String.Format("[{0}] : select [{1}]", elementName, index));
            }
        }
        public static void SelectByValue     (this IWebElement element, string elementName, string text)
        {
            if (elementName == null)
                Report.Log(LogStatus.Fail, "Select Dropdown", String.Format("[{0}] : select [{1}]", elementName, text));
            else
            {
                SelectElement oSelect = new SelectElement(element);
                oSelect.SelectByValue(text);
                WriteConsole.Cyan(String.Format("[{0}] : select [{1}]", elementName, text));
                Report.Log(LogStatus.Pass, "Select Dropdown", String.Format("[{0}] : select [{1}]", elementName, text));
            }
        }

    }

    

}

