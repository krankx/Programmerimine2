using System;
using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VeresuhkruMootmised
{
    [ExcludeFromCodeCoverage]
    public class SaveVeresuhkruMootmineCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public DateTime Kuupaev { get; set; }
        public TimeSpan Kellaaeg { get; set; }
        public decimal Veresuhkur { get; set; }
        public int PatsientId { get; set; }
    }
}
