using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Kasutajad
{
    public class DeleteKasutajaCommandHandler : IRequestHandler<DeleteKasutajaCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteKasutajaCommandHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteKasutajaCommand request, CancellationToken cancellationToken)
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

            var kasutaja = await _dbContext.Kasutajad
                .Where(k => k.Id == request.Id)
                .FirstOrDefaultAsync();

            if (kasutaja == null)
            {
                return result;
            }

            _dbContext.Kasutajad.Remove(kasutaja);
            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
