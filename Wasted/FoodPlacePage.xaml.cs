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

        /// <summary>
        /// Sets binding for xaml file.
        /// </summary>
        public void InitializeViews()
        {
            contentScrollView.BindingContext = SelectedFoodPlace;
            dealsCollectionView.ItemsSource = SelectedFoodPlace.Deals;
            InitializeStars();
        }

        /// <summary>
        /// Goes to navigation page.
        /// </summary>
        void DealsCollectionViewListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Deal selectedDeal = e.CurrentSelection.FirstOrDefault() as Deal;
            Navigation.PushAsync(new ItemsPage(selectedDeal));
        }

        /// <summary>
        /// Shows rating popup.
        /// </summary>
        private void OnRateButtonClicked(object sender, EventArgs e)
        {
            ratingView.IsVisible = true;
        }

        private void ClosePopup()
        {
            ratingView.IsVisible = false;
        }
        private void OnCancelClicked(object sender, EventArgs e)
        {
            ClosePopup();
        }

        /// <summary>
        /// Allows user to rethink their given rating. Called if the rating was already chosen.
        /// </summary>
        private void ResetRating() //TODO: handle when user exits and reenters page.
        {
            --SelectedFoodPlace.RatingCount;
            SelectedFoodPlace.Rating =
                (SelectedFoodPlace.Rating * (SelectedFoodPlace.RatingCount + 1) - previousRating) / SelectedFoodPlace.RatingCount * (SelectedFoodPlace.RatingCount + 1)
                - SelectedFoodPlace.Rating * SelectedFoodPlace.RatingCount;
            --SelectedFoodPlace.RatingCount;
        }

        /// <summary>
        /// Sets new rating.
        /// </summary>
        private void OnConfirmClicked(object sender, EventArgs e)
        {
            ClosePopup();
            if (previousRating != -1)
                ResetRating();

            SelectedFoodPlace.Rating = selectedRating;
            previousRating = selectedRating;
        }

        /// <summary>
        /// Creates listeners for stars to get selected value and show the stars accordingly.
        /// </summary>
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