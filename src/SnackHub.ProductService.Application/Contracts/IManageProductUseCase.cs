using System;
using System.Threading.Tasks;
using SnackHub.ProductService.Application.Models;

namespace SnackHub.ProductService.Application.Contracts
{
    public interface IManageProductUseCase
    {
        Task<ManageProductResponse> AddAsync(ManageProductRequest request);
        Task<ManageProductResponse> UpdateAsync(Guid id, ManageProductRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
