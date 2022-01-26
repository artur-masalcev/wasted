namespace DataAPI.Models.Users
{
    public abstract class AbstractUser
    {
        public int Id { get; set; }
        
        public abstract string UserType { get; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}