using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace ConsoleApplication1.Extensions
{
    public static class ElementVerify
    {
        // Will wait for seconds by default 
        public static IWebElement Wait(this IWebDriver driver, By by, int seconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            IWebElement element;

            try
            {
                wait.Until(ExpectedConditions.ElementExists(by));
                element = driver.FindElement(by);
                WriteConsole.DarkCyan(String.Format("Element found --> {0}", by.ToString()));
                return element;
            }
            catch (OpenQA.Selenium.NoSuchElementException e)
            {
                WriteConsole.DarkMagenta(String.Format("NoSuchElementException of Wait : {0}", e.Message.ToString()));
                return null;
            }
            catch (OpenQA.Selenium.NotFoundException e)
            {
                WriteConsole.DarkMagenta(String.Format("NotFoundException of Wait : {0}", e.Message.ToString()));
                return null;
            }
            catch (System.TimeoutException e)
            {
                WriteConsole.DarkMagenta(String.Format("TimeoutException of Wait : {0}", e.Message.ToString()));
                return null;
            }
            catch (Exception e)
            {
                WriteConsole.DarkMagenta(String.Format("Exception of Wait : {0}", e.Message.ToString()));
                return null;
            }

        }
 
    }

    public static class ElementExtensions
    {


        public static void EnterText    (this IWebElement element, string elementName, string text)
        {
            element.Clear();
            element.SendKeys(text);
            WriteConsole.Cyan(String.Format("[{0}] : key in [{1}]", elementName, text));
        }

        public static void MouseOver    (this IWebElement element, IWebDriver driver, string elementName)
        {
            Actions action = new Actions(driver);

            //Hover Element
            action.MoveToElement(element).Perform();
            WriteConsole.Cyan(String.Format("[{0}] : Hovered", elementName));  
        }

        public static void MouseOverThenClick(this IWebElement element, IWebDriver driver, string elementName)
        {
            Actions action = new Actions(driver);

            //Hover Element
            action.MoveToElement(element).Click().Perform();     //action.MoveToElement(element).Click().Perform(); //hover then click
            WriteConsole.Cyan(String.Format("[{0}] : Hovered then click", elementName));
        }

        public static bool IsDisplayed  (this IWebElement element, string elementName)
        {
            bool result;
            try
            {
                result = element.Displayed;
                WriteConsole.Cyan(String.Format("[{0}] : Displayed", elementName));
            }
            catch(Exception)
            {
                result = false;
                WriteConsole.Cyan(String.Format("[{0}] : not Displayed", elementName));
            }
 
            return result;            
        }

        public static void ClickOnIt    (this IWebElement element, string elementName)
        {
            element.Click();
            WriteConsole.Cyan(String.Format("[{0}] : clicked", elementName)); 
        }

        public static void SelectByText (this IWebElement element, string elementName, string text)
        {
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByText(text);
            WriteConsole.Cyan(String.Format("[{0}] : select [{1}]", elementName, text));
        }

        public static void SelectByIndex(this IWebElement element, string elementName, int index)
        {
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByIndex(index);
            WriteConsole.Cyan(String.Format("{0} : select [{1}]", elementName, index));
        }

        public static void SelectByValue(this IWebElement element, string elementName, string text)
        {
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByValue(text);
            WriteConsole.Cyan(String.Format("[{0}] : select [{1}]", elementName, text));
        }
 
    }

    

}

