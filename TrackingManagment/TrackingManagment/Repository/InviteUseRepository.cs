using Azure.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using TrackingManagment.Identity;
using TrackingManagment.Models;
using TrackingManagment.ViewModel;

namespace TrackingManagment.Repository
{
    public class InviteUseRepository : IInviteUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public InviteUseRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public bool CreateInvitation(string senderId, string reciverId)
        {
            throw new NotImplementedException();
        }

        public ICollection<UserView> GetUsers(string username)
        {

            //var data = _userManager.FindByIdAsync(senderId).Result;
            var usersInDb = _userManager.Users;
            return usersInDb.Where(u => u.UserName.Contains(username)).Select(m => new UserView() { UserId = m.Id, UserName = m.UserName }).ToList();

            /*ApplicationUser user = new ApplicationUser();
            if (username == null) throw new ArgumentNullException("username");
            user = _context.Users.FirstOrDefault(user => user.UserName == username);
            if (user == null) throw new ArgumentNullException();
            return Task.FromResult<ApplicationUser>(user);*/
        }

       
    }
}
