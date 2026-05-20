using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Soogikorrad
{
    public class DeleteSoogikordCommandHandler : IRequestHandler<DeleteSoogikordCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteSoogikordCommandHandler(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteSoogikordCommand request, CancellationToken cancellationToken)
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

            var soogikord = await _dbContext.Soogikorrad
                .Where(s => s.Id == request.Id)
                .FirstOrDefaultAsync();

            if (soogikord == null)
            {
                return result;
            }

            // Kustutame seotud read enne
            var read = await _dbContext.SoogikorraRead
                .Where(r => r.SoogikordId == request.Id)
                .ToListAsync();
            _dbContext.SoogikorraRead.RemoveRange(read);

            _dbContext.Soogikorrad.Remove(soogikord);
            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
