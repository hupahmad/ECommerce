using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebAPI.Helpers;

namespace WebAPI.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public required string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        
    }
}