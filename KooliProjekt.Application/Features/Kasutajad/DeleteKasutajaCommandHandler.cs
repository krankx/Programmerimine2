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
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteKasutajaCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            await _dbContext
                .Kasutajad
                .Where(k => k.Id == request.Id)
                .ExecuteDeleteAsync();

            return result;
        }
    }
}
