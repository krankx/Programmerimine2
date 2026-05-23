using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Data
{
    [ExcludeFromCodeCoverage]
    public class Kasutaja : Entity
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
        [MaxLength(100)]
        [MinLength(1)]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(1)]
        public string Parool { get; set; }

        public List<Patsient> Patsiendid { get; set; }
    }
}
