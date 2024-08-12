using APV.ViewModels;

namespace APV.Views;

public partial class HomePage : ContentPage
{
    private readonly HomePageViewModel homePageViewModel;
    private double PrevScrollY { get; set; } = 0;

   
    public HomePage(HomePageViewModel homePageViewModel)
    {
        InitializeComponent();
        BindingContext = homePageViewModel;
        this.homePageViewModel = homePageViewModel;

        DisplayInfo currentDeviceDisplayInfo = DeviceDisplay.Current.MainDisplayInfo;

        // TODO: add event handler to update deviceDisplayInfo when the app window is resized
#if WINDOWS
            homePageViewModel.DeviceDisplayInfo = new DisplayInfo(
                Shell.Current.Window.Width,
                Shell.Current.Window.Height * .7,
                currentDeviceDisplayInfo.Density,
                currentDeviceDisplayInfo.Orientation,
                currentDeviceDisplayInfo.Rotation
                );
#endif

#if !WINDOWS
        homePageViewModel.DeviceDisplayInfo = new DisplayInfo(
            Shell.Current.Window.Width,
            Shell.Current.Window.Height * .3,
            currentDeviceDisplayInfo.Density,
            currentDeviceDisplayInfo.Orientation,
            currentDeviceDisplayInfo.Rotation);
#endif
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (homePageViewModel.CurrentUser is null)
        {
            accountBtn.IsVisible = true;
            accountLogoBtn.IsVisible = false;
        }
        else
        {
            accountBtn.IsVisible = false;
            accountLogoBtn.IsVisible = true;
        }
    }

    private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
    {

        double curScrollY = e.ScrollY;
        bool menuIsVisible = label1.IsVisible;

        if (Math.Abs(curScrollY - PrevScrollY) > 20)
        {
            // if scrolling down, curScrollY > prevScrollY && menu is visible
                // hide menu, menu visible = false
            // if scrolling up && menu is not visible
                // show menu, menu visible = true
            if (curScrollY > PrevScrollY && menuIsVisible == true)
            {
                label1.IsVisible = false;
                label2.IsVisible = false;
            }
            else if (curScrollY < PrevScrollY && menuIsVisible == false)
            {
                label1.IsVisible = true;
                label2.IsVisible = true;
            }
        }

        PrevScrollY = e.ScrollY;
    }
}