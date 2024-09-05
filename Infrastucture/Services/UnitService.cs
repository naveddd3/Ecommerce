using Application.Interfaces;
using Domain.Entities;
using Domain.Helper;
using System.Data;

namespace Infrastucture.Services
{
    public class UnitService : IUnitService
    {
        private readonly IDapperRepository repository;

        public UnitService(IDapperRepository repository)
        {
            this.repository=repository;
        }

        public async Task<Response> SaveOrUpdateUnit(MasterUnit Unit)
        {
            var res = new Response() { ResponseText="An Error Occured During Save Unit !", StatusCode = ResponseStatus.Failed };
            try
            {
                res = await repository.GetAsync<Response>("Proc_SaveOrUpdateUnit", new
                {
                    Unit.UnitId,
                    Unit.UnitName,
                    Unit.UnitType
                });
                return res;
            }
            catch (Exception ex)
            {
                res.ResponseText = ex.Message;
                res.StatusCode = ResponseStatus.Failed;
                return res;
            }
        }

        public async Task<MasterUnit> AddOrEditUnit (int Id)
        {
            try
            {
                var res = await repository.GetAsync<MasterUnit>("SELECT Id as UnitId,* FROM Master_Unit WHERE Id = @Id", new
                {
                    Id = Id,
                },CommandType.Text);
                return res;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<MasterUnit>> GetAll()
        {
            try
            {
                var list = await repository.GetAllAsync<MasterUnit>("SELECT Id as UnitId,* FROM Master_Unit", null, CommandType.Text);
                return list;    
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
