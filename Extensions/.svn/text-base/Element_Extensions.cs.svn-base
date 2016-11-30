using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace ConsoleApplication1.Extensions
{
    public static class ElementVerify
    {
        public static IWebElement IsElementExists(this IWebDriver driver, By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            IWebElement element;


            try
            {
                wait.Until(ExpectedConditions.ElementExists(by));
                element = driver.FindElement(by);
                //wait.Until( dr => dr.FindElement(by));
                Console.WriteLine(String.Format("Element exists"));
                return element;
            }
            catch (OpenQA.Selenium.NoSuchElementException)
            {
                Console.WriteLine(String.Format("NoSuchElementException"));
                return null;
            }
            catch (OpenQA.Selenium.NotFoundException)
            {
                Console.WriteLine(String.Format("NotFoundException"));
                return null;
            }
            catch (System.TimeoutException)
            {
                Console.WriteLine(String.Format("TimeoutException"));
                return null;
            }
            catch (Exception)
            {
                Console.WriteLine(String.Format("Exception"));
                return null;
            }

        }
 
    }

    public static class ElementExtensions
    {

        public static void DoAction(Action action)
        {
            switch (action)
            {
                case Action.ClickOnIt:
                    break;

            }
               

        }

        public static void EnterText    (this IWebElement element, string elementName, string text)
        {
            element.Clear();
            //element.Click();
            element.SendKeys(text);
            //element.Click();
            Console.WriteLine(String.Format("[{0}] : key in [{1}]", elementName,text));
        }

        public static void MouseOver    (this IWebElement element, IWebDriver driver, string elementName)
        {
            Actions action = new Actions(driver);

            //Hover Element
            action.MoveToElement(element).Perform();     //action.MoveToElement(element).Click().Perform(); //hover then click
            Console.WriteLine(String.Format("[{0}] : Hovered", elementName));  
        }

        public static void MouseOverThenClick(this IWebElement element, IWebDriver driver, string elementName)
        {
            Actions action = new Actions(driver);

            //Hover Element
            action.MoveToElement(element).Click().Perform();     //action.MoveToElement(element).Click().Perform(); //hover then click
            Console.WriteLine(String.Format("[{0}] : Hovered then click", elementName));
        }

        public static bool IsDisplayed  (this IWebElement element, string elementName)
        {
            bool result;
            try
            {
                result = element.Displayed;
                Console.WriteLine(String.Format("[{0}] : Displayed",elementName));
            }
            catch(Exception)
            {
                result = false;
                Console.WriteLine(String.Format("[{0}] : not Displayed",elementName));
            }
 
            return result;            
        }

        public static void ClickOnIt    (this IWebElement element, string elementName)
        {
            element.Click();
            Console.WriteLine(String.Format("[{0}] : clicked", elementName)); 
        }

        public static void SelectByText (this IWebElement element, string elementName, string text)
        {
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByText(text);
            Console.WriteLine(String.Format("[{0}] : select [{1}]",elementName,text));
        }

        public static void SelectByIndex(this IWebElement element, string elementName, int index)
        {
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByIndex(index);
            Console.WriteLine(String.Format("{0} : select [{1}]",elementName,index));
        }

        public static void SelectByValue(this IWebElement element, string elementName, string text)
        {
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByValue(text);
            Console.WriteLine(String.Format("[{0}] : select [{1}]", elementName, text));
        }
 
    }

    public enum Action
    {
        EnterText,
        MouseOver,
        IsDisplayed,
        ClickOnIt,
        SelectByText,
        SelectByIndex,
        SelectByValue
    }
    

}

