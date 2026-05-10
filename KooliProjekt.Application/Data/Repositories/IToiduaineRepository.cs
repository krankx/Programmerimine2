using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IToiduaineRepository
    {
        Task<Toiduaine> GetByIdAsync(int id);
        Task SaveAsync(Toiduaine toiduaine);
        Task DeleteAsync(Toiduaine entity);
    }
}
