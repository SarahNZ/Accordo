using NUnit.Framework;

namespace BusInCarparkTests.Tests {
    public class MoveAndRotateBus {
        // Smoke Test: Likely user scenario where a sequence of different move and rotation actions are performed. Test checks the bus is in the correct x,y co-ordinate and facing the correct direction after each action. Also, checks the message displayed at the end is correct.
        [Test]
        public void PlaceBusInX1Y2EastPositionThenMoveAndRotateAndMoveAgainAndReport() {
            // Step 1: Load the landing page
            var singlePage = new SinglePage();
            singlePage.LoadPage();

            // Step 2: Place the bus in the carpark at the initial coordinate X1Y2 and face east
            int x = 1;
            int y = 2;
            // TODO: Code Improvement - Convert int to string, so not hard-coded below
            string xString = "1";
            string yString = "2";
            string direction = "east";

            singlePage.SelectXAndYCoordinates(xString, yString);
            singlePage.SelectDirection(direction);
            // TODO: Create Enum for directions and pass x and y values in to create locator rather than hard-coding it
            singlePage.ClickPlaceBusButton(SinglePage.CoordinateX1Y2Locator, SinglePage.East);

            // Step 3: Click Report button and make sure the success message displayed and the information is correct
            singlePage.Report(x, y, "east");

            // Step 4: Move one unit east
            SinglePage.GetInstance().Move();

            // Step 5: Move one unit east again
            SinglePage.GetInstance().Move();

            // Step 6: Rotate bus to the left
            SinglePage.GetInstance().RotateBusToLeft();

            // Step 5: Move one unit north
            SinglePage.GetInstance().Move();

            // Step 3: Report generated
            SinglePage.GetInstance().Report(3, 3, "North");
        }

        [TearDown]
        // All browser windows associated with the driver are closed and the session safely ended after each test
        public void Quit() {
            SinglePage.GetInstance().QuitWebDriver();
        }
    }
}
