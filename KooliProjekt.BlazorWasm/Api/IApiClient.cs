namespace KooliProjekt.BlazorWasm
{
    public interface IApiClient
    {
        Task<OperationResult<Toiduaine>> Get(int id);
        Task<OperationResult<PagedResult<Toiduaine>>> List(int page, int pageSize);
        Task<OperationResult> Save(Toiduaine toiduaine);
        Task<OperationResult> Delete(int id);
    }
}
