using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IPatsientRepository
    {
        Task<Patsient> GetByIdAsync(int id);
        Task SaveAsync(Patsient patsient);
        Task DeleteAsync(Patsient entity);
    }
}
