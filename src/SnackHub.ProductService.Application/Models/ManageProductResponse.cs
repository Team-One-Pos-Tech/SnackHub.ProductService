using System.Collections.Generic;
using Flunt.Notifications;

namespace SnackHub.ProductService.Application.Models
{
    public class ManageProductResponse : Notifiable<Notification>
    {
        public bool IsValid { get; set; }
        public List<string> Notifications { get; set; }
    }
}
