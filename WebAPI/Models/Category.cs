using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    [Index(nameof(Name),IsUnique = true)]
    public class Category 
    {
        public int Id { get; set; }

        
        public required string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}