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

        public TrackingRepository(ApplicationDbContext context, UserManager<ApplicationUser>userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task Add(TracingUser trackingUser)
        {
          await  _context.tracingUsers.AddAsync(trackingUser);
        await   _context.SaveChangesAsync();
          
        }

        public async Task<bool> Delete(int id)
        {
            var removeUser = await _context.tracingUsers.FindAsync(id);

            _context.tracingUsers.Remove(removeUser);
            _context.SaveChanges();
            return true;
        }

        public ICollection<TracingUser> GetAll()
        {
          var getAll =   _context.tracingUsers.ToList();
            return getAll;
        }

       
    }
}
