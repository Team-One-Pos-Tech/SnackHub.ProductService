using System;
using MassTransit;

namespace SnackHub.ProductService.Application.Models;

[MessageUrn("snack-hub-products")]
[EntityName("product-deleted")]
public record ProductDeleted(Guid Id);