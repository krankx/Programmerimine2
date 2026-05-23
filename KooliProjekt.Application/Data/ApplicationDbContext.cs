using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    [ExcludeFromCodeCoverage]
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Olemasolevad (õpetaja näide)
        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<ToDoItem> ToDoItems { get; set; }

        // Minu klassid (Tervise monitoorimine)
        public DbSet<Kasutaja> Kasutajad { get; set; }
        public DbSet<Patsient> Patsiendid { get; set; }
        public DbSet<KaaluMootmine> KaaluMootmised { get; set; }
        public DbSet<VeresuhkruMootmine> VeresuhkruMootmised { get; set; }
        public DbSet<VererohuMootmine> VererohuMootmised { get; set; }
        public DbSet<Toiduaine> Toiduained { get; set; }
        public DbSet<Soogikord> Soogikorrad { get; set; }
        public DbSet<SoogikorraRida> SoogikorraRead { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // decimal täpsus mõõtenäitajate jaoks
            modelBuilder.Entity<KaaluMootmine>().Property(x => x.Kaal).HasPrecision(6, 2);
            modelBuilder.Entity<VeresuhkruMootmine>().Property(x => x.Veresuhkur).HasPrecision(6, 2);
            modelBuilder.Entity<SoogikorraRida>().Property(x => x.Kogus).HasPrecision(8, 2);

            modelBuilder.Entity<Toiduaine>().Property(x => x.Energia).HasPrecision(8, 2);
            modelBuilder.Entity<Toiduaine>().Property(x => x.Valgud).HasPrecision(6, 2);
            modelBuilder.Entity<Toiduaine>().Property(x => x.Susivesikud).HasPrecision(6, 2);
            modelBuilder.Entity<Toiduaine>().Property(x => x.MillestSuhkrud).HasPrecision(6, 2);
            modelBuilder.Entity<Toiduaine>().Property(x => x.Rasvad).HasPrecision(6, 2);
            modelBuilder.Entity<Toiduaine>().Property(x => x.MillestKullastunud).HasPrecision(6, 2);
            modelBuilder.Entity<Toiduaine>().Property(x => x.Kiudained).HasPrecision(6, 2);
            modelBuilder.Entity<Toiduaine>().Property(x => x.Sool).HasPrecision(6, 2);
        }
    }
}
