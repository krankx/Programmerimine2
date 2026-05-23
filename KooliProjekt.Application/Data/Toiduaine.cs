using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Data
{
    [ExcludeFromCodeCoverage]
    public class Toiduaine : Entity
    {
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        public string Nimetus { get; set; }

        public decimal Energia { get; set; }            // kcal / 100g
        public decimal Valgud { get; set; }             // g / 100g
        public decimal Susivesikud { get; set; }        // g / 100g
        public decimal MillestSuhkrud { get; set; }     // g / 100g
        public decimal Rasvad { get; set; }             // g / 100g
        public decimal MillestKullastunud { get; set; } // g / 100g
        public decimal Kiudained { get; set; }          // g / 100g
        public decimal Sool { get; set; }               // g / 100g

        public List<SoogikorraRida> SoogikorraRead { get; set; }
    }
}
