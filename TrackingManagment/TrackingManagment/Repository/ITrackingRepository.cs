using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using TrackingManagment.Models;

namespace TrackingManagment.Repository
{
    public interface ITrackingRepository
    {
        ICollection<TracingUser> GetAll(string DataChangeUserId);
        Task Add(TracingUser trackingUser);
    

    }
}
