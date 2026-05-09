using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Toiduained
{
    public class SaveToiduaineCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public string Nimetus { get; set; }
        public decimal Energia { get; set; }
        public decimal Valgud { get; set; }
        public decimal Susivesikud { get; set; }
        public decimal MillestSuhkrud { get; set; }
        public decimal Rasvad { get; set; }
        public decimal MillestKullastunud { get; set; }
        public decimal Kiudained { get; set; }
        public decimal Sool { get; set; }
    }
}
