namespace KooliProjekt.Application.Data.Repositories
{
    public class PatsientRepository : BaseRepository<Patsient>, IPatsientRepository
    {
        public PatsientRepository(ApplicationDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}
