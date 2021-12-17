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
        public CurrentUserService CurrentUserService { get; set; }

        public int RatingBarRating => RatingBar.SelectedStarValue;

        public String RatingEmoji { get; set; }
        public String RatingComment { get; set; }
        private RatingDTO _rating;

        /// <summary>
        /// Initialiser for RatingPopup class
        /// </summary>
        /// <param name="foodPlace">Food place to be rated</param>
        public RatingPopup(FoodPlace foodPlace)
        {
            SelectedFoodPlace = foodPlace;
            InitializeComponent();

            CurrentUserService = DependencyService.Get<CurrentUserService>();
            User = (ClientUser) CurrentUserService.CurrentUser;
            _rating = User.Ratings.FirstOrDefault(r => r.FoodPlaceId == foodPlace.Id);

            if (_rating != null)
            {
                RatingBar.SelectedStarValue = (int) _rating.Value; //Sets value to the user's previous rating
            }

            FoodPlaceTitleLabel.BindingContext = SelectedFoodPlace;
            RatingEmojiLabel.BindingContext = this;
            RatingCommentLabel.BindingContext = this;
        }

        private void OnConfirmClicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(); // Close the popup
            if (_rating != null)
            {
                RatingDTO placeRating = SelectedFoodPlace.Ratings.Find(r => r.Id == _rating.Id);
                placeRating.Value = RatingBarRating;
                _rating.Value = RatingBarRating; //Sets value for place and user
                DataProvider.UpdateRating(_rating);
            }
            else
            {
                _rating = new RatingDTO(User.Id, SelectedFoodPlace.Id, RatingBarRating);
                SelectedFoodPlace.Ratings.Add(_rating);
                User.Ratings.Add(_rating);
                DataProvider.CreateRating(_rating);
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
            RatingEmojiLabel.Text = RatingToAssociation.ConvertToEmoji(RatingBarRating);
            RatingCommentLabel.Text = RatingToAssociation.ConvertToComment(RatingBarRating);
        }
    }
}