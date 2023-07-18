using System.ComponentModel;
using TrackingManagment.Identity;
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

        public bool ChangeInvitationStatus(string receiverId, string senderId, int action);

        ICollection<InvitedUser> GetAll();

        public bool UpdateInvitationAction(string receiverID,string senderId,int status);

        public ICollection<InvitedUser> InvitationComesFromUser(string userId);
        public ICollection<InvitedUser> GetAllRegisteredPersons(string userId);





    }
}
