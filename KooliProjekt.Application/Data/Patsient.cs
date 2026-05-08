using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data
{
    public class Patsient
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string Eesnimi { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string Perekonnanimi { get; set; }

        [Required]
        [MaxLength(11)]
        [MinLength(11)]
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
