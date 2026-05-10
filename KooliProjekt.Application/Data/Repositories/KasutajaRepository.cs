using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data.Repositories
{
    public class KasutajaRepository : BaseRepository<Kasutaja>, IKasutajaRepository
    {
        public KasutajaRepository(ApplicationDbContext dbContext) :
            base(dbContext)
        {
        }

        public override async Task<Kasutaja> GetByIdAsync(int id)
        {
            return await DbContext
                .Kasutajad
                .Include(k => k.Patsiendid)
                .Where(k => k.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
