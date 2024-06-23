using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos.Product;
using WebAPI.Helpers;
using WebAPI.Interface;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _productRepo;
        private readonly ICategoryRepo _categoryRepo;
        public ProductController(IProductRepo productRepo, ICategoryRepo categoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            return Ok(await _productRepo.GetAll());
        }


        [HttpPost("{CategoryName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateProductDto productDto, [FromRoute] string CategoryName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = await _categoryRepo.GetByNameAsync(CategoryName);
            if (category == null)
            {
                return NotFound();
            }
            else
            {

                var product = await _productRepo.Add(new Product
                {
                    Name = productDto.Name,
                    Discretion = productDto.Discretion,
                    Price = productDto.Price,
                    Quantity = productDto.Quantity,
                    CategoryId = category.Id
                });
                if (product == null)
                {
                    return BadRequest();
                }
                return Created();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id){
            var product = await _productRepo.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productRepo.Delete(id);
            return Ok();
        }
        
        [HttpPost("{id}/image")]
        public async Task<IActionResult> UploadImage(int id,[FromForm] UploadImage file)
        {
            try
            {
                
                if (file.File.Length == 0)
                    return BadRequest("File is empty");

                if (file.File.Length > 4194304)
                    return BadRequest("Image size must be less than 4 MB");
                
                if(!file.File.ContentType.Contains("jpeg") && !file.File.ContentType.Contains("png") && !file.File.ContentType.Contains("jpg"))
                    return BadRequest("File must be an image (png, jpeg, jpg)");

                var type = file.File.ContentType;

                var fileName = await _productRepo.ChangeImage(id, file);
                if(fileName == null)
                {
                    return BadRequest("product not found");
                }

                return Created();
            }
            catch (System.Exception)
            {
                return BadRequest();
                throw;
            }
        }

        [HttpGet("image/{image}")]
        public IActionResult GetImage(string image)
        {
            // Specify the path where the images are stored on the server
            var imagePath = Path.Combine("wwwroot","images","product", image);

            if (System.IO.File.Exists(imagePath))
            {
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                if(image.Contains(".png")){
                    return File(imageBytes, "image/png");
                }
                else if(image.Contains(".jpg")){
                    return File(imageBytes, "image/jpg");
                }
                else if(image.Contains(".jpeg")){
                    return File(imageBytes, "image/jpeg");
                }
            }

            return NotFound("Image not found");
        }
    }
}