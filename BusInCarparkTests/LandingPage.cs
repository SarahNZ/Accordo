using System;
//using OpenQA.Selenium.dotNetSeleniumExtras
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace BusInCarparkTests {
    public class LandingPage
    {

        private static LandingPage _instance;
        
        // Create a new instance of Chrome Driver
        private IWebDriver _driver = new ChromeDriver();

        // URL of application's landing page
        private const string LandingPageUrl = "https://accordo-it.github.io/carpark/";

        // Locators
        public const string XCoordinateSelectControlLocator = "#positionX";
        public const string YCoordinateSelectControlLocator = "#positionY";
        public const string FacingControlLocator = "#face .custom-select";
        public const string PlaceBusButton = "btn-block";
        public const string MoveButtonLocator = ".btn-group #move";
        public const string LeftButton = "rotate-left";
        public const string RightButton = "rotate-right";
        public const string ReportButton = "report";
        public const string Carpark = "park";

        // Locators of co-ordinates of the bus in the carpark (where pos-0-0 is the south-western most cell of the carpark)
        public const string CoordinateX0Y0Locator = "pos-0-0";
        public const string CoordinateX0Y1Locator = "pos-0-1";
        public const string CoordinateX0Y2Locator = "pos-0-2";
        public const string CoordinateX0Y3Locator = "pos-0-3";
        public const string CoordinateX0Y4Locator = "pos-0-4";

        // Direction the bus is facing. This dictates which way the bus will move in.
        public const string North = "face-north";
        public const string South = "face-south";
        public const string East = "face-east";
        public const string West = "face-west";

        public static LandingPage GetInstance()
        {
            if (_instance == null)
                _instance = new LandingPage();
            return _instance;
        }

        /// <summary>
        /// This method loads the landing page and makes sure the page is loaded.
        /// </summary>
        /// <returns>An instance of the Chrome Driver</returns>
        public IWebDriver LoadPage() {
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(LandingPageUrl);
            _driver.FindElement(By.ClassName(Carpark));
            return _driver;
        }

        public void RefreshPage()
        {
            _driver.Navigate().Refresh();
        }

        // Click on the Place Bus button to place the bus on a grid co-ordinate in the carpark, then check that it has been placed at the correct co-ordinate
        public void ClickPlaceBusButton(string coordinates, string direction)
        {
            _driver.FindElement(By.ClassName(PlaceBusButton)).Click();
            Assert.IsTrue(_driver.FindElement(By.ClassName(coordinates)).Displayed,"The bus has been placed at the wrong co-ordinates in the carpark. It should have been placed at co-ordinate "+ coordinates + ".");
        }

        public void SelectXAndYCoordinates(string x, string y)
        {
            //Select the X Co-ordinate drop-down list
            var xCoordinateControl = _driver.FindElement(By.CssSelector(XCoordinateSelectControlLocator));

            // Create select element object for X Co-ordinate
            var selectElementX = new SelectElement(xCoordinateControl);

            // Select X Co-ordinate by value
            selectElementX.SelectByValue("0");

            //Select the Y Coordinate drop-down list
            var yCoordinateControl = _driver.FindElement(By.CssSelector(YCoordinateSelectControlLocator));

            // Create select element object for Y Co-ordinate
            var selectElementY = new SelectElement(yCoordinateControl);

            // Select Y Co-ordinate by value 
            selectElementY.SelectByValue("1");
        }

        public void Move()
        {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(1000)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .VisibilityOfAllElementsLocatedBy(By.Id("move")));
            _driver.FindElement(By.CssSelector(MoveButtonLocator)).Click();

        }

        public void Report()
        {
            Assert.IsTrue(_driver.FindElement(By.Id(ReportButton)).Displayed);
        }


    }
}
