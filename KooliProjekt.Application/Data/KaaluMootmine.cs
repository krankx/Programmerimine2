using System;

namespace KooliProjekt.Application.Data
{
    public class KaaluMootmine
    {
        public int Id { get; set; }
        public DateTime Kuupaev { get; set; }
        public decimal Kaal { get; set; } // kg

        public int PatsientId { get; set; }
        public Patsient Patsient { get; set; }
    }
}
