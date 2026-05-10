using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.VeresuhkruMootmised
{
    public class DeleteVeresuhkruMootmineCommandHandler : IRequestHandler<DeleteVeresuhkruMootmineCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteVeresuhkruMootmineCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteVeresuhkruMootmineCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            await _dbContext
                .VeresuhkruMootmised
                .Where(m => m.Id == request.Id)
                .ExecuteDeleteAsync();

            return result;
        }
    }
}
