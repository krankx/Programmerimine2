using System;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Data
{
    [ExcludeFromCodeCoverage]
    public class KaaluMootmine : Entity
    {
        public DateTime Kuupaev { get; set; }
        public decimal Kaal { get; set; } // kg

        public int PatsientId { get; set; }
        public Patsient Patsient { get; set; }
    }
}
