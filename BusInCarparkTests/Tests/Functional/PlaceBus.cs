using BusInCarparkTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;

namespace BusInCarparkTests.Tests.Functional
{
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(InternetExplorerDriver))]
    public class PlaceBus<TWebDriver> where TWebDriver : IWebDriver, new() {
   
        private IWebDriver _driver;

        // Test checks that the bus is placed at the default position in the carpark when the Place Bus button is clicked (using default co-ordinates)
        [Test]
        public void PlaceBusInDefaultPosition() {
            // Create a new instance of the Selenium WebDriver
            _driver = new TWebDriver();

            // Step 1: Load the landing page
            var singlePage = SinglePage<TWebDriver>.NewInstance();
            singlePage.LoadPage();

            // Step 2: Check that the bus is located in the 0,0 (x,y) position of the carpark, facing north
            singlePage.ClickPlaceBusButton(SinglePage<TWebDriver>.CoordinateX0Y0Locator, SinglePage<TWebDriver>.North);
        }

        // Test checks that the bus is placed at the default position in the carpark when the Place Bus button is clicked (using default co-ordinates) and the success message is correct
        [Test]
        public void PlaceBusInDefaultPositionAndReport()
        {
            // Create a new instance of the Selenium WebDriver
            _driver = new TWebDriver();

            // Step 1: Load the landing page
            var singlePage = SinglePage<TWebDriver>.NewInstance();
            singlePage.LoadPage();

            // Step 2: Check that the bus is located in the 0,0 (x,y) position of the carpark, facing north
            singlePage.ClickPlaceBusButton(SinglePage<TWebDriver>.CoordinateX0Y0Locator, SinglePage<TWebDriver>.North);

            // Step 3: Click Report button
            singlePage.Report(0, 0, "north");
        }

        // Test checks that the bus is placed at the X0Y0 position in the carpark when the Place Bus button is clicked
        [Test]
        public void PlaceBusAtCoordinateX0Y1AndReport()
        {
            // Create a new instance of the Selenium WebDriver
            _driver = new TWebDriver();

            // Step 1: Load the landing page
            var singlePage = new SinglePage<TWebDriver>();
            singlePage.LoadPage();

            // Step 2: Place the bus at coordinate X0Y1 (leave default direction as north), and check that it is actually placed in the correct position
            int x = 0;
            int y = 1;
            string xString = "0";
            string yString = "1";

            singlePage.SelectXAndYCoordinates(xString, yString);
            singlePage.ClickPlaceBusButton(SinglePage<TWebDriver>.CoordinateX0Y1Locator, SinglePage<TWebDriver>.North);

            // Step 3: Click Report button
            singlePage.Report(x, y, "north");
        }

        [TearDown]
        // All browser windows associated with the driver are closed and the session safely ended after each test
        public void Quit() {
            SinglePage<TWebDriver>.GetInstance().QuitWebDriver();
        }
    }
}
