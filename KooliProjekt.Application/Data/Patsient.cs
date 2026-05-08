using System;
using System.Collections.Generic;

namespace KooliProjekt.Application.Data
{
    public class Patsient
    {
        public int Id { get; set; }
        public string Eesnimi { get; set; }
        public string Perekonnanimi { get; set; }
        public string Isikukood { get; set; }
        public DateTime Synniaeg { get; set; }

        public int KasutajaId { get; set; }
        public Kasutaja Kasutaja { get; set; }

        public List<KaaluMootmine> KaaluMootmised { get; set; }
        public List<VeresuhkruMootmine> VeresuhkruMootmised { get; set; }
        public List<VererohuMootmine> VererohuMootmised { get; set; }
        public List<Soogikord> Soogikorrad { get; set; }
    }
}
