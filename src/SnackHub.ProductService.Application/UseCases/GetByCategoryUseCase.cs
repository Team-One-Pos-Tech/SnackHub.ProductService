using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SnackHub.ProductService.Application.Contracts;
using SnackHub.ProductService.Application.Models;
using SnackHub.ProductService.Domain.Contracts;
using SnackHub.ProductService.Domain.Entities;

namespace SnackHub.ProductService.Application.UseCases;

public class GetByCategoryUseCase(IProductRepository productRepository) : IGetByCategoryUseCase
{
    public async Task<IEnumerable<GetProductResponse>> Get(Category category)
    {
        var products = await productRepository.GetByCategory(category);
        
        return products.Select(product => new GetProductResponse(
            product.Id,
            product.Name,
            product.Category,
            product.Price,
            product.Description,
            product.Images
        ));
    }
}