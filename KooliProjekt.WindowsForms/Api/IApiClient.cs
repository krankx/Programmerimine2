namespace KooliProjekt.WindowsForms.Api
{
    public interface IApiClient
    {
        Task<OperationResult<PagedResult<Toiduaine>>> List(int page, int pageSize);
        Task<OperationResult> Save(Toiduaine toiduaine);
        Task<OperationResult> Delete(int id);
    }
}
