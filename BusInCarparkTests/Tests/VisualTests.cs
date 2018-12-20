using NUnit;
using NUnit.Framework;

namespace BusInCarparkTests.Tests {
    public class VisualTests {
 
        // Visual Test to check that the carpark page UI hasn't changed in different screen sizes, representative of different devices
        // [Test]
        public void CarparkPageVisualCheck()
        {
            // Step 1: Load the landing page
            var singlePage = SinglePage.NewInstance();
            singlePage.LoadPage();

            // Step 2: Capture Screenshot and compare to baseline image
            // CaptureScreenshot();
        }

        // TODO: Finish writing method below and capture and save baseline images
        //private void CaptureScreenshot() {
            // Step 1: SinglePage.HideCursor();
            // Step 2: CaptureDesktop("VisualTestScreenshotsDesktop");
            // Step 3: CaptureTablet("VisualTestScreenshotsTablet");
            // Step 4: CaptureMobile("VisualTestScreenshotsMobile");
            // Step 5: Compare to baseline images and assert the images are the same
        }
}
