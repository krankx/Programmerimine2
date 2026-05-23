using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Dto
{
    [ExcludeFromCodeCoverage]
    public class SoogikorraRidaDto
    {
        public int Id { get; set; }
        public decimal Kogus { get; set; }
        public int SoogikordId { get; set; }
        public int ToiduaineId { get; set; }
    }
}
