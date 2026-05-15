using apbd_cw7_git_s33418.Models;

namespace apbd_cw7_git_s33418.Repository
{
    public interface IProductRepository
    {
        Task<List<PC>> GetAllAsync();
        Task<PC?> GetByIdAsync(int id);
        Task<List<PCComponent>?> GetComponentsByPcIdAsync(int id);
        Task AddAsync(PC pc);
        void Delete(PC pc);
        Task SaveChangesAsync();
    }
}
