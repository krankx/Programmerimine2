using System;
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
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteKaaluMootmineCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult();

            if (request.Id <= 0)
            {
                return result;
            }

            var mootmine = await _dbContext.KaaluMootmised
                .Where(m => m.Id == request.Id)
                .FirstOrDefaultAsync();

            if (mootmine == null)
            {
                return result;
            }

            _dbContext.KaaluMootmised.Remove(mootmine);
            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
