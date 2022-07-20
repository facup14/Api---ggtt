using DATA.Errors;
using DATA.Models;
using MediatR;
using PERSISTENCE.Repository;
using Service.EventHandlers.Command;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Service.EventHandlers
{
    public class UpdateTaller : IRequestHandler<UpdateTallerCommand, GetResponse>
    {
        private readonly IRepositoryAsync<Talleres> _repositoryAsync;

        public UpdateTaller(IRepositoryAsync<Talleres> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }
        
        public async Task<GetResponse> Handle(UpdateTallerCommand request, CancellationToken cancellationToken)
        {
            var record = await _repositoryAsync.GetByIdAsync(request.IdTaller);
            if(record == null)
            {
                return new GetResponse()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "error",
                    Result = null
                };
            }
            record.NombreTaller = request.NombreTaller;

            await _repositoryAsync.UpdateAsync(record);
            
            return new GetResponse()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "success",
                Result = record
            };        
        }
    }
}
