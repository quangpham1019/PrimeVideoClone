using AppiumIntegrationTest;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Interactions;
using Assert = NUnit.Framework.Assert;

namespace APV.Test
{
    [TestFixture("14.0", "emulator-5556", 4724)]
    public class HomePageTest : AndroidBaseTest
    {

        private readonly string carouselViewId = "com.companyname.apv:id/carouselView";
        private readonly string carouselViewIndicatorId = "com.companyname.apv:id/carouselViewIndicator";
        private readonly string topMenuShadowId = "com.companyname.apv:id/topMenuShadow";
        private readonly string topMenuId = "com.companyname.apv:id/topMenu";
        private readonly string movieRowCollectionViewId = "com.companyname.apv:id/movieRowCollectionView";
        private readonly string movieRowCollectionView_movieRowClass = "androidx.recyclerview.widget.RecyclerView";
        private readonly string homePageNavigationBarId = "com.companyname.apv:id/homePageNavigationBar";
        private readonly string homePageItemFilterId = "com.companyname.apv:id/homePageItemFilter";
        //private readonly string Id = "";

        public HomePageTest(string platformVersion, string avdUdid, int port) : base(platformVersion, avdUdid, port) { }

        [Test]
        [Order(0)]
        public void SwipeDownOnScreen()
        {
            var carouselView = _driver.FindElement(MobileBy.Id(carouselViewId));
            var carouselViewIndicator = _driver.FindElement(MobileBy.Id(carouselViewIndicatorId));
            var topMenuShadow = _driver.FindElement(MobileBy.Id(topMenuShadowId));
            var topMenu = _driver.FindElement(MobileBy.Id(topMenuId));
            var movieRowCollectionView = _driver.FindElement(MobileBy.Id(movieRowCollectionViewId));
            var movieRowCollectionView_movieRowList = movieRowCollectionView.FindElements(MobileBy.Id(movieRowCollectionView_movieRowClass));
            var homePageNavigationBarList = _driver.FindElements(MobileBy.Id(homePageNavigationBarId));
            var homePageItemFilterList = _driver.FindElements(MobileBy.Id(homePageItemFilterId));


        }

        [Test]
        [Order(1)]
        public void TopMenuDisappearOnScrollUp()
        {

        }

        [Test]
        [Order(2)]
        public void TopMenuReappearOnScrollDown()
        {

        }



        [Test]
        [Order(4)]
        public void SwipeOnLastMovieRowInView()
        {
            // TODO: swiping action need adjustment
            string comedy_deadpoolWolverine_poster_XPath = "//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup/androidx.recyclerview.widget.RecyclerView/android.view.ViewGroup[3]/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/androidx.recyclerview.widget.RecyclerView/android.view.ViewGroup[1]/android.view.ViewGroup/android.view.ViewGroup/android.widget.ImageView";
            string comedy_theGarfieldMovie_poster_XPath = "//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup/androidx.recyclerview.widget.RecyclerView/android.view.ViewGroup[3]/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/androidx.recyclerview.widget.RecyclerView/android.view.ViewGroup[3]/android.view.ViewGroup/android.view.ViewGroup/android.widget.ImageView";

            var calibrateCoordinate = pointerInputDevice.CreatePointerMove(CoordinateOrigin.Viewport, windowWidth / 2, windowHeight / 2, TimeSpan.Zero);
            var pressDown = pointerInputDevice.CreatePointerDown(pointerButton);
            var releasePress = pointerInputDevice.CreatePointerUp(pointerButton);

            var swipeVertical = pointerInputDevice.CreatePointerMove(CoordinateOrigin.Pointer, 0, -windowHeight, TimeSpan.FromSeconds(2));
            ActionBuilder swipeDownToComedyGenre = new ActionBuilder().AddActions(
                calibrateCoordinate,
                pressDown,
                swipeVertical,
                releasePress);
            _driver.PerformActions(swipeDownToComedyGenre.ToActionSequenceList());

            var deadpoolWolverinePoster = _driver.FindElement(MobileBy.XPath(comedy_deadpoolWolverine_poster_XPath));

            var swipeLeftOnDeadpoolWolverinePoster = pointerInputDevice.CreatePointerMove(deadpoolWolverinePoster, -windowWidth*4/3, 0, TimeSpan.FromSeconds(2));

            //var moveFingerToDeadpoolWolverinePoster = new Actions(_driver).MoveToElement(deadpoolWolverinePoster);
            ActionBuilder swipeLeftOnComedyGenre = new ActionBuilder().AddActions(
                pointerInputDevice.CreatePointerMove(deadpoolWolverinePoster, 0, 0, TimeSpan.FromSeconds(2)),
                pressDown,
                swipeLeftOnDeadpoolWolverinePoster,
                releasePress);
            _driver.PerformActions(swipeLeftOnComedyGenre.ToActionSequenceList());

            var theGarfieldMoviePoster = _driver.FindElement(MobileBy.XPath(comedy_theGarfieldMovie_poster_XPath));
            Assert.That(theGarfieldMoviePoster.Displayed, Is.True);

            theGarfieldMoviePoster.Click();

            var backgroundImage = _driver.FindElement(MobileBy.XPath("//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup/android.widget.ImageView"));

            Assert.That(backgroundImage.Displayed);
        }

        [Test]
        [Order(5)]
        public void Login()
        {

        }
    }
}
