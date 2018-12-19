using NUnit.Framework;

namespace BusInCarparkTests.Tests {
    public class MultipleActions {
        [Test]
        public void PlaceBusInDefaultPositionThenMoveAndReport()
        {
            LandingPage landingPage = LandingPage.GetInstance();
            // Step 1: PlaceBusInDefaultPosition
            var placeBus = new PlaceBus();
            placeBus.PlaceBusInDefaultPosition();

            // Step 2: Move
            landingPage.Move();

            // Step 3: Report
           landingPage.Report();

        }
    }
}
