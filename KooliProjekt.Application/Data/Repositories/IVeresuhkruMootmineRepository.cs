using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IVeresuhkruMootmineRepository
    {
        Task<VeresuhkruMootmine> GetByIdAsync(int id);
        Task SaveAsync(VeresuhkruMootmine mootmine);
        Task DeleteAsync(VeresuhkruMootmine entity);
    }
}
