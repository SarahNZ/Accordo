using NUnit.Framework;

namespace BusInCarparkTests.Tests {
   public class PlaceBus {

       [Test]
       public void PlaceBusInDefaultPosition()
       {
            // Step 1: Load the landing page
            var landingPage = new LandingPage();
            landingPage.RefreshPage();
            landingPage.LoadPage();

            // Step 2: Check that the bus is located in the 0,0 (x,y) position of the carpark, facing north
            landingPage.ClickPlaceBusButton(LandingPage.CoordinateX0Y0Locator, LandingPage.North);
       }

        [Test]
       public void PlaceBusAtCoordinateX0Y1()
       {
            // Step 1: Load the landing page
            var landingPage = new LandingPage();
            landingPage.RefreshPage();
            landingPage.LoadPage();

            // Step 2: Place the bus at coordinate X0Y1 (leave default direction as north)
            landingPage.SelectXAndYCoordinates("0","1");
            landingPage.ClickPlaceBusButton(LandingPage.CoordinateX0Y1Locator, LandingPage.North);
        }
    }
}
