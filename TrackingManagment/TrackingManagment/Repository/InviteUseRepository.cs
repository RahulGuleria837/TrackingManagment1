using Azure.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IdentityModel.Tokens.Jwt;
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
        private readonly IEmailService _emailService;

        public InviteUseRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
        }

        public bool ChangeInvitationStatus(string receiverId, string senderId, int status)
        {
            throw new NotImplementedException();
        }

        public  bool CreateInvitation(string senderId, string reciverId)
        {

            var validatasender = _userManager.FindByIdAsync(senderId).Result;
            var validateReceiver = _userManager.FindByIdAsync(reciverId).Result;
            if (validateReceiver == null || validatasender == null)
                return false;
            


            if (senderId == null && reciverId == null) throw new ArgumentNullException();
            InvitedUser invitedUser = new InvitedUser
            {
                InvitationSenderUserId = senderId,
                InvitationReceiverUserId = reciverId,
                Status = Status.Pending,
                Action = Models.Action.Disable
            };
            _context.invitedUsers.Add(invitedUser);
            var saveInvitation = _context.SaveChanges() == 1 ? true : false;
           

            EmailModel email = new EmailModel()
            {
                Token= senderId,
                UserID= reciverId,
                Username= validateReceiver.Email
            };


             _emailService.SendEmail(email);
          
            return true;
        }


        public string? GetUserIdFromToken(string userToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var data = tokenHandler.ReadJwtToken(userToken);
            var userId = data.Claims.FirstOrDefault(x => x.Type == "unique_name")?.Value;
            return userId;
        }


        //To get the user from frontend
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

        public ICollection<InvitedUser> InvitationComesFromUser(string userId)
        {
            throw new NotImplementedException();
        }

        public ICollection<InvitedUser> InvitedPersonList(string senderId)
        {
            throw new NotImplementedException();
        }

        public bool TakeActionOnInvitedPerson(string receiverId, string senderId, int action)
        {
            throw new NotImplementedException();
        }
    }
}
