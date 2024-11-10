using System;
using MassTransit;

namespace SnackHub.ProductService.Application.Models;

[MessageUrn("snack-hub-products")]
[EntityName("product-created")]
public record ProductCreated(Guid Id, string Name, decimal Price, string Description);