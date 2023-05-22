using TrackingManagment.Models;

namespace TrackingManagment.Repository
{
    public interface IEmailService
    {
        void SendEmail(Email email);
    }
}
