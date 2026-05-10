using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.KaaluMootmised
{
    public class DeleteKaaluMootmineCommandHandler : IRequestHandler<DeleteKaaluMootmineCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteKaaluMootmineCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteKaaluMootmineCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            await _dbContext
                .KaaluMootmised
                .Where(m => m.Id == request.Id)
                .ExecuteDeleteAsync();

            return result;
        }
    }
}
