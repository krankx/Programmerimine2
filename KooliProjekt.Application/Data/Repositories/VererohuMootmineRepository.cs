namespace KooliProjekt.Application.Data.Repositories
{
    public class VererohuMootmineRepository : BaseRepository<VererohuMootmine>, IVererohuMootmineRepository
    {
        public VererohuMootmineRepository(ApplicationDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}
