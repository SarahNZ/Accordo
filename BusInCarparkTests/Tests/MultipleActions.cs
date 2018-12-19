using NUnit.Framework;

namespace BusInCarparkTests.Tests {
    public class MultipleActions {
        // Smoke Test: Likely user scenario where the bus is placed in the default position in the carpark, then moved one unit. Test checks that the bus is placed and moved to the correct co-ordinate and the message displayed is correct
        [Test]
        public void PlaceBusInDefaultPositionThenMoveAndReport()
        {
            // Step 1: Load the landing page
            new PlaceBus().PlaceBusInDefaultPosition();

            // Step 2: Move one unit
            LandingPage.GetInstance().Move();

            // Step 3: Report generated
            LandingPage.GetInstance().Report();

        }
    }
}
