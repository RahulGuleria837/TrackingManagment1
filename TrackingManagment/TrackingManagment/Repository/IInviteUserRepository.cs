using TrackingManagment.Identity;
using TrackingManagment.Models;
using TrackingManagment.ViewModel;

namespace TrackingManagment.Repository
{
    public interface IInviteUserRepository
    {

        public ICollection<UserView> GetUsers(string username);

        public bool CreateInvitation(string senderId, string reciverId);
    }
}
