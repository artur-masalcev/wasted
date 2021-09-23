using Xamarin.Forms;

namespace Wasted
{
    /// <summary>
    /// Core page of the app, manages transitioning between other screens
    /// </summary>
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            this.SelectedTabColor = Color.Black;
        }

    }
}
