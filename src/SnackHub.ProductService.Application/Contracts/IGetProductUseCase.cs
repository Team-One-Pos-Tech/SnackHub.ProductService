using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SnackHub.ProductService.Application.Models;

namespace SnackHub.ProductService.Application.Contracts
{
    public interface IGetProductUseCase
    {
        Task<GetProductResponse?> Execute(Guid id);
        Task<IEnumerable<GetProductResponse>> Execute();
    }
}
