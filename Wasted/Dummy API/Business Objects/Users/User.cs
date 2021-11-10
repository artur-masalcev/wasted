using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Wasted.Utils;
using Xamarin.Forms;

namespace Wasted.Dummy_API.Business_Objects
{
    public abstract class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public Dictionary<int, int> Ratings { get; set; }

        [JsonConstructor]
        protected User(string username = null, string password = null, string name = null,
            string surname = null, string city = null, string address = null,
            Dictionary<int, int> ratings = null)
        {
            Username = username;
            Password = password;
            Name = name;
            Surname = surname;
            City = city;
            Address = address;           
            Ratings = ratings ?? new Dictionary<int, int>();
        }
        public abstract void PushPage(ContentPage page);
        public abstract void CreateUser(DataService service);
    }
}
