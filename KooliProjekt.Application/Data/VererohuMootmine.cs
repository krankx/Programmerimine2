using System;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Data
{
    [ExcludeFromCodeCoverage]
    public class VererohuMootmine : Entity
    {
        public DateTime Kuupaev { get; set; }
        public TimeSpan Kellaaeg { get; set; }
        public int Sustoolne { get; set; }   // ülemine
        public int Diastoolne { get; set; }  // alumine
        public int Pulss { get; set; }

        public int PatsientId { get; set; }
        public Patsient Patsient { get; set; }
    }
}
