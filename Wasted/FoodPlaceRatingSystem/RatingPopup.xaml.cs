using System;
using Rg.Plugins.Popup.Services;
using Wasted.DummyAPI.BusinessObjects;
using Wasted.FoodPlaceRatingSystem;

namespace Wasted
{
    public partial class RatingPopup : Rg.Plugins.Popup.Pages.PopupPage
    {

        public String FoodPlaceTitle { get; }
        public int FoodPlaceID { get; }
        public int Rating => ratingBar.SelectedStarValue;
        public String RatingEmoji { get; set; }
        public String RatingComment { get; set; }

        /// <summary>
        /// Initialiser for RatingPopup class
        /// </summary>
        /// <param name="foodPlace">Food place to be rated</param>
        public RatingPopup(FoodPlace foodPlace)
        {
            this.FoodPlaceTitle = foodPlace.Title;
            this.FoodPlaceID = foodPlace.ID;

            InitializeComponent();

            BindingContext = this;
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
            ratingEmoji.Text = RatingToAssociation.ConvertToEmoji(Rating);
            ratingComment.Text = RatingToAssociation.ConvertToComment(Rating);
        }
    }
}
