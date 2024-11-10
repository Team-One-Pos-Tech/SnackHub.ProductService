using System;
using MassTransit;

namespace SnackHub.ProductService.Application.Models;

[MessageUrn("snack-hub-products")]
[EntityName("product-updated")]
public record ProductUpdated(Guid Id, string Name, decimal Price, string Description);