using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interactions;
using OpenQA.Selenium.Interactions;
using Assert = NUnit.Framework.Assert;

namespace APV.Test
{
    public class APV_AppiumTest
    {
        private AndroidDriver<AndroidElement> _driver;
        PointerKind pointerKind = PointerKind.Touch;
        PointerButton pointerButton = PointerButton.TouchContact;
        OpenQA.Selenium.Appium.Interactions.PointerInputDevice pointerInputDevice;
        int windowHeight, windowWidth;

        [OneTimeSetUp]
        public void SetUp()
        {
            var serverUri = new Uri(Environment.GetEnvironmentVariable("APPIUM_HOST") ?? "http://127.0.0.1:4723/");

            var driverOptions = new AppiumOptions();

            driverOptions.AddAdditionalCapability("appium:automationName", AutomationName.AndroidUIAutomator2);
            driverOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Pixel 5 - API 34");
            driverOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            driverOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "14.0");

            driverOptions.AddAdditionalCapability("appPackage", "com.companyname.apv-Signed");
            driverOptions.AddAdditionalCapability("appium:app", "C:\\Users\\Quang Pham\\source\\repos\\APV\\APV\\bin\\Debug\\net8.0-android\\com.companyname.apv-Signed.apk");
            // NoReset assumes the app com.google.android is preinstalled on the emulator
            driverOptions.AddAdditionalCapability("noReset", true);

            _driver = new AndroidDriver<AndroidElement>(serverUri, driverOptions, TimeSpan.FromSeconds(180));
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            pointerInputDevice = new(pointerKind);
            windowWidth = _driver.Manage().Window.Size.Width;
            windowHeight = _driver.Manage().Window.Size.Height;
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver.Dispose();
        }

        [Test]
        public void TestOnPosterClick()
        {
            var godFather_poster_xPath = "//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup/androidx.recyclerview.widget.RecyclerView[2]/android.view.ViewGroup[1]/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/androidx.recyclerview.widget.RecyclerView/android.view.ViewGroup[2]/android.view.ViewGroup/android.view.ViewGroup/android.widget.ImageView";
            var godFather_textOverview_xPath = "//android.widget.TextView[@text=\"Spanning the years 1945 to 1955, a chronicle of the fictional Italian-American Corleone crime family. When organized crime family patriarch, Vito Corleone barely survives an attempt on his life, his youngest son, Michael steps in to take care of the would-be killers, launching a campaign of bloody revenge.\"]";
            var godFather_moreDetailsBtn_xPath = "//android.widget.TextView[@text=\"More Details\"]";
            var godFather_relatedTab_randomRelatedMoviePoster_xPath = "//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup[4]/android.view.ViewGroup/android.view.ViewGroup[15]/android.widget.ImageView";
            var godFather_moreDetailsTab_starringLabel_xPath = "//android.widget.TextView[@text=\"Starring\"]";

            var godFather_Poster = _driver.FindElementByXPath(godFather_poster_xPath);
            godFather_Poster.Click();

            var godFather_textOverview = _driver.FindElementByXPath(godFather_textOverview_xPath);
            var godFather_moreDetailsBtn = _driver.FindElementByXPath(godFather_moreDetailsBtn_xPath);

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

            var godFather_relatedTab_randomRelatedMoviePoster = _driver.FindElementByXPath(godFather_relatedTab_randomRelatedMoviePoster_xPath);
            Assert.Multiple(() =>
            {
                Assert.That(godFather_textOverview.Displayed, Is.True);
                Assert.That(godFather_moreDetailsBtn.Displayed, Is.True);
                Assert.That(godFather_relatedTab_randomRelatedMoviePoster.Displayed, Is.True);
            });

            godFather_moreDetailsBtn = _driver.FindElementByXPath(godFather_moreDetailsBtn_xPath);
            godFather_moreDetailsBtn.Click();

            var godFather_moreDetailsTab_starringLabel = _driver.FindElementByXPath(godFather_moreDetailsTab_starringLabel_xPath);
            Assert.Multiple(() =>
            {
                Assert.That(godFather_textOverview.Displayed, Is.True);
                Assert.That(godFather_moreDetailsBtn.Displayed, Is.True);
                Assert.That(godFather_moreDetailsTab_starringLabel.Displayed, Is.True);
            });
        }

        //[Test]
        //public void SwipingOnMovieList()
        //{

        //    string comedy_deadpoolWolverine_poster_XPath = "//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup/androidx.recyclerview.widget.RecyclerView/android.view.ViewGroup[4]/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/androidx.recyclerview.widget.RecyclerView/android.view.ViewGroup[1]/android.view.ViewGroup/android.view.ViewGroup/android.widget.ImageView";
        //    string comedy_theGarfieldMovie_poster_XPath = "//android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup/androidx.recyclerview.widget.RecyclerView/android.view.ViewGroup[3]/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/androidx.recyclerview.widget.RecyclerView/android.view.ViewGroup[3]/android.view.ViewGroup/android.view.ViewGroup/android.widget.ImageView";

        //    var calibrateCoordinate = pointerInputDevice.CreatePointerMove(CoordinateOrigin.Viewport, windowWidth/2, 1600, TimeSpan.Zero);
        //    var pressDown = pointerInputDevice.CreatePointerDown(pointerButton);
        //    var releasePress = pointerInputDevice.CreatePointerUp(pointerButton);

        //    var swipeVertical = pointerInputDevice.CreatePointerMove(CoordinateOrigin.Pointer, 0, -windowHeight, TimeSpan.FromSeconds(2));
        //    ActionBuilder swipeDownToComedyGenre = new ActionBuilder()
        //        .AddActions(calibrateCoordinate, pressDown, swipeVertical, releasePress);
        //    _driver.PerformActions(swipeDownToComedyGenre.ToActionSequenceList());

        //    var deadpoolWolverinePoster = _driver.FindElementByXPath(comedy_deadpoolWolverine_poster_XPath);
        //    var swipeLeftOnDeadpoolWolverinePoster = pointerInputDevice.CreatePointerMove(deadpoolWolverinePoster, -windowWidth, 0, TimeSpan.FromSeconds(2));
        //    ActionBuilder swipeLeftOnComedyGenre = new ActionBuilder().AddActions(calibrateCoordinate, pressDown, swipeLeftOnDeadpoolWolverinePoster, releasePress);
        //    _driver.PerformActions(swipeLeftOnComedyGenre.ToActionSequenceList());

        //    var theGarfieldMoviePoster = _driver.FindElementByXPath(comedy_theGarfieldMovie_poster_XPath);
        //    Assert.That(theGarfieldMoviePoster.Displayed, Is.True);

        //    theGarfieldMoviePoster.Click();
        //    Assert.That(1==1, Is.True);
        //}
    }
}
