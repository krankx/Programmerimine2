using System;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Dto
{
    [ExcludeFromCodeCoverage]
    public class KaaluMootmineDto
    {
        public int Id { get; set; }
        public DateTime Kuupaev { get; set; }
        public decimal Kaal { get; set; }
        public int PatsientId { get; set; }
    }
}
