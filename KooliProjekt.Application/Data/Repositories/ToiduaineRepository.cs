namespace KooliProjekt.Application.Data.Repositories
{
    public class ToiduaineRepository : BaseRepository<Toiduaine>, IToiduaineRepository
    {
        public ToiduaineRepository(ApplicationDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}
