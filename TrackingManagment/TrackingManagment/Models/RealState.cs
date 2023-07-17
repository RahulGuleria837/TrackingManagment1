using TrackingManagment.Identity;

namespace TrackingManagment.Models
{
    public class RealState
    {
        public int Id { get; set; }
        public string PropertyName { get; set; }=string.Empty;
        public int Price { get; set; } = 0;
        public string City { get; set; }=string.Empty;
        public string Area { get; set; } = string.Empty;
       
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public bool? IsDeleted { get; set; } = false;


    }
}
