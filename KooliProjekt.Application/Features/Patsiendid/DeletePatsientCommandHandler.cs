using System;
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
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeletePatsientCommand request, CancellationToken cancellationToken)
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

            var patsient = await _dbContext.Patsiendid
                .Where(p => p.Id == request.Id)
                .FirstOrDefaultAsync();

            if (patsient == null)
            {
                return result;
            }

            // Kustutame seotud andmed (mitu relatsiooni)
            var kaaluMootmised = await _dbContext.KaaluMootmised
                .Where(m => m.PatsientId == request.Id)
                .ToListAsync();
            _dbContext.KaaluMootmised.RemoveRange(kaaluMootmised);

            var vererohuMootmised = await _dbContext.VererohuMootmised
                .Where(m => m.PatsientId == request.Id)
                .ToListAsync();
            _dbContext.VererohuMootmised.RemoveRange(vererohuMootmised);

            var veresuhkruMootmised = await _dbContext.VeresuhkruMootmised
                .Where(m => m.PatsientId == request.Id)
                .ToListAsync();
            _dbContext.VeresuhkruMootmised.RemoveRange(veresuhkruMootmised);

            var soogikorraIds = await _dbContext.Soogikorrad
                .Where(s => s.PatsientId == request.Id)
                .Select(s => s.Id)
                .ToListAsync();

            var soogikorraRead = await _dbContext.SoogikorraRead
                .Where(r => soogikorraIds.Contains(r.SoogikordId))
                .ToListAsync();
            _dbContext.SoogikorraRead.RemoveRange(soogikorraRead);

            var soogikorrad = await _dbContext.Soogikorrad
                .Where(s => s.PatsientId == request.Id)
                .ToListAsync();
            _dbContext.Soogikorrad.RemoveRange(soogikorrad);

            _dbContext.Patsiendid.Remove(patsient);
            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
