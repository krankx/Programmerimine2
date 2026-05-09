using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.SoogikorraRead
{
    public class SaveSoogikorraRidaCommandHandler : IRequestHandler<SaveSoogikorraRidaCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveSoogikorraRidaCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveSoogikorraRidaCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var rida = new SoogikorraRida();
            if (request.Id == 0)
            {
                await _dbContext.SoogikorraRead.AddAsync(rida);
            }
            else
            {
                rida = await _dbContext.SoogikorraRead.FindAsync(request.Id);
            }

            rida.Kogus = request.Kogus;
            rida.SoogikordId = request.SoogikordId;
            rida.ToiduaineId = request.ToiduaineId;

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
