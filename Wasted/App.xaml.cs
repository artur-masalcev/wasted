using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wasted
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Sharpnado.HorizontalListView.Initializer.Initialize(true, false);
            //Main page manages all the other pages.
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
