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
using TrackingManagment.Migrations;
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
        

        //TO CHANGE THE INVITATION STATUS WHEN THE USER ACCEPTED THE REQUEST
        public bool ChangeInvitationStatus(string receiverId, string senderId, int status)
        {
            var sender = _userManager.FindByIdAsync(senderId).Result;
            var reciver = _userManager.FindByIdAsync(receiverId).Result;
            if (reciver == null || sender == null) return false;
            var findInvitation = _context.invitedUsers.FirstOrDefault(i => i.InvitationSenderUserId == senderId && i.InvitationReceiverUserId == receiverId);
            if (findInvitation == null) return false;
            findInvitation.Status = (Status)status;
            if (findInvitation.Status == Status.Approved)
           
                findInvitation.Action = Models.Action.Enable;

            _context.Update(findInvitation);
            return _context.SaveChanges() == 1 ? true : false;
        } 

        //TO CHANGE OR UPDATAE THE USER REQUEST
        public bool UpdateInvitationAction(string receiverID, string senderId, int action)
        {
            var sender = _userManager.FindByIdAsync(senderId).Result;
            var receiver = _userManager.FindByIdAsync(receiverID).Result;
            if(receiver == null || sender == null) return false;
            var findInvitation = _context.invitedUsers.FirstOrDefault(m => m.InvitationSenderUserId == senderId && m.InvitationReceiverUserId == receiverID);
            if (findInvitation == null) { return false; }
                findInvitation.Action= (Models.Action)action;

            if (findInvitation.Action == Models.Action.Deleted) 
            {
                _context.Remove(findInvitation);
                return _context.SaveChanges() == 1 ? true:false;
            }

            _context.Update(findInvitation);

            return _context.SaveChanges()==1 ? true : false;
        }

        public  bool CreateInvitation(string senderId, string reciverId)
        {

            var validatasender = _userManager.FindByIdAsync(senderId).Result;
            var validateReceiver = _userManager.FindByIdAsync(reciverId).Result;
            if (validateReceiver == null || validatasender == null)
                return false;
            


            //if (senderId == null && reciverId == null) throw new ArgumentNullException();
            InvitedUser invitedUser = new InvitedUser
            {
                InvitationSenderUserId = senderId,
                InvitationReceiverUserId = reciverId,
               InvitationSenderName=validatasender.UserName,
               InvitationReciverName=validateReceiver.UserName,
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

        public  ICollection<InvitedUser> GetAll()
        {
            var getAll =  _context.invitedUsers.ToList();
            return getAll;
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
            return _context.Set<InvitedUser>().Include(u => u.ApplicationUserSender)
            .Where(u => u.InvitationReceiverUserId == userId && u.Status == Status.Approved)
            .Select(u => new InvitedUser()
            {   
                InvitationSenderUserId = u.InvitationSenderUserId,
                InvitationSenderName = u.ApplicationUserSender.UserName,
            
                Action = u.Action,
                Status = u.Status
            }
           )
            .ToList();
        }

        //To get all data to register invited person
        public ICollection<InvitedUser> GetAllRegisteredPersons(string userId)
        {
            var data = _context.Set<InvitedUser>().Include(m => m.ApplicationUserReceiver).Where(u => u.InvitationSenderUserId == userId).
                Select(u => new InvitedUser()
                {
                    InvitationReceiverUserId = u.InvitationReceiverUserId,
                    InvitationSenderUserId = u.InvitationSenderUserId,
                    /*ApplicationUserReceiver = new ApplicationUser()
                    {*/
                        UserNameRec = u.ApplicationUserReceiver.UserName,
                    UserNamesen = u.ApplicationUserSender.UserName,
                   /* },*/
                    Action = u.Action,
                    Status = u.Status
                }).
                ToList();
            return data;
        }
    }




}

