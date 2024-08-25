using AppiumIntegrationTest;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Interactions;
using Assert = NUnit.Framework.Assert;

namespace APV.Test
{
    [TestFixture("14.0", "emulator-5554", 4723)]
    public class MovieDetailsPageTest : AndroidBaseTest
    {
        private readonly string homePage_movieRowCollectionViewId = "com.companyname.apv:id/movieRowCollectionView";
        private readonly string homePage_movieRowCollectionView_movieRowClass = "androidx.recyclerview.widget.RecyclerView";
        private readonly string homePage_movieRowCollectionView_movieRow_posterClass = "android.widget.ImageView";

        private readonly string movieDetailsPage_scrollViewClassName = "android.widget.ScrollView";
        private readonly string movieDetailsPage_backgroundImageId = "com.companyname.apv:id/backgroundImage";
        private readonly string movieDetailsPage_overviewId = "com.companyname.apv:id/overview";
        private readonly string movieDetailsPage_title = "com.companyname.apv:id/title";

        private readonly string movieDetailsPage_relatedTabId = "com.companyname.apv:id/relatedTab";
        private readonly string movieDetailsPage_relatedTabContentId = "com.companyname.apv:id/relatedTabContent";
        private readonly string movieDetailsPage_moreDetailsTabId = "com.companyname.apv:id/moreDetailsTab";
        private readonly string movieDetailsPage_moreDetailsTabContentId = "com.companyname.apv:id/moreDetailsTabContent";

        //private readonly string Id = "";

        private Interaction pressDown, releasePress, movePointerToScreenCenter;
        public MovieDetailsPageTest(string platformVersion, string avdUdid, int port) : base(platformVersion, avdUdid, port) { }

        [Test]
        [Order(0)]
        public void SetUpVar()
        {

            movePointerToScreenCenter = pointerInputDevice.CreatePointerMove(CoordinateOrigin.Viewport, windowWidth / 2, windowHeight / 2, TimeSpan.FromSeconds(1));
            pressDown = pointerInputDevice.CreatePointerDown(pointerButton);
            releasePress = pointerInputDevice.CreatePointerUp(pointerButton);
        }

        [Test]
        [Order(1)]
        public void ClickOnPoster()
        {
            var movieRowCollectionView = _driver.FindElement(MobileBy.Id(homePage_movieRowCollectionViewId));
            var movieRowCollectionView_movieRowList = movieRowCollectionView.FindElements(MobileBy.ClassName(homePage_movieRowCollectionView_movieRowClass));

            var lastPoster = movieRowCollectionView_movieRowList[^1]
                .FindElements(MobileBy.ClassName(homePage_movieRowCollectionView_movieRow_posterClass))
                .Last();
            lastPoster.Click();

            var movieDetailsPage_scrollView = _driver.FindElement(MobileBy.ClassName(movieDetailsPage_scrollViewClassName));

            var movieDetailsPage_overview = _driver.FindElement(MobileBy.Id(movieDetailsPage_overviewId));
            var movieDetailsPage_moreDetailsTab = movieDetailsPage_scrollView.FindElement(MobileBy.Id(movieDetailsPage_moreDetailsTabId));
            var movieDetailsPage_backgroundImage = _driver.FindElement(MobileBy.Id(movieDetailsPage_backgroundImageId));

            Assert.Multiple(() =>
            {
                Assert.That(movieDetailsPage_overview.Displayed, Is.True);
                Assert.That(movieDetailsPage_moreDetailsTab.Displayed, Is.True);
                Assert.That(movieDetailsPage_backgroundImage.Displayed, Is.True);

            });


            PerformScroll(0, -windowHeight, 2);

            PseudoTap(movieDetailsPage_moreDetailsTab, 1);
        }

        [Test]
        [Order(2)]
        public void test2()
        {

        }



        [Test]
        [Order(3)]
        public void test3()
        {

        }

        void PerformScroll(int distanceX, int distanceY, double seconds)
        {
            var mainAction = pointerInputDevice.CreatePointerMove(CoordinateOrigin.Pointer, distanceX, distanceY, TimeSpan.FromSeconds(seconds));
            ActionBuilder quickSwipeUp = new ActionBuilder().AddActions(
                movePointerToScreenCenter,
                pressDown,
                mainAction,
                releasePress);
            _driver.PerformActions(quickSwipeUp.ToActionSequenceList());
        }
        void PseudoTap(AppiumElement element, double seconds)
        {
            var moveToElement = pointerInputDevice.CreatePointerMove(element, 0, 0, TimeSpan.FromSeconds(seconds));
            ActionBuilder pseudoTap = new ActionBuilder().AddActions(
                moveToElement,
                pressDown,
                releasePress);
            _driver.PerformActions(pseudoTap.ToActionSequenceList());
        }
    }
}
