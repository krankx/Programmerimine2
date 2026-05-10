using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IKaaluMootmineRepository
    {
        Task<KaaluMootmine> GetByIdAsync(int id);
        Task SaveAsync(KaaluMootmine mootmine);
        Task DeleteAsync(KaaluMootmine entity);
    }
}
