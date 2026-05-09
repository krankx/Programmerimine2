using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VeresuhkruMootmised
{
    public class SaveVeresuhkruMootmineCommandHandler : IRequestHandler<SaveVeresuhkruMootmineCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveVeresuhkruMootmineCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveVeresuhkruMootmineCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var mootmine = new VeresuhkruMootmine();
            if (request.Id == 0)
            {
                await _dbContext.VeresuhkruMootmised.AddAsync(mootmine);
            }
            else
            {
                mootmine = await _dbContext.VeresuhkruMootmised.FindAsync(request.Id);
            }

            mootmine.Kuupaev = request.Kuupaev;
            mootmine.Kellaaeg = request.Kellaaeg;
            mootmine.Veresuhkur = request.Veresuhkur;
            mootmine.PatsientId = request.PatsientId;

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
