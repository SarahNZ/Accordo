using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace BusInCarparkTests.Tests {
   public class PlaceBus {

       // Test checks that the bus is placed at the default position in the carpark when the Place Bus button is clicked (using default co-ordinates)
       [Test]
       public void PlaceBusInDefaultPosition()
       {
            // Step 1: Load the landing page
            var landingPage = LandingPage.NewInstance();
            landingPage.LoadPage();

            // Step 2: Check that the bus is located in the 0,0 (x,y) position of the carpark, facing north
            landingPage.ClickPlaceBusButton(LandingPage.CoordinateX0Y0Locator, LandingPage.North);
       }

        // Test checks that the bus is placed at the X0Y0 position in the carpark when the Place Bus button is clicked
        [Test]
       public void PlaceBusAtCoordinateX0Y1()
       {
            // Step 1: Load the landing page
            var landingPage = new LandingPage();
            landingPage.LoadPage();

            // Step 2: Place the bus at coordinate X0Y1 (leave default direction as north), and check that it is actually placed in the correct position
            landingPage.SelectXAndYCoordinates("0","1");
            landingPage.ClickPlaceBusButton(LandingPage.CoordinateX0Y1Locator, LandingPage.North);
        }

       // All browser windows associated with the driver are closed and the session safely ended after each test
        [TearDown]
       public void Quit()
       {
           LandingPage.GetInstance().QuitWebDriver();
        }
   }
}
