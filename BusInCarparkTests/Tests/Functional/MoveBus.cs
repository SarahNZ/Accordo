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

        // Test to ensure bus can't exit the carpark
        [Test]
        public void BusCantExitCarpark()
        {
            // Step 1: Load the landing page
            var singlePage = SinglePage.NewInstance();
            singlePage.LoadPage();

            // Step 2: Place the bus in the 0,0 (x,y) position in the carpark, facing north
            singlePage.ClickPlaceBusButton(SinglePage.CoordinateX0Y0Locator, SinglePage.North);

            // Step 2: Move 5 positions north from starting position
            SinglePage.GetInstance().Move();
            SinglePage.GetInstance().Move();
            SinglePage.GetInstance().Move();
            SinglePage.GetInstance().Move();
            SinglePage.GetInstance().Move();

            // TODO: Remove hard-coding from x,y co-ordinate and direction locator
            // Step 3: Check that the bus is located in the 4,4 (x,y) position of the carpark, facing east
            singlePage.CheckBusIsInCorrectPosition(SinglePage.CoordinateX0Y4Locator, SinglePage.North);

            // Step 4: Rotate the bus right and then move 5 positions east
            SinglePage.GetInstance().RotateBusToRight();
            SinglePage.GetInstance().Move();
            SinglePage.GetInstance().Move();
            SinglePage.GetInstance().Move();
            SinglePage.GetInstance().Move();
            SinglePage.GetInstance().Move();

            // Step 5: Check that the bus is located in the 4,4 (x,y) position of the carpark, facing east
            singlePage.CheckBusIsInCorrectPosition(SinglePage.CoordinateX4Y4Locator, SinglePage.East);

            // Step 6: Rotate the bus right and then move 5 positions south
            // BUG: Unable to move bus south at all from the 4,4 x,y co-ordinate position (manually or via automated test)
            SinglePage.GetInstance().RotateBusToRight();
            SinglePage.GetInstance().Move();
            SinglePage.GetInstance().Move();
            SinglePage.GetInstance().Move();
            SinglePage.GetInstance().Move();
            SinglePage.GetInstance().Move();

            // Step 7: Check that the bus is located in the 4,0 (x,y) position of the carpark, facing east
            singlePage.CheckBusIsInCorrectPosition(SinglePage.CoordinateX4Y0Locator, SinglePage.South);

            // Step 8: Rotate the bus right and then move 5 positions west
            SinglePage.GetInstance().RotateBusToRight();
            SinglePage.GetInstance().Move();
            SinglePage.GetInstance().Move();
            SinglePage.GetInstance().Move();
            SinglePage.GetInstance().Move();
            SinglePage.GetInstance().Move();

            // Step 9: Check that the bus is located in the 0,0 (x,y) position of the carpark, facing west
            singlePage.CheckBusIsInCorrectPosition(SinglePage.CoordinateX0Y0Locator, SinglePage.West);

            // Step 10: Rotate the bus right and then move 1 position north
            SinglePage.GetInstance().RotateBusToRight();
            SinglePage.GetInstance().Move();

            // Step 11: Check that the bus is located in the 0,1 (x,y) position of the carpark, facing north
            singlePage.CheckBusIsInCorrectPosition(SinglePage.CoordinateX0Y1Locator, SinglePage.North);
        }

        [TearDown]
        // All browser windows associated with the driver are closed and the session safely ended after each test
        public void Quit() {
            SinglePage.GetInstance().QuitWebDriver();
        }
    }
}
