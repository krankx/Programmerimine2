namespace KooliProjekt.Application.Data.Repositories
{
    public class KaaluMootmineRepository : BaseRepository<KaaluMootmine>, IKaaluMootmineRepository
    {
        public KaaluMootmineRepository(ApplicationDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}
