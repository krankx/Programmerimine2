using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface ISoogikorraRidaRepository
    {
        Task<SoogikorraRida> GetByIdAsync(int id);
        Task SaveAsync(SoogikorraRida rida);
        Task DeleteAsync(SoogikorraRida entity);
    }
}
