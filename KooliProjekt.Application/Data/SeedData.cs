using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace KooliProjekt.Application.Data
{
    [ExcludeFromCodeCoverage]
    public class SeedData
    {
        private readonly ApplicationDbContext _dbContext;

        public SeedData(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Generate()
        {
            if (_dbContext.Kasutajad.Any())
            {
                return;
            }

            // Kasutajad (10 tk)
            for (var i = 0; i < 10; i++)
            {
                var kasutaja = new Kasutaja
                {
                    Eesnimi = "Eesnimi" + (i + 1),
                    Perekonnanimi = "Perekonnanimi" + (i + 1),
                    Email = "kasutaja" + (i + 1) + "@tervis.ee",
                    Parool = "parool" + (i + 1)
                };
                _dbContext.Kasutajad.Add(kasutaja);

                // Patsiendid (igale kasutajale 1 patsient = 10 patsienti kokku)
                var patsient = new Patsient
                {
                    Eesnimi = "Patsient" + (i + 1),
                    Perekonnanimi = "Perekonnanimi" + (i + 1),
                    Isikukood = "3850101000" + (i % 10),
                    Synniaeg = new DateTime(1985, 1, 1).AddDays(i * 100),
                    Kasutaja = kasutaja
                };
                _dbContext.Patsiendid.Add(patsient);

                // Iga patsiendi kohta kaalu mõõtmine
                var kaal = new KaaluMootmine
                {
                    Kuupaev = new DateTime(2025, 10, 1).AddDays(i),
                    Kaal = 70 + i,
                    Patsient = patsient
                };
                _dbContext.KaaluMootmised.Add(kaal);

                // Veresuhkru mõõtmine
                var suhkur = new VeresuhkruMootmine
                {
                    Kuupaev = new DateTime(2025, 10, 1).AddDays(i),
                    Kellaaeg = new TimeSpan(8, 0, 0),
                    Veresuhkur = 5 + (decimal)(i * 0.1),
                    Patsient = patsient
                };
                _dbContext.VeresuhkruMootmised.Add(suhkur);

                // Vererõhu mõõtmine
                var rohk = new VererohuMootmine
                {
                    Kuupaev = new DateTime(2025, 10, 1).AddDays(i),
                    Kellaaeg = new TimeSpan(9, 0, 0),
                    Sustoolne = 120 + i,
                    Diastoolne = 80 + i,
                    Pulss = 70 + i,
                    Patsient = patsient
                };
                _dbContext.VererohuMootmised.Add(rohk);
            }

            // Toiduained (10 tk)
            string[] nimetused = { "Leib", "Sai", "Kana", "Kala", "Õun", "Banaan", "Piim", "Juust", "Riis", "Pasta" };
            for (var i = 0; i < 10; i++)
            {
                var toit = new Toiduaine
                {
                    Nimetus = nimetused[i],
                    Energia = 100 + i * 10,
                    Valgud = 5 + i,
                    Susivesikud = 20 + i,
                    MillestSuhkrud = 2 + i,
                    Rasvad = 1 + i,
                    MillestKullastunud = (decimal)(0.5 + i * 0.1),
                    Kiudained = 1 + i,
                    Sool = (decimal)(0.1 + i * 0.05)
                };
                _dbContext.Toiduained.Add(toit);
            }

            _dbContext.SaveChanges();

            // Söögikorrad (10 tk) - vaja olemasolevaid patsiente ja toiduaineid
            var patsiendid = _dbContext.Patsiendid.ToList();
            var toiduained = _dbContext.Toiduained.ToList();

            for (var i = 0; i < 10; i++)
            {
                var soogikord = new Soogikord
                {
                    Kuupaev = new DateTime(2025, 10, 1).AddDays(i),
                    Tyyp = (SoogikorraTyyp)((i % 3) + 1),
                    PatsientId = patsiendid[i].Id
                };
                _dbContext.Soogikorrad.Add(soogikord);

                // Iga söögikorra kohta üks rida
                var rida = new SoogikorraRida
                {
                    Kogus = 100 + i * 10,
                    Soogikord = soogikord,
                    ToiduaineId = toiduained[i].Id
                };
                _dbContext.SoogikorraRead.Add(rida);
            }

            _dbContext.SaveChanges();
        }
    }
}
