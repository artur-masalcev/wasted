using Newtonsoft.Json;
using Xamarin.Forms;

namespace Wasted.WastedAPI.Business_Objects.Users
{
    public abstract class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int Id { get; set; }

        [JsonConstructor]
        protected User(string username = null, string password = null, string name = null,
            string surname = null, string city = null, string address = null, int id = default)
        {
            Username = username;
            Password = password;
            Name = name;
            Surname = surname;
            City = city;
            Address = address;
            Id = id;
        }

        public abstract void InitializePage(Page page);
        public abstract void CreateUser();
    }
}