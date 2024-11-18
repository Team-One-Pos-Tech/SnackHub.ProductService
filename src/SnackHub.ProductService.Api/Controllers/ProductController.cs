﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SnackHub.OrderService.Api.Extensions;
using SnackHub.ProductService.Application.Contracts;
using SnackHub.ProductService.Application.Models;
using SnackHub.ProductService.Domain.Entities;

namespace SnackHub.ProductService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1")]
    public class ProductController : ControllerBase
    {
        private readonly IGetProductUseCase _getProductUseCase;
        private readonly IManageProductUseCase _manageProductUseCase;
        private readonly IGetByCategoryUseCase _getByCategoryUseCase;

        public ProductController(IGetProductUseCase getProductUseCase, IManageProductUseCase manageProductUseCase, IGetByCategoryUseCase getByCategoryUseCase)
        {
            _getProductUseCase = getProductUseCase;
            _manageProductUseCase = manageProductUseCase;
            _getByCategoryUseCase = getByCategoryUseCase;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetProductResponse>> GetById([FromRoute] Guid id)
        {
            var productResponse = await _getProductUseCase.Execute(id);
            if (productResponse is null)
                return NotFound();

            return Ok(productResponse);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductResponse>>> GetAll()
        {
            var products = await _getProductUseCase.Execute();
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ManageProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ManageProductResponse>> Post(ManageProductRequest request)
        {
            var response = await _manageProductUseCase.AddAsync(request);

            return response.IsValid ? Ok(response) : ValidationProblem(ModelState.AddNotifications((IEnumerable<Flunt.Notifications.Notification>)response.Notifications));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ManageProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ManageProductResponse>> Put(Guid id, ManageProductRequest request)
        {
            var response = await _manageProductUseCase.UpdateAsync(id, request);

            return response.IsValid ? Ok(response) : ValidationProblem(ModelState.AddNotifications((IEnumerable<Flunt.Notifications.Notification>)response.Notifications));
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _manageProductUseCase.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
        
        [HttpGet("{category:int}")]
        [ProducesResponseType(typeof(IEnumerable<GetProductResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GetProductResponse>>> GetByCategory([FromRoute] Category category)
        {
            var products = await _getByCategoryUseCase.Get(category);

            if (!products.Any())
            {
                return NotFound();
            }
            
            return Ok(products);
        }
    }
}
