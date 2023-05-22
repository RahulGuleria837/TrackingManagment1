using TrackingManagment.Identity;

namespace TrackingManagment.Models
{
    public class TracingUser
    {
        public int Id { get; set; }
        public DateTime ChangeMade { get; set; }
        public int RealStateId { get; set; } = 0;
        public RealState? RealState { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
