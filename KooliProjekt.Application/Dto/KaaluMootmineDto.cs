using System;

namespace KooliProjekt.Application.Dto
{
    public class KaaluMootmineDto
    {
        public int Id { get; set; }
        public DateTime Kuupaev { get; set; }
        public decimal Kaal { get; set; }
        public int PatsientId { get; set; }
    }
}
