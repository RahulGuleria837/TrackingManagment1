using TrackingManagment.Identity;

namespace TrackingManagment.Models
{
    public class TrackingOutput
    {
        public string TrackingId { get; set; }

        public int realStateId { get; set; } = 0;
        public RealState? RealState { get; set; }
        public DateTime TrackingDate { get; set; } = DateTime.Now;
        public Action UserActions { get; set; }
        public enum Action
        {
            Add = 1,
            Update = 2,
            Delete = 3
        };
        public string? DataChangeId { get; set; } = string.Empty;
        public ApplicationUser? DataChangeUser { get; set; }
    }
}

