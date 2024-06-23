using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos.Category;
using WebAPI.Helpers;
using WebAPI.Interface;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/Category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo _categoryRepo;
        public CategoryController(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _categoryRepo.GetAllAsync());
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName([FromRoute] string name, [FromQuery] QueryObject query)
        {
            var category = await _categoryRepo.GetQueryableAsync(name, query);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                var skipNumber = (query.pageNumber - 1) * query.pageSize;
                category.Products = category.Products.Skip(skipNumber).Take(query.pageSize).ToList();
                return Ok(category.Products);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = await _categoryRepo.GetByNameAsync(categoryDto.Name);
            if (category != null)
            {
                return BadRequest("This category already exists");
            }
            await _categoryRepo.CreateAsync(new Category { Name = categoryDto.Name });
            return Created();
        }

    }
}