﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasted.DummyAPI.BusinessObjects;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodPlacesPage : ContentPage
    {
        public FoodPlace SelectedFoodPlace { get; set;}
        public FoodPlacesPage(FoodPlace selectedFoodPlace)
        {
            SelectedFoodPlace = selectedFoodPlace;

            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);

            InitializeViews();
        }

        public void InitializeViews()
        {
            contentScrollView.BindingContext = SelectedFoodPlace;
            dealsCollectionView.ItemsSource= SelectedFoodPlace.Deals;
        }
        void DealsCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDealsSelectionData(e.CurrentSelection);
        }

        void UpdateDealsSelectionData(IEnumerable<object> currentSelectedDeal)
        {
            Deal selectedDeal = currentSelectedDeal.FirstOrDefault() as Deal;
            Navigation.PushAsync(new ItemsPage(selectedDeal));
            //TODO: pass selectedPlace to Deal activity.
        }
    }
}