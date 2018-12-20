using NUnit.Framework;

namespace BusInCarparkTests.Tests {
    public class MoveBus {
        // Smoke Test: Likely user scenario where the bus is placed in the default position in the carpark, then moved one unit. Test checks that the bus is placed and moved to the correct co-ordinate and the message displayed is correct
        [Test]
        public void PlaceBusInDefaultPositionThenMoveAndReport()
        {
            // Step 1: Load the landing page
            new PlaceBus().PlaceBusInDefaultPosition();

            // Step 2: Move one unit
            SinglePage.GetInstance().Move();

            // Step 3: Report generated
            SinglePage.GetInstance().Report(0,1,"North");
        }

        [TearDown]
        // All browser windows associated with the driver are closed and the session safely ended after each test
        public void Quit() {
            SinglePage.GetInstance().QuitWebDriver();
        }
    }
}
