using System;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Dto
{
    [ExcludeFromCodeCoverage]
    public class VererohuMootmineDto
    {
        public int Id { get; set; }
        public DateTime Kuupaev { get; set; }
        public TimeSpan Kellaaeg { get; set; }
        public int Sustoolne { get; set; }
        public int Diastoolne { get; set; }
        public int Pulss { get; set; }
        public int PatsientId { get; set; }
    }
}
