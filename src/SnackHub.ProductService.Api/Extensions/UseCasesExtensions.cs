using Microsoft.Extensions.DependencyInjection;
using SnackHub.ProductService.Application.Contracts;
using SnackHub.ProductService.Application.UseCases;

namespace SnackHub.ProductService.Api.Extensions;

public static class UseCasesExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<IGetProductUseCase, GetProductUseCase>()
            .AddScoped<IManageProductUseCase, ManageProductUseCase>()
            .AddScoped<IGetByCategoryUseCase, GetByCategoryUseCase>();
    }
}