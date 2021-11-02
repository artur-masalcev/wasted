﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasted.Dummy_API.Business_Objects;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Pages.Place.NewDeal;
using Wasted.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wasted.Pages.Place
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceHomePage : ContentPage
    {
        private DataService service;
        public UserPlace CurrentUser { get; set; }
        public IEnumerable<FoodPlace> OwnedPlaces { get; set; }
        public PlaceHomePage()
        {
            service = DependencyService.Get<DataService>();
            CurrentUser = (UserPlace)service.CurrentUser;
            BindingContext = this;
            OwnedPlaces = CurrentUser.OwnedPlaceIDs.Select(id => service.AllFoodPlaces[id - 1]);
            InitializeComponent();
        }

        private void YourPlacesCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChanger.ListSelectionChanged(sender, e, () =>
            {
                FoodPlace selectedPlace = e.CurrentSelection.FirstOrDefault() as FoodPlace;
                Navigation.PushAsync(new NewDealPage(selectedPlace));
            });
        }

        private void AddNewPlaceButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewPlacePage());
        }
    }
}