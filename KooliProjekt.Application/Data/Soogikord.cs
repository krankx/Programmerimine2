using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Data
{
    public enum SoogikorraTyyp
    {
        Hommikusook = 1,
        Louna = 2,
        Ohtusook = 3
    }

    [ExcludeFromCodeCoverage]
    public class Soogikord : Entity
    {
        public DateTime Kuupaev { get; set; }
        public SoogikorraTyyp Tyyp { get; set; }

        public int PatsientId { get; set; }
        public Patsient Patsient { get; set; }

        public List<SoogikorraRida> Read { get; set; }
    }
}
