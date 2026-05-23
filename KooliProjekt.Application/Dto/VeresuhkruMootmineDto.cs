using System;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Dto
{
    [ExcludeFromCodeCoverage]
    public class VeresuhkruMootmineDto
    {
        public int Id { get; set; }
        public DateTime Kuupaev { get; set; }
        public TimeSpan Kellaaeg { get; set; }
        public decimal Veresuhkur { get; set; }
        public int PatsientId { get; set; }
    }
}
