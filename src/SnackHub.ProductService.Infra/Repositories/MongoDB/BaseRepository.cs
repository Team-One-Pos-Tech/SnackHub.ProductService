using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using SnackHub.ProductService.Infra.Repositories.Abstractions;

namespace SnackHub.ProductService.Infra.Repositories.MongoDB;

public abstract class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : class
{
    protected readonly IMongoCollection<TModel?> MongoCollection;

    protected BaseRepository(IMongoDatabase mongoDatabase, string collectionName = "")
    {
        if (mongoDatabase is null)
            throw new ArgumentException("MongoDatabase should not be null!");

        MongoCollection = mongoDatabase.GetCollection<TModel?>(collectionName == string.Empty ? nameof(TModel) : collectionName);
    }

    public async Task InsertAsync(TModel? model)
    {
        await MongoCollection.InsertOneAsync(model);
    }
    
    public async Task UpdateByPredicateAsync(Expression<Func<TModel, bool>> predicate, TModel? model)
    {
        await MongoCollection.ReplaceOneAsync(predicate, model);
    }

    public async Task DeleteByPredicateAsync(Expression<Func<TModel, bool>> predicate)
    {
        await MongoCollection.DeleteOneAsync(predicate);
    }

    public async Task<TModel?> FindByPredicateAsync(Expression<Func<TModel?, bool>> predicate)
    {
        var cursor = await MongoCollection.FindAsync(predicate);
        return await cursor.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TModel?>> ListByPredicateAsync(Expression<Func<TModel?, bool>> predicate)
    {
        var cursor = await MongoCollection.FindAsync(predicate);
        return await cursor.ToListAsync();
    }
}