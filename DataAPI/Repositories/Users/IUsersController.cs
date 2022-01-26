using DataAPI.Models.Users;

namespace DataAPI.Repositories.Users
{ 
    public interface IUsersController<out T>
    {
        public T GetByUsernameAndPassword (string username, string password);
    }
    
}