using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.SoogikorraRead
{
    public class DeleteSoogikorraRidaCommandHandler : IRequestHandler<DeleteSoogikorraRidaCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteSoogikorraRidaCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteSoogikorraRidaCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            await _dbContext
                .SoogikorraRead
                .Where(r => r.Id == request.Id)
                .ExecuteDeleteAsync();

            return result;
        }
    }
}
