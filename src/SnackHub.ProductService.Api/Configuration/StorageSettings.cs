namespace SnackHub.ProductService.Api.Configuration;

public record StorageSettings
{
    public MongoDbSettings MongoDb { get; set; }
}