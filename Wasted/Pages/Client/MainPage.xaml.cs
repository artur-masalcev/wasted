using Xamarin.Forms;

namespace Wasted.Pages.Client
{
    /// <summary>
    /// Core page of the app, manages transitioning between other screens
    /// </summary>
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            SelectedTabColor = Color.Black;
        }
    }
}