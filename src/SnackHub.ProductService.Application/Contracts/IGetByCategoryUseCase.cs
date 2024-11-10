using System.Collections.Generic;
using System.Threading.Tasks;
using SnackHub.ProductService.Application.Models;
using SnackHub.ProductService.Domain.Entities;

namespace SnackHub.ProductService.Application.Contracts;

public interface IGetByCategoryUseCase
{
    Task<IEnumerable<GetProductResponse>> Get(Category category);
}