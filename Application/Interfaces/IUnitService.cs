using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUnitService
    {
        Task<Response> SaveOrUpdateUnit(MasterUnit unit);
        Task<MasterUnit> AddOrEditUnit(int Id);
        Task<IEnumerable<MasterUnit>> GetAll();
    }
}
