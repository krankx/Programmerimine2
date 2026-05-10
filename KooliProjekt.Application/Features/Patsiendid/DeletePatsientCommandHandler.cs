using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Patsiendid
{
    public class DeletePatsientCommandHandler : IRequestHandler<DeletePatsientCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeletePatsientCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeletePatsientCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            await _dbContext
                .Patsiendid
                .Where(p => p.Id == request.Id)
                .ExecuteDeleteAsync();

            return result;
        }
    }
}
