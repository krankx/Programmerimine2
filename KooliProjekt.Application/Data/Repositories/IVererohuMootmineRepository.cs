using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IVererohuMootmineRepository
    {
        Task<VererohuMootmine> GetByIdAsync(int id);
        Task SaveAsync(VererohuMootmine mootmine);
        Task DeleteAsync(VererohuMootmine entity);
    }
}
