using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Data
{
    [ExcludeFromCodeCoverage]
    public class Patsient : Entity
    {
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
