using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dtos.Category
{
    public class CreateCategoryDto
    {
        public required string Name { get; set; }
    }
}