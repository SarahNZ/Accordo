﻿using BusInCarparkTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;

namespace BusInCarparkTests.Tests.Functional {

    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(InternetExplorerDriver))]

    public class MoveAndRotateBus<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private IWebDriver _driver;

        // Smoke Test: User scenario where a sequence of different move and rotation actions are performed. Test checks the bus is in the correct x,y co-ordinate and facing the correct direction after each action. Also, checks the message displayed at the end is correct.
        [Test]
        public void PlaceBusInX1Y2EastPositionThenMoveAndRotateAndMoveAgainAndReport() {
            // Create a new instance of the Selenium WebDriver
            _driver = new TWebDriver();
            // Step 1: Load the landing page
            var singlePage = new SinglePage<TWebDriver>();
            singlePage.LoadPage();

            // Step 2: Place the bus in the carpark at the initial coordinate X1Y2 and face east
            // TODO: Code Improvement - Convert int to string, so not hard-coded below
            string xString = "1";
            string yString = "2";
            string direction = "east";

            singlePage.SelectXAndYCoordinates(xString, yString);
            singlePage.SelectDirection(direction);
            // TODO: Create Enum for directions and pass x and y values in to create locator rather than hard-coding it
            singlePage.ClickPlaceBusButton(SinglePage<TWebDriver>.CoordinateX1Y2Locator, SinglePage<TWebDriver>.East);

            // TODO: Automated test fails at Step 3. Can't find the existing instance of the Single Page Class, so it creates one. A new browser window is opened with no URL. Not sure what's going on.
            // Step 3: Move one unit east
            SinglePage<TWebDriver>.GetInstance().Move();

            // Step 4: Move one unit east again
            SinglePage<TWebDriver>.GetInstance().Move();

            // Step 5: Rotate bus to the left
            SinglePage<TWebDriver>.GetInstance().RotateBusToLeft();

            // Step 5: Move one unit north
            SinglePage<TWebDriver>.GetInstance().Move();

            // Step 6: Report generated
            SinglePage<TWebDriver>.GetInstance().Report(3, 3, "North");
        }

        [TearDown]
        // All browser windows associated with the driver are closed and the session safely ended after each test
        public void Quit() {
            SinglePage<TWebDriver>.GetInstance().QuitWebDriver();
        }
    }
}
