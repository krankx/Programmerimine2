using System;
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
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteVeresuhkruMootmineCommand request, CancellationToken cancellationToken)
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

            var mootmine = await _dbContext.VeresuhkruMootmised
                .Where(m => m.Id == request.Id)
                .FirstOrDefaultAsync();

            if (mootmine == null)
            {
                return result;
            }

            _dbContext.VeresuhkruMootmised.Remove(mootmine);
            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
