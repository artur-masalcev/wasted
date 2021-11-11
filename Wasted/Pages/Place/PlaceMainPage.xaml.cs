using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Place
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceMainPage : TabbedPage
    {
        public PlaceMainPage()
        {
            InitializeComponent();
            SelectedTabColor = Color.Black;
        }
    }
}