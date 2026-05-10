using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.VererohuMootmised
{
    public class DeleteVererohuMootmineCommandHandler : IRequestHandler<DeleteVererohuMootmineCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteVererohuMootmineCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteVererohuMootmineCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            await _dbContext
                .VererohuMootmised
                .Where(m => m.Id == request.Id)
                .ExecuteDeleteAsync();

            return result;
        }
    }
}
