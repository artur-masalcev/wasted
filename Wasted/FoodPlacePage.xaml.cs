using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.Utils;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace Wasted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodPlacesPage : ContentPage
    {
        public FoodPlace SelectedFoodPlace { get; set; }
        private int previousRating = -1;
        private int selectedRating;
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
            dealsCollectionView.ItemsSource = SelectedFoodPlace.Deals;
            InitializeStars();
        }
        void DealsCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDealsSelectionData(e.CurrentSelection);
        }

        void UpdateDealsSelectionData(IEnumerable<object> currentSelectedDeal)
        {
            Deal selectedDeal = currentSelectedDeal.FirstOrDefault() as Deal;
            Navigation.PushAsync(new ItemsPage(selectedDeal));
        }

        private void OnRateButtonClicked(object sender, EventArgs e)
        {
            popupLoadingView.IsVisible = true;
        }

        private void ClosePopup()
        {
            popupLoadingView.IsVisible = false;
        }
        private void OnCancelClicked(object sender, EventArgs e)
        {
            ClosePopup();
        }

        private void ResetRating() //TODO: handle when user exits and reenters page.
        {
            --SelectedFoodPlace.RatingCount;
            SelectedFoodPlace.Rating =
                (SelectedFoodPlace.Rating * (SelectedFoodPlace.RatingCount + 1) - previousRating) / SelectedFoodPlace.RatingCount * (SelectedFoodPlace.RatingCount + 1)
                - SelectedFoodPlace.Rating * SelectedFoodPlace.RatingCount;
            --SelectedFoodPlace.RatingCount;
        }

        private void OnConfirmClicked(object sender, EventArgs e)
        {
            ClosePopup();
            if (previousRating != -1)
                ResetRating();

            SelectedFoodPlace.Rating = selectedRating;
            ratingSpan.Text = string.Format("{0:F1}", SelectedFoodPlace.Rating); //TODO: add view models.
            previousRating = selectedRating;
        }

        private void InitializeStars()
        {
            IList<View> stars = starsLayout.Children;
            
            for (int i = 0; i < stars.Count; ++i)
            {
                Label star = (Label)stars[i];
                var starClickListener = new TapGestureRecognizer();
                int rating = i + 1;
                starClickListener.Tapped += (sender, e) =>
                {
                    for (int j = 0; j < rating; ++j)
                        ((Label)stars[j]).Text = "✭";
                    for (int j = rating; j < stars.Count; ++j)
                        ((Label)stars[j]).Text = "☆";
                    selectedRating = rating;
                };
                star.GestureRecognizers.Add(starClickListener);
            }
        }
    }  

}