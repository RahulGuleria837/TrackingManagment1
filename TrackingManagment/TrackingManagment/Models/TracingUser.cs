using System.ComponentModel.DataAnnotations;
using TrackingManagment.Identity;

namespace TrackingManagment.Models
{
    public class TracingUser
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime ChangeTracktime { get; set; }=DateTime.Now;
        public int RealStateId { get; set; } = 0;
        public RealState? RealState { get; set; }
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? ApplicationUser { get; set; }
        public string DataChangeId { get; set; } = string.Empty;
        public ApplicationUser? DataChangeUser { get; set; }
        public Action UserActions { get; set; }
        public enum Action
        {
            Add = 1,
            Update = 2,
            Delete = 3
        };
    }
}
