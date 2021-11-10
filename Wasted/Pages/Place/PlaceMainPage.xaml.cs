using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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