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
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteSoogikordCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            await _dbContext
                .Soogikorrad
                .Where(s => s.Id == request.Id)
                .ExecuteDeleteAsync();

            return result;
        }
    }
}
