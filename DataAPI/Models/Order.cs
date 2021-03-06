using DataAPI.Models.Users;

namespace DataAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
        public long ExpectedFinishTime { get; set; }

        public int DealId { get; set; }
        public Deal Deal { get; set; }
        public int ClientUserId { get; set; }
        public ClientUser ClientUser { get; set; }
    }
}