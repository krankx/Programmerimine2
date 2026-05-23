using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class VererohuMootmineRepository : BaseRepository<VererohuMootmine>, IVererohuMootmineRepository
    {
        public VererohuMootmineRepository(ApplicationDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}
