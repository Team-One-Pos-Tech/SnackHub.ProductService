using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SnackHub.ProductService.Domain.Entities;

namespace SnackHub.ProductService.Domain.Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product?>> GetByIdsAsync(IEnumerable<Guid> ids);
        Task AddAsync(Product? product);
        Task EditAsync(Product product);
        Task RemoveAsync(Guid id);
        Task<Product?> GetProductByIdAsync(Guid id);
        Task<Product?> GetProductByNameAsync(string name);
        Task<IEnumerable<Product?>> ListAllAsync();
        Task<IEnumerable<Product?>> GetByCategory(Category category);
    }
}
