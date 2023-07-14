using Microsoft.AspNetCore.Identity;
using TrackingManagment.Identity;

namespace TrackingManagment.Models
{
    public class UnitOfWork
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UnitOfWork(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public ApplicationUser? CheckPersonsId(string userId)
        {
            var validatasender = _userManager.FindByIdAsync(userId).Result;
            return validatasender;
        }
    }
}
