using BusInCarparkTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;

namespace BusInCarparkTests.Tests.Navigation
{
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(InternetExplorerDriver))]
    public class RotateBus<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private IWebDriver _driver;

        // Test checks that when the bus is placed in the carpark in the default position and rotated to the left, it is facing the correct direction
        [Test]
        public void RotateBusToLeftInDefaultPosition()
        {
            // Create a new instance of the Selenium WebDriver
            _driver = new TWebDriver();

            // Step 1: Load the landing page
            var singlePage = SinglePage<TWebDriver>.NewInstance();
            singlePage.LoadPage();

            // Step 2: Check that the bus is located in the 0,0 (x,y) position of the carpark, facing north
            singlePage.ClickPlaceBusButton(SinglePage<TWebDriver>.CoordinateX0Y0Locator, SinglePage<TWebDriver>.North);

            // Step 3: Rotate the bus to the left
            singlePage.RotateBusToLeft();

            // Step 4: Click the report button and check a success message is displayed and that the x and y coordinates and direction the bus is now facing is correct
            singlePage.Report(0, 0, "west");
        }

        // Test checks that when the bus is placed in the carpark in the default position and rotated to the right, it is facing the correct direction
        [Test]
        public void RotateBusToRightInDefaultPosition()
        {
            // Create a new instance of the Selenium WebDriver
            _driver = new TWebDriver();

            // Step 1: Load the landing page
            var singlePage = SinglePage<TWebDriver>.NewInstance();
            singlePage.LoadPage();

            // Step 2: Check that the bus is located in the 0,0 (x,y) position of the carpark, facing north
            singlePage.ClickPlaceBusButton(SinglePage<TWebDriver>.CoordinateX0Y0Locator, SinglePage<TWebDriver>.North);

            // Step 3: Rotate the bus to the right
            singlePage.RotateBusToRight();

            // Step 4: Click the report button and check a success message is displayed and that the x and y coordinates and direction the bus is now facing is correct
            singlePage.Report(0, 0, "east");
        }

        [TearDown]
        // All browser windows associated with the driver are closed and the session safely ended after each test
        public void Quit()
        {
            SinglePage<TWebDriver>.GetInstance().QuitWebDriver();
        }
    }
}
