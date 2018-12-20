﻿using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace BusInCarparkTests {
    public class SinglePage
    {
        private static SinglePage _instance;

        private IWebDriver _driver = new ChromeDriver();

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

        // Direction the bus is facing. This dictates which way the bus will move in.
        public const string North = "face-north";
        public const string South = "face-south";
        public const string East = "face-east";
        public const string West = "face-west";

        public static SinglePage GetInstance()
        {
            if (_instance == null)
            {
                _instance = new SinglePage();
            }
            return _instance;
        }

        public static SinglePage NewInstance()
        {
            _instance = new SinglePage();
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
            Console.WriteLine("The bus has been placed at the correct co-ordinates in the carpark. I.e. " + coordinates + direction + ".");
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
            var directionControl = _driver.FindElement((By.CssSelector(SinglePage.DirectionControlLocator)));

            // Create select element object for direction
            var selectDirectionElement = new SelectElement(directionControl);

            // Select direction by value
            selectDirectionElement.SelectByValue(facing);
        }

        public void Move()
        {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(1000)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .VisibilityOfAllElementsLocatedBy(By.Id(MoveButton)));
            _driver.FindElement(By.Id(MoveButton)).Click();

        }

        public void RotateBusToLeft()
        {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(1000)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .VisibilityOfAllElementsLocatedBy(By.Id(LeftButton)));
            _driver.FindElement(By.Id(LeftButton)).Click();
        }

        public void RotateBusToRight() {
            new WebDriverWait(_driver, TimeSpan.FromSeconds(1000)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .VisibilityOfAllElementsLocatedBy(By.Id(RightButton)));
            _driver.FindElement(By.Id(RightButton)).Click();
        }

        public void Report(int x, int y, string direction)
        {
            int expectedX = x;
            int expectedY = y;
            string expectedDirection = direction.ToLower();

            // Step 1: Wait for the report button to be visible
            new WebDriverWait(_driver, TimeSpan.FromSeconds(1000)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .VisibilityOfAllElementsLocatedBy(By.Id(ReportButton))); 

            // Step 2: Click on the report button
            _driver.FindElement(By.Id(ReportButton)).Click();   

            // Step 3: Check that a success message is displayed and the content re: the position of the bus is correct
            _driver.FindElement(By.ClassName("alert-success"));

            // Get the entire success message
            string successMessage = _driver.FindElement(By.CssSelector("div.alert")).Text;
            Console.WriteLine("Success message is "+ successMessage + ".");

            // Check to see if the x and y co-ordinates and the direction the bus is facing in the message is what you expect
            Assert.IsTrue(successMessage.Contains("X: " + expectedX), "The x co-ordinate in the success message is incorrect.");
            Assert.IsTrue(successMessage.Contains("Y: " + expectedY), "The y co-ordinate in the success message is incorrect");
            Assert.IsTrue(successMessage.Contains("facing " + expectedDirection), "The direction in the success message is incorrect");
        }

        public void QuitWebDriver() {
            _driver.Quit();
        }
    }
}