using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Soogikorrad
{
    public class SaveSoogikordCommandHandler : IRequestHandler<SaveSoogikordCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveSoogikordCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveSoogikordCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var soogikord = new Soogikord();
            if (request.Id == 0)
            {
                await _dbContext.Soogikorrad.AddAsync(soogikord);
            }
            else
            {
                soogikord = await _dbContext.Soogikorrad.FindAsync(request.Id);
            }

            soogikord.Kuupaev = request.Kuupaev;
            soogikord.Tyyp = request.Tyyp;
            soogikord.PatsientId = request.PatsientId;

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
