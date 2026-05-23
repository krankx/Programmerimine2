using System;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Data
{
    [ExcludeFromCodeCoverage]
    public class VeresuhkruMootmine : Entity
    {
        public DateTime Kuupaev { get; set; }
        public TimeSpan Kellaaeg { get; set; }
        public decimal Veresuhkur { get; set; } // mmol/L

        public int PatsientId { get; set; }
        public Patsient Patsient { get; set; }
    }
}
