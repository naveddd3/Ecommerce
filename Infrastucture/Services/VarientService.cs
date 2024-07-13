using Application.Interfaces;
using Domain.Entities;
using Domain.Helper;
using System.Data;

namespace Infrastucture.Services
{
    public class VarientService : IVarientService
    {
        private readonly IDapperRepository repository;

        public VarientService(IDapperRepository repository)
        {
            this.repository=repository;
        }

        public async Task<Response> SaveOrUpdateVarient(Varient varient)
        {
            var res = new Response() { ResponseText="An Error Occured During Save Varient !", StatusCode = ResponseStatus.Failed };
            try
            {
                res = await repository.GetAsync<Response>("Proc_SaveOrUpdateVarient", new
                {
                    varient.VarientId,
                    varient.VarientName,
                    varient.VarientType
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

        public async Task<Varient> AddOrEditVarient (int Id)
        {
            try
            {
                var res = await repository.GetAsync<Varient>("SELECT * FROM Master_Varient_Type WHERE Id = @Id", new
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

        public async Task<IEnumerable<Varient>> GetAll()
        {
            try
            {
                var list = await repository.GetAllAsync<Varient>("SELECT * FROM Master_Varient_Type", null, CommandType.Text);
                return list;    
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
