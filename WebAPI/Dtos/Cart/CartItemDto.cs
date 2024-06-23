using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos.Cart
{
    public class CartItemDto
    {
        [Range(1,10)]
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}