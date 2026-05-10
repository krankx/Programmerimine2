namespace KooliProjekt.Application.Data.Repositories
{
    public class SoogikorraRidaRepository : BaseRepository<SoogikorraRida>, ISoogikorraRidaRepository
    {
        public SoogikorraRidaRepository(ApplicationDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}
