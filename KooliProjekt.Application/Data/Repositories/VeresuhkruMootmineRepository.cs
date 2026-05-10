namespace KooliProjekt.Application.Data.Repositories
{
    public class VeresuhkruMootmineRepository : BaseRepository<VeresuhkruMootmine>, IVeresuhkruMootmineRepository
    {
        public VeresuhkruMootmineRepository(ApplicationDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}
