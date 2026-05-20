using System;
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
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteSoogikorraRidaCommand request, CancellationToken cancellationToken)
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

            var rida = await _dbContext.SoogikorraRead
                .Where(r => r.Id == request.Id)
                .FirstOrDefaultAsync();

            if (rida == null)
            {
                return result;
            }

            _dbContext.SoogikorraRead.Remove(rida);
            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
