using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrackingManagment.Models;

namespace TrackingManagment.Identity
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }
       
        public DbSet<RealState> realStates { get; set; }
        public DbSet<TracingUser> tracingUsers { get; set; }
        public DbSet<InvitedUser> invitedUsers { get; set; }
    }
}
