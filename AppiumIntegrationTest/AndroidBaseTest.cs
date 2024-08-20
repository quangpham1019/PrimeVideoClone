using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interactions;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Interactions;

using PointerInputDevice = OpenQA.Selenium.Appium.Interactions.PointerInputDevice;

namespace AppiumIntegrationTest
{
    public class AndroidBaseTest
    {
        protected AndroidDriver _driver;
        protected AppiumOptions driverOptions;
        protected AppiumLocalService appiumLocalServer;

        protected PointerKind pointerKind = PointerKind.Touch;
        protected PointerButton pointerButton = PointerButton.TouchContact;
        protected PointerInputDevice pointerInputDevice;
        protected int windowHeight, windowWidth;
        
        int Port { get; set; }
        string PlatformVersion { get; set; }
        string AvdUdid { get; set; }

        public AndroidBaseTest(string platformVersion, string avdUdid, int port)
        {
            PlatformVersion = platformVersion;
            AvdUdid = avdUdid;
            Port = port;
        }

        [OneTimeSetUp]
        public void SetUp()
        {
            StartAppiumServer();
            InitializeDriverOptions();
            InitializeDriver();

            pointerInputDevice = new(pointerKind);
            windowWidth = _driver.Manage().Window.Size.Width;
            windowHeight = _driver.Manage().Window.Size.Height;
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            _driver.Dispose();
            appiumLocalServer.Dispose();
        }

        void StartAppiumServer()
        {
            appiumLocalServer = new AppiumServiceBuilder()
                .UsingPort(Port)
                .Build();
            appiumLocalServer.Start();
        }
        void InitializeDriverOptions()
        {
            driverOptions = new AppiumOptions();

            driverOptions.AutomationName = AutomationName.AndroidUIAutomator2;
            driverOptions.PlatformName = "Android";
            driverOptions.App = "C:\\Users\\Quang Pham\\source\\repos\\DOTNET MAUI\\APV\\APV\\bin\\Debug\\net8.0-android\\com.companyname.apv-Signed.apk";

            driverOptions.PlatformVersion = PlatformVersion;
            driverOptions.AddAdditionalAppiumOption(MobileCapabilityType.Udid, AvdUdid);
            //driverOptions.AddAdditionalAppiumOption("appPackage", "com.companyname.apv-Signed");
        }
        void InitializeDriver()
        {
            _driver = new AndroidDriver(appiumLocalServer.ServiceUrl, driverOptions, TimeSpan.FromSeconds(180));
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
    }
}
