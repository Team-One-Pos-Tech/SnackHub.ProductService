using Microsoft.Extensions.DependencyInjection;

namespace SnackHub.ProductService.Api.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }
}