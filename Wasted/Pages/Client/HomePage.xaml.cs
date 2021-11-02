﻿using System.Collections.Generic;
using System.Linq;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.DummyDataAPI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using System;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Threading;
using Wasted.Utils;

namespace Wasted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class HomePage : ContentPage
    {
        private DataService service;
        
        const int MaxRadiusInKilometers = 50;
        const int MaxNearbyPlacesCount = 10;
        const int MaxSpecialOffersCount = 10;
        const int MaxPopularPlacesCount = 10;

        public HomePage()
        {
            service = DependencyService.Get<DataService>();
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        /// <summary>
        /// Puts food places from json file to collectionView.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            nearbyFoodPlacesCollectionView.ItemsSource =
                DummyAPI.DataFilters.GetNearbyPlaces(service.AllFoodPlaces, service.UserLocation, MaxNearbyPlacesCount, MaxRadiusInKilometers);

            specialOffersCollectionView.ItemsSource =
                DummyAPI.DataFilters.GetSpecialOffers(service.AllDeals, MaxSpecialOffersCount);

            popularFoodPlacesCollectionView.ItemsSource =
                DummyAPI.DataFilters.GetPopularFoodPlaces(service.AllFoodPlaces, MaxPopularPlacesCount);
        }

        /// <summary>
        /// Function is called when user selects food place from nearby food places collectionView. Called from xaml file.
        /// </summary>
        private void FoodPlacesCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChanger.ListSelectionChanged(sender, e, () =>
            {
                FoodPlace selectedPlace = e.CurrentSelection.FirstOrDefault() as FoodPlace;
                Navigation.PushAsync(new FoodPlacesPage(selectedPlace));
            });
        }

        /// <summary>
        /// Function is called when user selects deal from nearby food places collectionView. Called from xaml file.
        /// </summary>
        private void DealsCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChanger.ListSelectionChanged(sender, e, () =>
            {
                Deal selectedDeal = e.CurrentSelection.FirstOrDefault() as Deal;
                Navigation.PushAsync(new ItemsPage(selectedDeal));
            });
        }
    }
}