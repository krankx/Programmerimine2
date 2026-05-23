using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class KaaluMootmineRepository : BaseRepository<KaaluMootmine>, IKaaluMootmineRepository
    {
        public KaaluMootmineRepository(ApplicationDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}
