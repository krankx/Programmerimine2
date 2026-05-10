using System;
using System.Collections.Generic;

namespace KooliProjekt.Application.Data
{
    public enum SoogikorraTyyp
    {
        Hommikusook = 1,
        Louna = 2,
        Ohtusook = 3
    }

    public class Soogikord : Entity
    {
        public DateTime Kuupaev { get; set; }
        public SoogikorraTyyp Tyyp { get; set; }

        public int PatsientId { get; set; }
        public Patsient Patsient { get; set; }

        public List<SoogikorraRida> Read { get; set; }
    }
}
