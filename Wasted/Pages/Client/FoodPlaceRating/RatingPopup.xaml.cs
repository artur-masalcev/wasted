using System;
using System.Linq;
using DataAPI.DTO;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Wasted.Utils.Services;
using Wasted.WastedAPI;
using Wasted.WastedAPI.Business_Objects;
using Wasted.WastedAPI.Business_Objects.Users;
using Xamarin.Forms;

namespace Wasted.Pages.Client.FoodPlaceRating
{
    public partial class RatingPopup : PopupPage
    {
        public FoodPlace SelectedFoodPlace { get; set; }
        public ClientUser User { get; set; }
        public DataService DataService { get; set; }

        public int RatingBarRating => ratingBar.SelectedStarValue;
        
        public String RatingEmoji { get; set; }
        public String RatingComment { get; set; }
        private RatingDTO rating;

        /// <summary>
        /// Initialiser for RatingPopup class
        /// </summary>
        /// <param name="foodPlace">Food place to be rated</param>
        public RatingPopup(FoodPlace foodPlace)
        {
            SelectedFoodPlace = foodPlace;
            InitializeComponent();
            
            DataService = DependencyService.Get<DataService>();
            User = (ClientUser)DataService.CurrentUser;
            rating = User.Ratings.FirstOrDefault(r => r.FoodPlaceId == foodPlace.Id);
            
            if (rating != null) 
            {
                ratingBar.SelectedStarValue = (int)rating.Value; //Sets value to the user's previous rating
            }
            foodPlaceTitleLabel.BindingContext = SelectedFoodPlace;
            ratingEmoji.BindingContext = this;
            ratingComment.BindingContext = this;
        }

        private void OnConfirmClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(); // Close the popup
            if (rating != null)
            {
                RatingDTO placeRating = SelectedFoodPlace.Ratings.Find(r => r.Id == rating.Id);
                placeRating.Value = RatingBarRating;
                rating.Value = RatingBarRating; //Sets value for place and user
                DataProvider.UpdateRating(rating);
            }
            else
            {
                rating = new RatingDTO(User.Id, SelectedFoodPlace.Id, RatingBarRating);
                SelectedFoodPlace.Ratings.Add(rating);
                User.Ratings.Add(rating);
                DataProvider.CreateRating(rating);
            }
            SelectedFoodPlace.RatingChanged();
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(); // Close the popup
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
