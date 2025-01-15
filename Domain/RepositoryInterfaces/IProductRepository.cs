using Domain.Dtos;

namespace Domain.RepositoryInterfaces
{
    public interface IProductRepository
    {
        Task<List<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task<int> InsertAsync(ProductInsertDto dto);
        Task<bool> UpdateAsync(ProductDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
