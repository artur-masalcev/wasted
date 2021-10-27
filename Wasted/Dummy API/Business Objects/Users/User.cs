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
        public Dictionary<int, int> Ratings { get; set; } = new Dictionary<int, int>();

        [JsonConstructor]
        protected User(string username, string password, string name, string surname,
            string city, string address, Dictionary<int, int> ratings)
        {
            Username = username;
            Password = password;
            Name = name;
            Surname = surname;
            City = city;
            Address = address;           
            Ratings = ratings;
        }

        protected User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        protected User()
        {
        }
        public abstract void PushPage(ContentPage page);
        public abstract void CreateUser(DataService service);
    }
}
