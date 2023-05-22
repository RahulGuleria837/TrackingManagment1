using TrackingManagment.Identity;

namespace TrackingManagment.Models
{
    public class InvitedUser
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;      
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
