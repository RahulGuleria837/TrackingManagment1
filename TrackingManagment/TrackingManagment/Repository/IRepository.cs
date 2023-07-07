using TrackingManagment.Models;

namespace TrackingManagment.Repository
{
    public interface IRepository
    {
        Task<List<RealState>>GetAll();
        Task<RealState> Get(int id);
        Task Update(RealState state);
        Task Add(RealState state);
        Task<bool> Delete(int id);
       ICollection<RealState> GetSpecificUserData(String UserID);
    }
}
