using System;

namespace KooliProjekt.Application.Data
{
    public class VererohuMootmine
    {
        public int Id { get; set; }
        public DateTime Kuupaev { get; set; }
        public TimeSpan Kellaaeg { get; set; }
        public int Sustoolne { get; set; }   // ülemine
        public int Diastoolne { get; set; }  // alumine
        public int Pulss { get; set; }

        public int PatsientId { get; set; }
        public Patsient Patsient { get; set; }
    }
}
