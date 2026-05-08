namespace KooliProjekt.Application.Data
{
    public class SoogikorraRida
    {
        public int Id { get; set; }
        public decimal Kogus { get; set; } // grammides

        public int SoogikordId { get; set; }
        public Soogikord Soogikord { get; set; }

        public int ToiduaineId { get; set; }
        public Toiduaine Toiduaine { get; set; }
    }
}
