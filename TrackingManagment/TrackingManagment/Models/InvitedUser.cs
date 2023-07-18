using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrackingManagment.Identity;

namespace TrackingManagment.Models
{
    public class InvitedUser
    {
        [Key]
        public int Id { get; set; }
        //public string Email { get; set; }
        public string InvitationSenderName { get; set; }

        public string InvitationReciverName { get; set; }

        public string InvitationSenderUserId { get; set; } = string.Empty;
        [ForeignKey("InvitationSenderUserId")]
        public ApplicationUser? ApplicationUserSender { get; set; }
        public string InvitationReceiverUserId { get; set; } = string.Empty;
        [ForeignKey("InvitationReceiverUserId")]
        public ApplicationUser? ApplicationUserReceiver { get; set; }
        public Action Action { get; set; }
        public Status Status { get; set; }
        [NotMapped]
        public string? UserNameRec { get; internal set; }
        [NotMapped]
        public string? UserNamesen { get; internal set; }
    }
}
