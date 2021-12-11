using System;
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
            MessagingCenter.Subscribe<Object, int>(this, "click", ((arg, idx) => {
                // idx the index of pages in tabbed that you want to navigate
                CurrentPage = this.Children[idx];
            }));
        }
    }
}