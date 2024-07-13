using APV.Views;

namespace APV
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(StorePage), typeof(StorePage));
            Routing.RegisterRoute(nameof(DownloadsPage), typeof(DownloadsPage));
            Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));

            Routing.RegisterRoute(nameof(MovieDetailsPage), typeof(MovieDetailsPage));
        }
    }
}
