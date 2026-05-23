using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.SoogikorraRead
{
    [ExcludeFromCodeCoverage]
    public class SaveSoogikorraRidaCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public decimal Kogus { get; set; }
        public int SoogikordId { get; set; }
        public int ToiduaineId { get; set; }
    }
}
