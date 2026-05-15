using apbd_cw7_git_s33418.Data;
using apbd_cw7_git_s33418.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_cw7_git_s33418.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PC>> GetAllAsync()
        {
            return await _context.PCs
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PC?> GetByIdAsync(int id)
        {
            return await _context.PCs
                .FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public async Task<List<PCComponent>?> GetComponentsByPcIdAsync(int id)
        {
            var exists = await _context.PCs.AnyAsync(pc => pc.Id == id);
            if (!exists)
            {
                return null;
            }

            return await _context.PCComponents
                .AsNoTracking()
                .Include(pc => pc.Component)
                    .ThenInclude(component => component.ComponentType)
                .Include(pc => pc.Component)
                    .ThenInclude(component => component.ComponentManufacturer)
                .Where(pc => pc.PCId == id)
                .ToListAsync();
        }

        public async Task AddAsync(PC pc)
        {
            await _context.PCs.AddAsync(pc);
        }

        public void Delete(PC pc)
        {
            _context.PCs.Remove(pc);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
