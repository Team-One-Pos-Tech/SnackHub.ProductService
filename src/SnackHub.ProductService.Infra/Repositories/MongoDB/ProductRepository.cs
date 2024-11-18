using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SnackHub.ProductService.Domain.Contracts;
using SnackHub.ProductService.Domain.Entities;

namespace SnackHub.ProductService.Infra.Repositories.MongoDB
{
    public sealed class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IMongoDatabase mongoDatabase, string collectionName = "Products")
            : base(mongoDatabase, collectionName)
        {
        }

        public async Task AddAsync(Product? product)
        {
            await InsertAsync(product);
        }

        public async Task EditAsync(Product product)
        {
            await UpdateByPredicateAsync(px => px.Id == product.Id, product);
        }

        public async Task RemoveAsync(Guid id)
        {
            await DeleteByPredicateAsync(product => product.Id == id);
        }

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            return await FindByPredicateAsync(product => product.Id.Equals(id));
        }

        public async Task<Product?> GetProductByNameAsync(string name)
        {
            return await FindByPredicateAsync(product => product.Name.Equals(name));
        }

        public async Task<IEnumerable<Product?>> ListAllAsync()
        {
            return await ListByPredicateAsync(px => true) ?? ArraySegment<Product>.Empty;
        }

        public async Task<IEnumerable<Product?>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            return await ListByPredicateAsync(p => ids.Contains(p.Id));
        }
        
        public async Task<IEnumerable<Product?>> GetByCategory(Category category)
        {
            return await ListByPredicateAsync(product => product.Category == category);
        }
    }

}
