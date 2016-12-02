using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace ConsoleApplication1.PageObjects
{
    public class GMMLogin
    {   
        // Elements
        [FindsBy(How = How.Id, Using = "SiteLogin_UserName")]
        public IWebElement UserName { get; set; }

        [FindsBy(How = How.Id, Using = "SiteLogin_Password")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "SiteLogin_LoginButton")]
        public IWebElement Submit { get; set; }

        public By LoginErrorMessage  = By.Id("errormessage");
        public By SiteMenuAfterLogin = By.Id("WebSiteMapMenu");




        public GMMLogin(IWebDriver driver) {
            PageFactory.InitElements(driver, this);
        }
    }

    public class GMMMenu
    {
        [FindsBy(How = How.LinkText, Using = "Sportsbook")]
        public IWebElement Sportsbook { get; set; }

        [FindsBy(How = How.LinkText, Using = "Event")]
        public IWebElement Event { get; set; }

        [FindsBy(How = How.LinkText, Using = "Fixture Event")]
        public IWebElement FixtureEvent { get; set; }

        public By Report       = By.LinkText("Report");
        public By WagerEnquiry = By.LinkText("Wager Enquiry");
        

        public GMMMenu(IWebDriver driver) {
            PageFactory.InitElements(driver, this);
        }
 
    }

    public class GMMCreateEvent
    {
        [FindsBy(How = How.LinkText, Using = "Create")]
        public IWebElement Create { get; set; }

        public By ParentEventId    = By.Id("mainContent_EventId");

        public GMMCreateEvent(IWebDriver driver) {
            PageFactory.InitElements(driver, this);
        }
    }

    public class SettlementPage
    {
      
        public By EventId        = By.Id("txtEventId");

        [FindsBy(How = How.Id, Using = "txtFromDate")]
        public IWebElement StartDate { get; set; }

        [FindsBy(How = How.ClassName, Using = "ui-datepicker-calendar")]
        public IWebElement CalendarPicker { get; set; }

        [FindsBy(How = How.Id, Using = "btnSubmit")]
        public IWebElement SearchButton { get; set; }

        public By LoadingBar      = By.XPath("//*[@class='ajax-loading-indicator'][contains(@style,'display: none')]");
        public By RefreshButton   = By.XPath("//input[@class='btn_refresh_icon refresh_roll btn-refresh-event']");
        public By MarketLine      = By.XPath("//div[contains(@id,'result_status_')]");
        public By ResultStatus    = By.ClassName("fr status-name");
        public By ActionButton    = By.XPath("//div[contains(@id ,'result_status_']/span[2]/a");
        public By ActionDropdown  = By.XPath("//*[@class='ui-dropdown-content action-content view'][contains(@style,'display: block')]");
        
        public By ResultPopup     = By.XPath("//*[@class='ui-dropdown-content popup-content view'][contains(@style,'display: block')]");
        public By ScorelineResult = By.ClassName("mode-of-scoreline");
        public By SelectionResult = By.ClassName("mode-of-selection");
        public By VoidResult      = By.ClassName("void-reason");
        public By ScoreHome       = By.XPath("//input[@class='score-home']");
        public By ScoreAway       = By.XPath("//input[@class='score-away']");
        public By WinSelection    = By.ClassName("selected-outcome");
        public By SaveButton      = By.XPath("//*[@class='ui-dropdown-content popup-content view'][contains(@style,'display: block')]/li/table/tbody/tr[4]/td[3]/input");
        public By SaveButtonProp  = By.XPath("//*[@class='ui-dropdown-content popup-content view'][contains(@style,'display: block')]/li/table[2]/tfoot/tr/td/input[2]");
        public By CheckAll        = By.XPath("//input[@class='checkbox-check-all']");
        public By SettleButton    = By.Id("btnSettleAll");
        public By UnSettleButton  = By.Id("btnUnSettleAll");

        public SettlementPage(IWebDriver driver,string eventid)
        {
            PageFactory.InitElements(driver, this);
        }
 
    }

    public class WagerEnquiry
    {
        public By BUCode          = By.Id("ddlBuList");
        public By UserWagerOption = By.Id("ddlUsercodeWagerno");
        public By UserWagerValue  = By.Id("ddlUsercodeWagerno_value");
        public By StartDate       = By.Id("datepicker1");
        public By CalendarPicker  = By.Id("ui-datepicker-div");
        public By SearchButton    = By.Id("button8");
        public By WagerToVerify   = By.XPath("//*[@id='AccountTime']/div[2]");

        public WagerEnquiry(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }
    }

}
