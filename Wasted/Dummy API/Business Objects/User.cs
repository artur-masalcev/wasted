using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wasted.Dummy_API.Business_Objects
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public Dictionary<int, int> Ratings { get; set; } = new Dictionary<int, int>();

        [JsonConstructor]
        public User(string username, string password, string name, string surname,
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

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
