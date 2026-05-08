using System;

namespace KooliProjekt.Application.Data
{
    public class VeresuhkruMootmine
    {
        public int Id { get; set; }
        public DateTime Kuupaev { get; set; }
        public TimeSpan Kellaaeg { get; set; }
        public decimal Veresuhkur { get; set; } // mmol/L

        public int PatsientId { get; set; }
        public Patsient Patsient { get; set; }
    }
}
