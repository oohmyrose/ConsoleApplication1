using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.PageObjects
{
    public class WLLogin
    {
        public By UserName    = By.Id("LoginName");
        public By Password    = By.Id("Password");
        public By LoginButton = By.Id("btn-login");
        public By Balance = By.Id("balance");

        public By QATUserName = By.XPath("//input[@name='t']");
        //public By QATLoginButton = By.XPath("/html/body/div/form/input[3]");

        public WLLogin(IWebDriver driver) {
            PageFactory.InitElements(driver, this);
        }
    }
    public class WLFrame
    {
        public By IFrame      = By.Id("main-frame");
        public By QATIFrame   = By.TagName("iframe");
        public By LeftFrame   = By.Id("NavigatorFrame");
        public By RightFrame  = By.Id("MainFrame");

        public WLFrame(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }
    }
    public class WLLeft
    {

        public By InplayTab         = By.Id("title-inplay");
        public By InplayCheckboxAll = By.XPath("//*[@id='lst-inplay']/ul/li[1]");
        

        public By TodayTab          = By.Id("title-today");
        public By Today1stAHOU      = By.XPath("//*[@id='lst-today']/ul/li[1]/ul/ul/li[1]");

        public By TomorrowTab       = By.Id("title-early");
        public By Tomorrow1stAHOU   = By.XPath("//*[@id='lst-early']/ul/li[1]/ul/ul/li[1]");

        public By BetSlipStake      = By.Id("stakeTxt_0");
        public By PlaceBetButton    = By.Id("placeBetBtn");
        public By BetConfirm        = By.Id("cfm-bet-info");

        public By WagerNumber       = By.ClassName("bet-no");

        public WLLeft(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }
    }
    public class WLRight
    {

        public WLRight(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }
    }
}
