using System.Collections.Generic;

namespace KooliProjekt.Application.Data
{
    public class Kasutaja
    {
        public int Id { get; set; }
        public string Eesnimi { get; set; }
        public string Perekonnanimi { get; set; }
        public string Email { get; set; }
        public string Parool { get; set; }

        public List<Patsient> Patsiendid { get; set; }
    }
}
