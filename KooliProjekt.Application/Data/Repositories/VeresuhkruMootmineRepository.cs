using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class VeresuhkruMootmineRepository : BaseRepository<VeresuhkruMootmine>, IVeresuhkruMootmineRepository
    {
        public VeresuhkruMootmineRepository(ApplicationDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}
