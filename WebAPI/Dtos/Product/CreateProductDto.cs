using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos.Product
{
    public class CreateProductDto
    {
        public required string Name { get; set; }
        public string? Discretion { get; set; }
        [Range(0.1, 1000)]
        public decimal Price { get; set; }
        [Range(0, 1000)]
        public int Quantity { get; set; }
    }
}