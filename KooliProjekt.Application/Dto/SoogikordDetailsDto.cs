using System;
using System.Collections.Generic;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Dto
{
    public class SoogikordDetailsDto
    {
        public int Id { get; set; }
        public DateTime Kuupaev { get; set; }
        public SoogikorraTyyp Tyyp { get; set; }
        public int PatsientId { get; set; }
        public List<SoogikorraRidaDto> Read { get; set; } = new List<SoogikorraRidaDto>();
    }
}
