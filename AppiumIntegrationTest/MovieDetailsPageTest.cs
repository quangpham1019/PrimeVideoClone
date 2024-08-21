using AppiumIntegrationTest;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Interactions;
using Assert = NUnit.Framework.Assert;

namespace APV.Test
{
    [TestFixture("14.0", "emulator-5554", 4723)]
    public class MovieDetailsPageTest : AndroidBaseTest
    {
        public MovieDetailsPageTest(string platformVersion, string avdUdid, int port) : base(platformVersion, avdUdid, port) { }

        [Test]
        public void TestOnPosterClick()
        {
            var godFather_poster_xPath = "//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup/androidx.recyclerview.widget.RecyclerView[2]/android.view.ViewGroup[1]/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/androidx.recyclerview.widget.RecyclerView/android.view.ViewGroup[2]/android.view.ViewGroup/android.view.ViewGroup/android.widget.ImageView";
            var godFather_textOverview_xPath = "//android.widget.TextView[@text=\"Spanning the years 1945 to 1955, a chronicle of the fictional Italian-American Corleone crime family. When organized crime family patriarch, Vito Corleone barely survives an attempt on his life, his youngest son, Michael steps in to take care of the would-be killers, launching a campaign of bloody revenge.\"]";
            var godFather_moreDetailsBtn_xPath = "//android.widget.TextView[@text=\"More Details\"]";
            var godFather_relatedTab_randomRelatedMoviePoster_xPath = "//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup[4]/android.view.ViewGroup/android.view.ViewGroup[15]/android.widget.ImageView";
            var godFather_moreDetailsTab_starringLabel_xPath = "//android.widget.TextView[@text=\"Starring\"]";

            var godFather_Poster = _driver.FindElement(MobileBy.XPath(godFather_poster_xPath));
            godFather_Poster.Click();

            var godFather_textOverview = _driver.FindElement(MobileBy.XPath(godFather_textOverview_xPath));
            var godFather_moreDetailsBtn = _driver.FindElement(MobileBy.XPath(godFather_moreDetailsBtn_xPath));

            Assert.Multiple(() =>
            {
                Assert.That(godFather_textOverview.Displayed, Is.True);
                Assert.That(godFather_moreDetailsBtn.Displayed, Is.True);
            });


            var calibrateCoordinate = pointerInputDevice.CreatePointerMove(CoordinateOrigin.Viewport, windowWidth / 2, 1600, TimeSpan.Zero);
            var pressDown = pointerInputDevice.CreatePointerDown(pointerButton);
            var releasePress = pointerInputDevice.CreatePointerUp(pointerButton);

            var swipeVertical = pointerInputDevice.CreatePointerMove(CoordinateOrigin.Pointer, 0, -windowHeight, TimeSpan.FromSeconds(2));
            ActionBuilder swipeDownToComedyGenre = new ActionBuilder()
                .AddActions(calibrateCoordinate, pressDown, swipeVertical, releasePress);
            _driver.PerformActions(swipeDownToComedyGenre.ToActionSequenceList());

            var godFather_relatedTab_randomRelatedMoviePoster = _driver.FindElement(MobileBy.XPath(godFather_relatedTab_randomRelatedMoviePoster_xPath));
            Assert.Multiple(() =>
            {
                Assert.That(godFather_textOverview.Displayed, Is.True);
                Assert.That(godFather_moreDetailsBtn.Displayed, Is.True);
                Assert.That(godFather_relatedTab_randomRelatedMoviePoster.Displayed, Is.True);
            });

            godFather_moreDetailsBtn = _driver.FindElement(MobileBy.XPath(godFather_moreDetailsBtn_xPath));
            godFather_moreDetailsBtn.Click();

            var godFather_moreDetailsTab_starringLabel = _driver.FindElement(MobileBy.XPath(godFather_moreDetailsTab_starringLabel_xPath));
            Assert.Multiple(() =>
            {
                Assert.That(godFather_textOverview.Displayed, Is.True);
                Assert.That(godFather_moreDetailsBtn.Displayed, Is.True);
                Assert.That(godFather_moreDetailsTab_starringLabel.Displayed, Is.True);
            });
        }
    }
}
