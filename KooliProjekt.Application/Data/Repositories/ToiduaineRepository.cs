using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class ToiduaineRepository : BaseRepository<Toiduaine>, IToiduaineRepository
    {
        public ToiduaineRepository(ApplicationDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}
