﻿using System;
using Wasted.Utils;

namespace Wasted.WastedAPI.Business_Objects
{
    public class OrderDeal : ChangeablePropertyObject
    {
        public int Id { get; set; }
        private string _status;

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        public int DealId { get; set; }

        public int Quantity { get; set; }
        public string Title { get; set; }
        public double Cost { get; set; }

        private double TimeLeft
        {
            get
            {
                double calculatedTimeLeft = TimeSpan
                    .FromMilliseconds(ExpectedFinishTime - DateTimeOffset.Now.ToUnixTimeMilliseconds())
                    .TotalMinutes;
                return calculatedTimeLeft > 0 ? calculatedTimeLeft : -1;
            }
        }
        public string DisplayableTimeLeft =>
            Status != OrderStatus.Preparing ? "-" :
            TimeLeft > 0 ? $"{Math.Ceiling(TimeLeft)} min": "ready soon";

        private long _expectedFinishTime;
        public long ExpectedFinishTime
        { 
            get => _expectedFinishTime;
            set
            {
                _expectedFinishTime = value; 
                OnPropertyChanged("DisplayableTimeLeft");
            }
        }
        public int PlaceUserId { get; set; }
        public int ClientUserId { get; set; }

        public OrderDeal(int dealId = default, string status = null, int quantity = default, int clientUserId = default,
            int dealFoodPlacePlaceUserId = default, string dealTitle = null, double dealCurrentCost = default)
        {
            DealId = dealId;
            Title = dealTitle;
            Quantity = quantity;
            ClientUserId = clientUserId;
            Cost = quantity * dealCurrentCost;
            Status = status;
            Quantity = quantity;
            PlaceUserId = dealFoodPlacePlaceUserId;
        }
    }
}