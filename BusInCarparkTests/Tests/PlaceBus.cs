using System.Diagnostics;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BusInCarparkTests.Tests {
   public class PlaceBus {

       [Test]
       public void PlaceBusInDefaultPosition()
       {
            // Step 1: Load the landing page
            var carparkPage = new CarparkPage();
            carparkPage.LoadPage();

            // Step 2: Check that the bus is located in the 0,0 (x,y) position of the carpark, facing north
            carparkPage.ClickPlaceBusButton(CarparkPage.CoordinateX0Y0Locator, CarparkPage.North);
       }

        [Test]
       public void PlaceBusAtCoordinateX0Y1()
       {
           // Step 1: Load the landing page
           var carparkPage = new CarparkPage();
           carparkPage.LoadPage();

            // Step 2: Place the bus at coordinate X0Y1 (leave default direction as north)
           carparkPage.SelectXAndYCoordinates("0","1");
           carparkPage.ClickPlaceBusButton(CarparkPage.CoordinateX0Y1Locator, CarparkPage.North);
        }
    }
}
