using System;
using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.KaaluMootmised
{
    [ExcludeFromCodeCoverage]
    public class SaveKaaluMootmineCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public DateTime Kuupaev { get; set; }
        public decimal Kaal { get; set; }
        public int PatsientId { get; set; }
    }
}
