using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackingManagment.Identity;
using TrackingManagment.Models;

namespace TrackingManagment.Repository
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        //To Add new Data to RealState
        public bool Add(RealState state)
        {

            _context.realStates.Add(state);
            _context.SaveChanges();
            return true;
        }

        //To delete entity with the help of id
        public bool Delete(int id)
        {
            var removeUser = _context.realStates.Find(id);

            _context.realStates.Remove(removeUser);
            _context.SaveChanges();
            return true;
        }

        //To get the realState entity with the help of id
        public async Task<RealState> Get(int id)
        {
            var state = await _context.realStates.Where(e => e.Id == id).FirstOrDefaultAsync();
            return state;
        }


        public async Task<List<RealState>> GetAll()
        {
            var getAll = await _context.realStates.ToListAsync();
            return getAll;

        }

        public RealState GetbyId(int id)
        {
            var emp = _context.realStates.FirstOrDefault(v => v.Id == id);
            return emp;
        }

        public ICollection<RealState> GetSpecificUserData(string UserID)
        {
            var data = _context.realStates.Where(u => u.ApplicationUserId == UserID).ToList();
            return data;
        }

        //To update any entity of realState
        public async Task<bool> Update(RealState state)
        {
            var updateState = _context.realStates.Update(state);
            await _context.SaveChangesAsync();
            return true;

        }

    }
}
