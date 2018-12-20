using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace BusInCarparkTests.PageObjects
{
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(InternetExplorerDriver))]
    public class SinglePage<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private IWebDriver _driver;

        private static SinglePage<TWebDriver> _instance;

        // URL of application's landing page
        private const string LandingPageUrl = "https://accordo-it.github.io/carpark/";

        // Locators
        public const string XCoordinateSelectControlLocator = "#positionX";
        public const string YCoordinateSelectControlLocator = "#positionY";
        public const string DirectionControlLocator = "#face";
        public const string PlaceBusButton = "btn-block";
        public const string MoveButton = "move";
        public const string LeftButton = "rotate-left";
        public const string RightButton = "rotate-right";
        public const string ReportButton = "report";
        public const string Carpark = "park";

        // Locators of co-ordinates of the bus in the carpark (where pos-0-0 is the south-western most cell of the carpark)
        public const string CoordinateX0Y0Locator = "pos-0-0";

        public const string CoordinateX0Y1Locator = "pos-0-1";
        public const string CoordinateX1Y2Locator = "pos-1-2";
        public const string CoordinateX0Y4Locator = "pos-0-4";
        public const string CoordinateX4Y4Locator = "pos-4-4";
        public const string CoordinateX4Y0Locator = "pos-4-0";

        // Direction the bus is facing. This dictates which way the bus will move in.
        public const string North = "face-north";
        public const string South = "face-south";
        public const string East = "face-east";
        public const string West = "face-west";

        public static SinglePage<TWebDriver> GetInstance()
        {
            if (_instance == null)
                _instance = new SinglePage<TWebDriver>();
            return _instance;
        }

        public static SinglePage<TWebDriver> NewInstance()
        {
            _instance = new SinglePage<TWebDriver>();
            return _instance;
        }


        /// <summary>
        ///     This method loads the landing page and makes sure the page is loaded.
        /// </summary>
        /// <returns>An instance of the Chrome Driver</returns>
        public IWebDriver LoadPage()
        {
            _driver = new TWebDriver();
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(LandingPageUrl);
            _driver.FindElement(By.ClassName(Carpark));
            // Check that the Place Bus button is enabled
            Assert.IsTrue(_driver.FindElement(By.ClassName(PlaceBusButton)).Enabled,
                "The Place Bus button is not enabled on initial page load. It should be.");
            // Check that the other buttons are disabled
            Assert.IsFalse(_driver.FindElement(By.Id(MoveButton)).Enabled,
                "The Move button is enabled. It should not be enabled on initial page load");
            Assert.IsFalse(_driver.FindElement(By.Id(LeftButton)).Enabled,
                "The Left button is enabled. It should not be enabled on initial page load");
            Assert.IsFalse(_driver.FindElement(By.Id(RightButton)).Enabled,
                "The Right button is enabled. It should not be enabled on initial page load");
            Assert.IsFalse(_driver.FindElement(By.Id(ReportButton)).Enabled,
                "The Report button is enabled. It should not enabled on initial page load");
            // Return an instance of the Selenium Web Driver
            return _driver;
        }

        // Click on the Place Bus button to place the bus on a grid co-ordinate in the carpark, then check that it has been placed at the correct co-ordinate
        public void ClickPlaceBusButton(string coordinates, string direction)
        {
            _driver.FindElement(By.ClassName(PlaceBusButton)).Click();
            try
            {
                Assert.IsTrue(_driver.FindElement(By.ClassName(coordinates)).Displayed,
                    "The bus has been placed at the wrong co-ordinates in the carpark. It should have been placed at co-ordinate " +
                    coordinates + ".");
                Console.WriteLine("The bus has been placed at the correct co-ordinates in the carpark. I.e. " +
                                  coordinates + " " + direction + ".");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine(
                    "The bus has been placed or moved to the wrong co-ordinates in the carpark. I.e. The bus should be at " +
                    coordinates + " " + direction + ". ");
                throw;
            }
        }

        public void CheckBusIsInCorrectPosition(string coordinates, string direction)
        {
            try
            {
                Assert.IsTrue(_driver.FindElement(By.ClassName(coordinates)).Displayed,
                    "The bus has been placed at the wrong co-ordinates in the carpark. It should have been placed at co-ordinate " +
                    coordinates + ".");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine(
                    "The bus has been placed or moved to the wrong co-ordinates in the carpark. I.e. The bus should be at " +
                    coordinates + " " + direction + ". ");
                throw;
            }
        }

        public void SelectXAndYCoordinates(string x, string y)
        {
            // Select the x co-ordinate drop-down list
            var xCoordinateControl = _driver.FindElement(By.CssSelector(XCoordinateSelectControlLocator));

            // Create select element object for x co-ordinate
            var selectXElement = new SelectElement(xCoordinateControl);

            // Select x co-ordinate by value
            selectXElement.SelectByValue(x);

            //Select the y co-ordinate drop-down list
            var yCoordinateControl = _driver.FindElement(By.CssSelector(YCoordinateSelectControlLocator));

            // Create select element object for y co-ordinate
            var selectYElement = new SelectElement(yCoordinateControl);

            // Select y Co-ordinate by value 
            selectYElement.SelectByValue(y);
        }

        public void SelectDirection(string facing)
        {
            // Select the Facing drop-down list
            var directionControl = _driver.FindElement(By.CssSelector(DirectionControlLocator));

            // Create select element object for direction
            var selectDirectionElement = new SelectElement(directionControl);

            // Select direction by value
            selectDirectionElement.SelectByValue(facing);
        }

        public void Move()
        {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(1000)).Until(ExpectedConditions
                .VisibilityOfAllElementsLocatedBy(By.Id(MoveButton)));
            _driver.FindElement(By.Id(MoveButton)).Click();
        }

        public void RotateBusToLeft()
        {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(1000)).Until(ExpectedConditions
                .VisibilityOfAllElementsLocatedBy(By.Id(LeftButton)));
            _driver.FindElement(By.Id(LeftButton)).Click();
        }

        public void RotateBusToRight()
        {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(1000)).Until(ExpectedConditions
                .VisibilityOfAllElementsLocatedBy(By.Id(RightButton)));
            _driver.FindElement(By.Id(RightButton)).Click();
        }

        // This method compares the actual x and y co-ordinates and direction in the report (message) with the expected values
        public void Report(int x, int y, string direction)
        {
            int expectedX = x;
            int expectedY = y;
            string expectedDirection = direction.ToLower();

            // Step 1: Wait for the report button to be visible
            new WebDriverWait(_driver, TimeSpan.FromSeconds(1000)).Until(ExpectedConditions
                .VisibilityOfAllElementsLocatedBy(By.Id(ReportButton)));

            // Step 2: Click on the report button
            _driver.FindElement(By.Id(ReportButton)).Click();

            // Step 3: Check that a success message is displayed and the content re: the position of the bus is correct
            _driver.FindElement(By.ClassName("alert-success"));

            // Get the entire success message
            string successMessage = _driver.FindElement(By.CssSelector("div.alert")).Text;
            Console.WriteLine("Success message is " + successMessage + ".");

            // Check to see if the x and y co-ordinates and the direction the bus is facing in the message is what you expect
            Assert.IsTrue(successMessage.Contains("X: " + expectedX),
                "The x co-ordinate in the success message is incorrect.");
            Assert.IsTrue(successMessage.Contains("Y: " + expectedY),
                "The y co-ordinate in the success message is incorrect");
            Assert.IsTrue(successMessage.Contains("facing " + expectedDirection),
                "The direction in the success message is incorrect");
        }

        public void QuitDriver()
        {
            _driver.Quit();
        }
    }
}
