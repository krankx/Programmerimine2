using System.Collections.Generic;

namespace KooliProjekt.Application.Dto
{
    public class KasutajaDetailsDto
    {
        public int Id { get; set; }
        public string Eesnimi { get; set; }
        public string Perekonnanimi { get; set; }
        public string Email { get; set; }
        public List<PatsientDto> Patsiendid { get; set; } = new List<PatsientDto>();
    }
}
