using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using SnackHub.ProductService.Application.Contracts;
using SnackHub.ProductService.Application.Models;
using SnackHub.ProductService.Domain.Contracts;
using SnackHub.ProductService.Domain.Entities;

namespace SnackHub.ProductService.Application.UseCases
{
    public class ManageProductUseCase : IManageProductUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public ManageProductUseCase(
            IProductRepository productRepository, 
            IPublishEndpoint publishEndpoint)
        {
            _productRepository = productRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<ManageProductResponse> AddAsync(ManageProductRequest request)
        {
            var product = Product.Create(request.Name, request.Category, request.Price, request.Description, request.Images);
            await _productRepository.AddAsync(product);

            var productAddedEvent = new ProductCreated(product.Id, product.Name, product.Price, product.Description);
            await _publishEndpoint.Publish(productAddedEvent);
            
            return new ManageProductResponse { IsValid = true };
        }

        public async Task<ManageProductResponse> UpdateAsync(Guid id, ManageProductRequest request)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product is null)
                return new ManageProductResponse { IsValid = false, Notifications = new List<string> { "Product not found." } };

            product.Edit(request.Name, request.Category, request.Price, request.Description, request.Images);
            await _productRepository.EditAsync(product);

            var productUpdatedEvent = new ProductUpdated(product.Id, product.Name, product.Price, product.Description);
            await _publishEndpoint.Publish(productUpdatedEvent);
            
            return new ManageProductResponse { IsValid = true };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product is null)
                return false;

            await _productRepository.RemoveAsync(id);
            
            var productDeletedEvent = new ProductDeleted(product.Id);
            await _publishEndpoint.Publish(productDeletedEvent);
            
            return true;
        }
    }
}
