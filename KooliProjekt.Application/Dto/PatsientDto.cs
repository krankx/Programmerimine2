using System;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Dto
{
    [ExcludeFromCodeCoverage]
    public class PatsientDto
    {
        public int Id { get; set; }
        public string Eesnimi { get; set; }
        public string Perekonnanimi { get; set; }
        public string Isikukood { get; set; }
        public DateTime Synniaeg { get; set; }
        public int KasutajaId { get; set; }
    }
}
