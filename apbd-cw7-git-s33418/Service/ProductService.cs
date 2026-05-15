using apbd_cw7_git_s33418.DTO;
using apbd_cw7_git_s33418.Models;
using apbd_cw7_git_s33418.Repository;

namespace apbd_cw7_git_s33418.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository productRepository)
        {
            _repository = productRepository;
        }

        public async Task<List<GetComputers>> GetAllAsync()
        {
            var pcs = await _repository.GetAllAsync();

            return pcs.Select(MapPcToDto).ToList();
        }

        public async Task<List<GetComponentsByID>?> GetComponentsByPcIdAsync(int id)
        {
            var pcComponents = await _repository.GetComponentsByPcIdAsync(id);
            if (pcComponents is null)
            {
                return null;
            }

            return pcComponents
                .Select(pcComponent => new GetComponentsByID
                {
                    Code = pcComponent.Component.Code,
                    Name = pcComponent.Component.Name,
                    Description = pcComponent.Component.Description,
                    Type = pcComponent.Component.ComponentType.Name,
                    Manufacturer = pcComponent.Component.ComponentManufacturer.FullName,
                    Amount = pcComponent.Amount
                })
                .ToList();
        }

        public async Task<PostPcAnswer> CreatePCAsync(PostPcRequest request)
        {
            ValidatePcData(request.Name, request.Weight, request.Warranty, request.Stock);

            var pc = new PC
            {
                Name = request.Name.Trim(),
                Weight = request.Weight,
                Warranty = request.Warranty,
                CreatedAt = request.CreatedAt,
                Stock = request.Stock
            };

            await _repository.AddAsync(pc);
            await _repository.SaveChangesAsync();

            return new PostPcAnswer
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock
            };
        }

        public async Task<GetComputers?> UpdatePCAsync(int id, PutPcRequest request)
        {
            ValidatePcData(request.Name, request.Weight, request.Warranty, request.Stock);

            var pc = await _repository.GetByIdAsync(id);
            if (pc is null)
            {
                return null;
            }

            pc.Name = request.Name.Trim();
            pc.Weight = request.Weight;
            pc.Warranty = request.Warranty;
            pc.CreatedAt = request.CreatedAt;
            pc.Stock = request.Stock;

            await _repository.SaveChangesAsync();

            return MapPcToDto(pc);
        }

        public async Task<bool> DeletePCAsync(int id)
        {
            var pc = await _repository.GetByIdAsync(id);
            if (pc is null)
            {
                return false;
            }

            _repository.Delete(pc);
            await _repository.SaveChangesAsync();

            return true;
        }

        private static GetComputers MapPcToDto(PC pc)
        {
            return new GetComputers
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock
            };
        }

        private static void ValidatePcData(string name, float weight, int warranty, int stock)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name is required.");
            }

            if (name.Length > 50)
            {
                throw new ArgumentException("Name cannot exceed 50 characters.");
            }

            if (weight <= 0)
            {
                throw new ArgumentException("Weight must be greater than 0.");
            }

            if (warranty < 0)
            {
                throw new ArgumentException("Warranty cannot be negative.");
            }

            if (stock < 0)
            {
                throw new ArgumentException("Stock cannot be negative.");
            }
        }
    }
}
