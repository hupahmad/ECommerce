using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Helpers;
using WebAPI.Interface;
using WebAPI.Models;

namespace WebAPI.Repos
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDBContext _context;
        public ProductRepo(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Product> Add(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<string?> ChangeImage(int id, UploadImage file)
        {
            var fileName = file.FileName + id.ToString();
            if (file.File.ContentType.Contains("png"))
            {
                fileName += ".png";
            }
            else if(file.File.ContentType.Contains("jpg")){
                fileName += ".jpg";
            }
            else if(file.File.ContentType.Contains("jpeg")){
                fileName += ".jpeg";
            }
            else{
                return null;
            }
            
            var filePath = Path.Combine("wwwroot", "images", "product", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.File.CopyToAsync(stream);
            }

            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return null;
            }
            product.ImageUrl = fileName;
            await _context.SaveChangesAsync();
            return fileName;
        }

        public async Task<Product?> Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return null;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Product>?> GetQueryableAsync(int categoryId, QueryObject query)
        {
            var products = await _context.Products.Where(x => x.CategoryId == categoryId).ToListAsync();

            return products;
        }
    }
}