using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Kasutajad
{
    [ExcludeFromCodeCoverage]
    public class SaveKasutajaCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public string Eesnimi { get; set; }
        public string Perekonnanimi { get; set; }
        public string Email { get; set; }
        public string Parool { get; set; }
    }
}
