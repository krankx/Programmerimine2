using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Toiduained
{
    public class DeleteToiduaineCommandHandler : IRequestHandler<DeleteToiduaineCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteToiduaineCommandHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteToiduaineCommand request, CancellationToken cancellationToken)
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

            var toiduaine = await _dbContext.Toiduained
                .Where(t => t.Id == request.Id)
                .FirstOrDefaultAsync();

            if (toiduaine == null)
            {
                return result;
            }

            _dbContext.Toiduained.Remove(toiduaine);
            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
