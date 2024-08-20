using AppiumIntegrationTest;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Interactions;
using Assert = NUnit.Framework.Assert;

namespace APV.Test
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture("14.0", "emulator-5556", 4724)]
    public class HomePageTest : AndroidBaseTest
    {

        public HomePageTest(string platformVersion, string avdUdid, int port) : base(platformVersion, avdUdid, port) { }

        [Test]
        public void SwipingOnMovieList()
        {
            // TODO: swiping action need adjustment
            string comedy_deadpoolWolverine_poster_XPath = "//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup/androidx.recyclerview.widget.RecyclerView/android.view.ViewGroup[4]/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/androidx.recyclerview.widget.RecyclerView/android.view.ViewGroup[1]/android.view.ViewGroup/android.view.ViewGroup/android.widget.ImageView";
            string comedy_theGarfieldMovie_poster_XPath = "//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup/androidx.recyclerview.widget.RecyclerView/android.view.ViewGroup[3]/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/androidx.recyclerview.widget.RecyclerView/android.view.ViewGroup[3]/android.view.ViewGroup/android.view.ViewGroup/android.widget.ImageView";

            var calibrateCoordinate = pointerInputDevice.CreatePointerMove(CoordinateOrigin.Viewport, windowWidth / 2, 1600, TimeSpan.Zero);
            var pressDown = pointerInputDevice.CreatePointerDown(pointerButton);
            var releasePress = pointerInputDevice.CreatePointerUp(pointerButton);

            var swipeVertical = pointerInputDevice.CreatePointerMove(CoordinateOrigin.Pointer, 0, -windowHeight, TimeSpan.FromSeconds(2));
            ActionBuilder swipeDownToComedyGenre = new ActionBuilder()
                .AddActions(calibrateCoordinate, pressDown, swipeVertical, releasePress);
            _driver.PerformActions(swipeDownToComedyGenre.ToActionSequenceList());

            var deadpoolWolverinePoster = _driver.FindElement(MobileBy.XPath(comedy_deadpoolWolverine_poster_XPath));
            var swipeLeftOnDeadpoolWolverinePoster = pointerInputDevice.CreatePointerMove(deadpoolWolverinePoster, -windowWidth, 0, TimeSpan.FromSeconds(2));
            ActionBuilder swipeLeftOnComedyGenre = new ActionBuilder().AddActions(calibrateCoordinate, pressDown, swipeLeftOnDeadpoolWolverinePoster, releasePress);
            _driver.PerformActions(swipeLeftOnComedyGenre.ToActionSequenceList());

            var theGarfieldMoviePoster = _driver.FindElement(MobileBy.XPath(comedy_theGarfieldMovie_poster_XPath));
            Assert.That(theGarfieldMoviePoster.Displayed, Is.True);

            theGarfieldMoviePoster.Click();
        }
    }
}
