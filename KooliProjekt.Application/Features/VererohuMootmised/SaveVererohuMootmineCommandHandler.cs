using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VererohuMootmised
{
    public class SaveVererohuMootmineCommandHandler : IRequestHandler<SaveVererohuMootmineCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveVererohuMootmineCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveVererohuMootmineCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var mootmine = new VererohuMootmine();
            if (request.Id == 0)
            {
                await _dbContext.VererohuMootmised.AddAsync(mootmine);
            }
            else
            {
                mootmine = await _dbContext.VererohuMootmised.FindAsync(request.Id);
            }

            mootmine.Kuupaev = request.Kuupaev;
            mootmine.Kellaaeg = request.Kellaaeg;
            mootmine.Sustoolne = request.Sustoolne;
            mootmine.Diastoolne = request.Diastoolne;
            mootmine.Pulss = request.Pulss;
            mootmine.PatsientId = request.PatsientId;

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
