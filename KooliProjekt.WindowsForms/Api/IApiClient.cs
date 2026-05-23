namespace KooliProjekt.WindowsForms.Api
{
    public interface IApiClient
    {
        Task<OperationResult<PagedResult<Toiduaine>>> List(int page, int pageSize);
        Task Save(Toiduaine toiduaine);
        Task Delete(int id);
    }
}
