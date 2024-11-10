using Microsoft.Extensions.DependencyInjection;
using SnackHub.ProductService.Domain.Contracts;
using SnackHub.ProductService.Infra.Repositories.MongoDB;

namespace SnackHub.ProductService.Api.Extensions;

public static class RepositoriesExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IProductRepository, ProductRepository>();

    }
}