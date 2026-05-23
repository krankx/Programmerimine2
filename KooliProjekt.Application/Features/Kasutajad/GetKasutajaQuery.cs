using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Kasutajad
{
    [ExcludeFromCodeCoverage]
    public class GetKasutajaQuery : IRequest<OperationResult<KasutajaDetailsDto>>
    {
        public int Id { get; set; }
    }
}
