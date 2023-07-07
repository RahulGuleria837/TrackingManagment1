using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using TrackingManagment.Models;

namespace TrackingManagment.Repository
{
    public interface ITrackingRepository
    {
        ICollection<TracingUser> GetAll();
        Task Add(TracingUser trackingUser);
        Task<bool> Delete(int id);

    }
}
