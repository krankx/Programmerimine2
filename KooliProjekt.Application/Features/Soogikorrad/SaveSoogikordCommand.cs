using System;
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Soogikorrad
{
    public class SaveSoogikordCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public DateTime Kuupaev { get; set; }
        public SoogikorraTyyp Tyyp { get; set; }
        public int PatsientId { get; set; }
    }
}
