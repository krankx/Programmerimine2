using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface ISoogikordRepository
    {
        Task<Soogikord> GetByIdAsync(int id);
        Task SaveAsync(Soogikord soogikord);
        Task DeleteAsync(Soogikord entity);
    }
}
