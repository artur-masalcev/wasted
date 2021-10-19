using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.FoodPlaceRatingSystem;

namespace Wasted
{
    public partial class RatingPopup : PopupPage
    {
        public FoodPlace SelectedFoodPlace { get; set; }
        public int FoodPlaceID { get; }
        public int RatingBarRating => ratingBar.SelectedStarValue;
  
        public String RatingEmoji { get; set; }
        public String RatingComment { get; set; }

        /// <summary>
        /// Initialiser for RatingPopup class
        /// </summary>
        /// <param name="foodPlace">Food place to be rated</param>
        public RatingPopup(FoodPlace foodPlace)
        {
            SelectedFoodPlace = foodPlace;
            InitializeComponent();

            foodPlaceTitleLabel.BindingContext = SelectedFoodPlace;
            ratingEmoji.BindingContext = this;
            ratingComment.BindingContext = this;
        }

        private void OnConfirmClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true); // Close the popup
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true); // Close the popup
        }

        private void OnRatingSet(object sender, EventArgs e)
        {
            UpdateAssociationView();
        }

        /// <summary>
        /// Updates all the text fields according to 'Rating' value
        /// </summary>
        private void UpdateAssociationView()
        {
            ratingEmoji.Text = RatingToAssociation.ConvertToEmoji(RatingBarRating);
            ratingComment.Text = RatingToAssociation.ConvertToComment(RatingBarRating);
        }
    }
}
