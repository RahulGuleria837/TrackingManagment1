using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrackingManagment.Identity;
using TrackingManagment.Models;

namespace TrackingManagment.Repository
{
    public class TrackingRepository : ITrackingRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TrackingRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public bool Add(TracingUser trackingUser)
        {
             _context.tracingUsers.Add(trackingUser);
             _context.SaveChanges();
            return true;

        }



        public ICollection<TracingUser> GetAll(string DataChangeUserId)
        {
            var getAll = _context.tracingUsers.Where(u => u.DataChangeId == DataChangeUserId).ToList();
            return getAll;
        }

        public ICollection<TracingUser> GetByrealID(int id)
        {
            throw new NotImplementedException();
        }
    }
}
