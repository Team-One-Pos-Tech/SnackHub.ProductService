namespace SnackHub.ProductService.Api.Configuration;

public record MongoDbSettings
{
    public required string ConnectionString { get; init; }
    public required string Database { get; init; }
}