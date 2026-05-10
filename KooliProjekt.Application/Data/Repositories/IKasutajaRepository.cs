using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IKasutajaRepository
    {
        Task<Kasutaja> GetByIdAsync(int id);
        Task SaveAsync(Kasutaja kasutaja);
        Task DeleteAsync(Kasutaja entity);
    }
}
