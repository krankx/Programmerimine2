using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.KaaluMootmised
{
    public class SaveKaaluMootmineCommandHandler : IRequestHandler<SaveKaaluMootmineCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveKaaluMootmineCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveKaaluMootmineCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var mootmine = new KaaluMootmine();
            if (request.Id == 0)
            {
                await _dbContext.KaaluMootmised.AddAsync(mootmine);
            }
            else
            {
                mootmine = await _dbContext.KaaluMootmised.FindAsync(request.Id);
            }

            mootmine.Kuupaev = request.Kuupaev;
            mootmine.Kaal = request.Kaal;
            mootmine.PatsientId = request.PatsientId;

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
