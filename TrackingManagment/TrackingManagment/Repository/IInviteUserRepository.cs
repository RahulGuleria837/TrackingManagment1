using TrackingManagment.Identity;
using TrackingManagment.Migrations;
using TrackingManagment.Models;
using TrackingManagment.ViewModel;

namespace TrackingManagment.Repository
{
    public interface IInviteUserRepository
    {
        //TO GET USER NAME AND ID AVILABLE
        public ICollection<UserView> GetUsers(string username);

        //TO SEND INVITATION TO THE SELECT USER
        public bool CreateInvitation(string senderId, string reciverId);

        public string? GetUserIdFromToken(string userToken);


        /* public bool TakeActionOnInvitedPerson(string receiverId, string senderId, int action);
          public ICollection<InvitedUser> InvitedPersonList(string senderId);
          public ICollection<InvitedUser> InvitationComesFromUser(string userId);
          public bool ChangeInvitationStatus(string receiverId, string senderId, int status);
          ;*/
    }
}
