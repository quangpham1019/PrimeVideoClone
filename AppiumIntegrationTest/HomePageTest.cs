using AppiumIntegrationTest;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Interactions;
using Assert = NUnit.Framework.Assert;

namespace APV.Test
{
    [TestFixture("14.0", "emulator-5556", 4724)]
    public class HomePageTest : AndroidBaseTest
    {
        private readonly string scrollViewClassName = "android.widget.ScrollView";

        private readonly string carouselViewId = "com.companyname.apv:id/carouselView";
        private readonly string carouselViewIndicatorId = "com.companyname.apv:id/carouselViewIndicator";

        private readonly string topMenuShadowId = "com.companyname.apv:id/topMenuShadow";
        private readonly string topMenuId = "com.companyname.apv:id/topMenu";

        private readonly string movieRowCollectionViewId = "com.companyname.apv:id/movieRowCollectionView";
        private readonly string movieRowCollectionView_movieRowClass = "androidx.recyclerview.widget.RecyclerView";
        private readonly string movieRowCollectionView_movieRow_posterClass = "android.widget.ImageView";

        private readonly string homePageNavigationBarId = "com.companyname.apv:id/homePageNavigationBar";
        private readonly string homePageItemFilterId = "com.companyname.apv:id/homePageItemFilter";
        //private readonly string Id = "";

        private Interaction pressDown, releasePress, movePointerToScreenCenter;

        public HomePageTest(string platformVersion, string avdUdid, int port) : base(platformVersion, avdUdid, port) { }

        [Test]
        [Order(0)]
        public void SetUpVar()
        {
            //var carouselView = _driver.FindElement(MobileBy.Id(carouselViewId));
            //var carouselViewIndicator = _driver.FindElement(MobileBy.Id(carouselViewIndicatorId));

            //var topMenuShadow = _driver.FindElement(MobileBy.Id(topMenuShadowId));
            //var topMenu = _driver.FindElement(MobileBy.Id(topMenuId));

            //var movieRowCollectionView = _driver.FindElement(MobileBy.Id(movieRowCollectionViewId));
            //var movieRowCollectionView_movieRowList = movieRowCollectionView.FindElements(MobileBy.Id(movieRowCollectionView_movieRowClass));

            //var homePageNavigationBarList = _driver.FindElements(MobileBy.Id(homePageNavigationBarId));
            //var homePageItemFilterList = _driver.FindElements(MobileBy.Id(homePageItemFilterId));

            movePointerToScreenCenter = pointerInputDevice.CreatePointerMove(CoordinateOrigin.Viewport, windowWidth/2, windowHeight/2, TimeSpan.FromSeconds(1));
            pressDown = pointerInputDevice.CreatePointerDown(pointerButton);
            releasePress = pointerInputDevice.CreatePointerUp(pointerButton);
        }

        [Test]
        [Order(1)]
        public void TopMenuDisappearOnQuickSwipeUp()
        {
            // move pointer to center of screen, press, move up about 20px/s, release
            PerformScroll(0, -windowHeight/2, 0.5);

            // Assert that the topMenu no longer contains navigationBar and itemFilterList
            var topMenu = _driver.FindElement(MobileBy.Id(topMenuId));
            bool homePageItemFilterIsVisible = topMenu.FindElements(MobileBy.Id(homePageItemFilterId)).Count != 0;
            bool homePageNavigationBarIsVisible = topMenu.FindElements(MobileBy.Id(homePageNavigationBarId)).Count != 0;

            Assert.Multiple(() =>
            {
                Assert.That(homePageNavigationBarIsVisible, Is.False);
                Assert.That(homePageItemFilterIsVisible, Is.False);
            });
        }

        [Test]
        [Order(2)]
        public void TopMenuReappearOnQuickSwipeDown()
        {
            // move pointer to center of screen, press, move down about 20px/s, release
            PerformScroll(0, windowHeight/2, 0.5);

            // Assert that the topMenu now contains navigationBar and itemFilterList
            var topMenu = _driver.FindElement(MobileBy.Id(topMenuId));
            bool homePageItemFilterIsVisible = topMenu.FindElements(MobileBy.Id(homePageItemFilterId)).Count != 0;
            bool homePageNavigationBarIsVisible = topMenu.FindElements(MobileBy.Id(homePageNavigationBarId)).Count != 0;


            Assert.Multiple(() =>
            {
                Assert.That(homePageNavigationBarIsVisible, Is.True);
                Assert.That(homePageItemFilterIsVisible, Is.True);
            });
        }



        [Test]
        [Order(3)]
        public void SwipeOnLastMovieRowInView()
        {
            // find the movieRowCollectionView
            // find the last recyclerView of movieRowCollectionView, record the elementId of the last element
            // swipe left on the last recylerView
            // Assert that the current last element of the last recyclerView has a different elementId

            PerformScroll(0, -windowHeight / 2, 0.5);

            var movieRowCollectionView = _driver.FindElement(MobileBy.Id(movieRowCollectionViewId));
            var movieRowCollectionView_movieRowList = movieRowCollectionView.FindElements(MobileBy.ClassName(movieRowCollectionView_movieRowClass));

            var lastPoster = movieRowCollectionView_movieRowList[^1]
                .FindElements(MobileBy.ClassName(movieRowCollectionView_movieRow_posterClass))
                .Last();
            var lastPosterId = lastPoster.Id;

            var mainAction = pointerInputDevice.CreatePointerMove(lastPoster, -windowWidth * 4 / 3, 0, TimeSpan.FromSeconds(2));
            ActionBuilder swipeLeftOnMovieRow = new ActionBuilder().AddActions(
                pointerInputDevice.CreatePointerMove(lastPoster, 0, 0, TimeSpan.FromSeconds(2)),
                pressDown,
                mainAction,
                releasePress);
            _driver.PerformActions(swipeLeftOnMovieRow.ToActionSequenceList());

            movieRowCollectionView_movieRowList = movieRowCollectionView.FindElements(MobileBy.ClassName(movieRowCollectionView_movieRowClass));
            var lastPosterAfterSwipeId = movieRowCollectionView_movieRowList[^1]
                .FindElements(MobileBy.ClassName(movieRowCollectionView_movieRow_posterClass))
                .Last()
                .Id;

            Assert.That(lastPosterId, Is.Not.EqualTo(lastPosterAfterSwipeId));
        }

        //[Test]
        //[Order(5)]
        //public void Login()
        //{

        //}

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
    }
}
