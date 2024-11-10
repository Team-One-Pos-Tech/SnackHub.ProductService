using System.Collections.Generic;
using Flunt.Notifications;
using SnackHub.ProductService.Domain.Entities;

namespace SnackHub.ProductService.Application.Models
{
    public class ManageProductRequest : Notifiable<Notification>
    {
        public string? Name { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public List<string>? Images { get; set; }
    }
}
