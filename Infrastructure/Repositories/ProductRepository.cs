using Dapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Database;

namespace Infrastructure.Repositories
{
    public class ProductRepository(ISqlConnectionFactory _connectionFactory) : IProductRepository
    {
        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                const string query = "DELETE FROM Products WHERE Id = @id";
                var rowsAffected = await connection.ExecuteAsync(query, new { id }); //new{}
                // Возвращает true, если была удалена хотя бы одна строка
                return rowsAffected > 0;
            }
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                const string query = "select * from Products";
                var products = await connection.QueryAsync<Product>(query);
                var dtos = products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Amount = p.Amount,
                    Price = p.Price,
                    ProductName = p.ProductName,

                }).ToList();
                return dtos;
            }
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                const string query = "select * from Products where Id=@id";
                var product = await connection.QueryFirstOrDefaultAsync<Product>(query, new { id });
                var dto = new ProductDto
                {
                    Id = product.Id,
                    Amount = product.Amount,
                    Price = product.Price,
                    ProductName = product.ProductName,
                };
                return dto;
            }
        }

        public async Task<int> InsertAsync(ProductInsertDto dto)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                const string query = @"
                    insert into Products(ProductName,Amount,Price)
                    Values(@ProductName,@Amount,@Price);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                var productId = await connection.ExecuteScalarAsync<int>(query, new
                {
                    dto.ProductName,
                    dto.Amount,
                    dto.Price,
                });
                return productId;
            }
        }

        public async Task<bool> UpdateAsync(ProductDto dto)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                const string query = @"
                    UPDATE Products
                    SET ProductName = @ProductName,
                        Amount = @Amount,
                        Price = @Price
                    WHERE Id = @Id";

                var rowsAffected = await connection.ExecuteAsync(query, new
                {
                    dto.ProductName,
                    dto.Amount,
                    dto.Price,
                    dto.Id
                });

                // Возвращает true, если хотя бы одна строка была обновлена
                return rowsAffected > 0;
            }
        }
    }
}
