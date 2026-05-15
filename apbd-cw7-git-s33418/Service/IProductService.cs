using apbd_cw7_git_s33418.DTO;

namespace apbd_cw7_git_s33418.Service
{
    public interface IProductService
    {
        Task<List<GetComputers>> GetAllAsync();
        Task<List<GetComponentsByID>?> GetComponentsByPcIdAsync(int id);
        Task<PostPcAnswer> CreatePCAsync(PostPcRequest request);
        Task<GetComputers?> UpdatePCAsync(int id, PutPcRequest request);
        Task<bool> DeletePCAsync(int id);
    }
}
