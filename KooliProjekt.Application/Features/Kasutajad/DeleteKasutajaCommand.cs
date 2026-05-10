using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Kasutajad
{
    public class DeleteKasutajaCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}
