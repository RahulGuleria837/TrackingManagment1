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
        public async Task Add(RealState state)
        {
          
          await _context.realStates.AddAsync(state);
           await _context.SaveChangesAsync();
        }

        //To delete entity with the help of id
        public async Task<bool> Delete(int id)
        {
            var removeUser = await  _context.realStates.FindAsync(id);

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

        //To update any entity of realState
        public async Task Update(RealState state)
        {
            var updateState = _context.realStates.Update(state);
         await   _context.SaveChangesAsync();
           
        }
    }
}
