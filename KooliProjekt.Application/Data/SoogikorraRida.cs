using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Data
{
    [ExcludeFromCodeCoverage]
    public class SoogikorraRida : Entity
    {
        public decimal Kogus { get; set; } // grammides

        public int SoogikordId { get; set; }
        public Soogikord Soogikord { get; set; }

        public int ToiduaineId { get; set; }
        public Toiduaine Toiduaine { get; set; }
    }
}
