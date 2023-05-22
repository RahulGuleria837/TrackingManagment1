using TrackingManagment.Identity;
using TrackingManagment.LoginViewModel;
using TrackingManagment.ViewModel;

namespace TrackingManagment.Services
{
    public interface IUserService
    {

         Task<bool> Register(RegisterView register);
        Task<ApplicationUser> AuhthenticateUser(LoginView login);

        Task<bool> IsUnique(string Email);
        ApplicationUser CreateToken(ApplicationUser? user);


    }
}

