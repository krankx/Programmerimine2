using System;
using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VererohuMootmised
{
    [ExcludeFromCodeCoverage]
    public class SaveVererohuMootmineCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public DateTime Kuupaev { get; set; }
        public TimeSpan Kellaaeg { get; set; }
        public int Sustoolne { get; set; }
        public int Diastoolne { get; set; }
        public int Pulss { get; set; }
        public int PatsientId { get; set; }
    }
}
