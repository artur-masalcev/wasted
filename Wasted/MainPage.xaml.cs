using Xamarin.Forms;

namespace Wasted
{
    /// <summary>
    /// Core screen of the app, manages transitioning between other screens
    /// </summary>
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            InitializeImageIcons();
        }

        /// <summary>
        /// Adds icons to navigation bar.
        /// </summary>
        public void InitializeImageIcons()
        {
            var assembly = typeof(MainPage); //Find if program runs on android or on ios and adjust images accordingly
            homePage.IconImageSource = ImageSource.FromResource("Wasted.Images.Icons.outline_home_black_48dp.png", assembly);
            searchPage.IconImageSource = ImageSource.FromResource("Wasted.Images.Icons.outline_search_black_48dp.png", assembly);
        }


    }
}
