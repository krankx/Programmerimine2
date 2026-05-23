using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Application.Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class SoogikorraRidaRepository : BaseRepository<SoogikorraRida>, ISoogikorraRidaRepository
    {
        public SoogikorraRidaRepository(ApplicationDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}
