using Application.ServiceInterfaces;
using Domain.Dtos;
using Domain.RepositoryInterfaces;

namespace Application.Services
{
    public class ProductService(IProductRepository _productRepository) : IProductService
    {
        public async Task<bool> DeleteAsync(int id)
        {
            return await _productRepository.DeleteAsync(id);
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<int> InsertAsync(ProductInsertDto dto)
        {
            return await _productRepository.InsertAsync(dto);
        }

        public async Task<bool> UpdateAsync(ProductDto dto)
        {
            return await _productRepository.UpdateAsync(dto);
        }
    }
}
