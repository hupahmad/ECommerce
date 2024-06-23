using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        [Range(1,10)]
        public int Quantity { get; set; }
        public int CartId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}