using Wasted.Dummy_API.Business_Objects;

namespace Wasted.Utils
{
    public interface IUserService
    {
        User GetUser();
        void SetUser(User user);
    }
}