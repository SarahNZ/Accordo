using BusInCarparkTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;

namespace BusInCarparkTests.Tests.Navigation
{
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(InternetExplorerDriver))]
    public class MoveBus<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        // Smoke Test: User scenario where the bus is placed in the default position in the carpark, then moved one unit. Test checks that the bus is placed and moved to the correct co-ordinate and the message displayed is correct
        [Test]
        public void PlaceBusInDefaultPositionThenMoveAndReport()
        {
            // Step 1: [Setup] Load the landing page
            new PlaceBus<TWebDriver>().PlaceBusInDefaultPosition();

            // Step 2: Move one unit
            SinglePage<TWebDriver>.GetInstance().Move();

            // Step 3: Report generated
            SinglePage<TWebDriver>.GetInstance().Report(0, 1, "North");
        }

        // Test to ensure bus can't exit the carpark
        [Test]
        public void BusCantExitCarpark()
        {
            // Step 1: [Setup] Load the landing page
            var singlePage = SinglePage<TWebDriver>.NewInstance();
            singlePage.LoadPage();

            // Step 2: Place the bus in the 0,0 (x,y) position in the carpark, facing north
            singlePage.ClickPlaceBusButton(SinglePage<TWebDriver>.CoordinateX0Y0Locator, SinglePage<TWebDriver>.North);

            // Step 3: Move 5 positions north from starting position
            SinglePage<TWebDriver>.GetInstance().Move();
            SinglePage<TWebDriver>.GetInstance().Move();
            SinglePage<TWebDriver>.GetInstance().Move();
            SinglePage<TWebDriver>.GetInstance().Move();
            SinglePage<TWebDriver>.GetInstance().Move();

            // TODO: Remove hard-coding from x,y co-ordinate and direction locator
            // Step 4: Check that the bus is located in the 4,4 (x,y) position of the carpark, facing east
            singlePage.CheckBusIsInCorrectPosition(SinglePage<TWebDriver>.CoordinateX0Y4Locator,
                SinglePage<TWebDriver>.North);

            // Step 5: Rotate the bus right and then move 5 positions east
            SinglePage<TWebDriver>.GetInstance().RotateBusToRight();
            SinglePage<TWebDriver>.GetInstance().Move();
            SinglePage<TWebDriver>.GetInstance().Move();
            SinglePage<TWebDriver>.GetInstance().Move();
            SinglePage<TWebDriver>.GetInstance().Move();
            SinglePage<TWebDriver>.GetInstance().Move();

            // Step 6: Check that the bus is located in the 4,4 (x,y) position of the carpark, facing east
            singlePage.CheckBusIsInCorrectPosition(SinglePage<TWebDriver>.CoordinateX4Y4Locator,
                SinglePage<TWebDriver>.East);

            // Step 7: Rotate the bus right and then move 5 positions south
            // BUG: Unable to move bus south at all from the 4,4 x,y co-ordinate position (manually or via automated test)
            SinglePage<TWebDriver>.GetInstance().RotateBusToRight();
            // TODO: Create loop for repeated functions
            SinglePage<TWebDriver>.GetInstance().Move();
            SinglePage<TWebDriver>.GetInstance().Move();
            SinglePage<TWebDriver>.GetInstance().Move();
            SinglePage<TWebDriver>.GetInstance().Move();
            SinglePage<TWebDriver>.GetInstance().Move();

            // Step 8: Check that the bus is located in the 4,0 (x,y) position of the carpark, facing east
            SinglePage<TWebDriver>.GetInstance()
                .CheckBusIsInCorrectPosition(SinglePage<TWebDriver>.CoordinateX4Y0Locator,
                    SinglePage<TWebDriver>.South);

            // Step 9: Rotate the bus right and then move 5 positions west
            SinglePage<TWebDriver>.GetInstance().RotateBusToRight();
            SinglePage<TWebDriver>.GetInstance().Move();
            SinglePage<TWebDriver>.GetInstance().Move();
            SinglePage<TWebDriver>.GetInstance().Move();
            SinglePage<TWebDriver>.GetInstance().Move();
            SinglePage<TWebDriver>.GetInstance().Move();

            // Step 10: Check that the bus is located in the 0,0 (x,y) position of the carpark, facing west
            SinglePage<TWebDriver>.GetInstance()
                .CheckBusIsInCorrectPosition(SinglePage<TWebDriver>.CoordinateX0Y0Locator, SinglePage<TWebDriver>.West);

            // Step 11: Rotate the bus right and then move 1 position north
            SinglePage<TWebDriver>.GetInstance().RotateBusToRight();
            SinglePage<TWebDriver>.GetInstance().Move();

            // Step 12: Check that the bus is located in the 0,1 (x,y) position of the carpark, facing north
            SinglePage<TWebDriver>.GetInstance()
                .CheckBusIsInCorrectPosition(SinglePage<TWebDriver>.CoordinateX0Y1Locator,
                    SinglePage<TWebDriver>.North);
        }

        [TearDown]
        // All browser windows associated with the driver are closed and the session safely ended after each test
        public void Quit()
        {
            SinglePage<TWebDriver>.GetInstance().QuitDriver();
        }
    }
}
