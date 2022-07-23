using Domain;

namespace Services
{
    public interface IAuthService
    {
        void CreateUser(AppUser user);

        void UpdateUser(AppUser user);
    }
}