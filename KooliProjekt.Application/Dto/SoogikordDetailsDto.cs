using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Dto
{
    [ExcludeFromCodeCoverage]
    public class SoogikordDetailsDto
    {
        public int Id { get; set; }
        public DateTime Kuupaev { get; set; }
        public SoogikorraTyyp Tyyp { get; set; }
        public int PatsientId { get; set; }
        public List<SoogikorraRidaDto> Read { get; set; } = new List<SoogikorraRidaDto>();
    }
}
