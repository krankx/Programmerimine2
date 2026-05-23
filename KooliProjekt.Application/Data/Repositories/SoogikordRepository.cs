using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class SoogikordRepository : BaseRepository<Soogikord>, ISoogikordRepository
    {
        public SoogikordRepository(ApplicationDbContext dbContext) :
            base(dbContext)
        {
        }

        public override async Task<Soogikord> GetByIdAsync(int id)
        {
            return await DbContext
                .Soogikorrad
                .Include(s => s.Read)
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
