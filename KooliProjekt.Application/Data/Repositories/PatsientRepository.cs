using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class PatsientRepository : BaseRepository<Patsient>, IPatsientRepository
    {
        public PatsientRepository(ApplicationDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}
