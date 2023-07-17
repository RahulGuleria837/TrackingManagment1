using Microsoft.EntityFrameworkCore;
using TrackingManagment.Models;

namespace TrackingManagment.Repository
{
    public interface IRepository
    {
        Task<List<RealState>>GetAll();
        Task<RealState> Get(int id);
        public RealState GetbyId(int id);
        Task<bool> Update(RealState state);
        
        public bool Add(RealState state);
        public bool Delete(int id);
       ICollection<RealState> GetSpecificUserData(String UserID);


    }
}
