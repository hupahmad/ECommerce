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
    public class CategoryRepo : ICategoryRepo
    {
        private readonly AppDBContext _context;
        public CategoryRepo(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return false;
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return true;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.Include(p => p.Products).ToListAsync();
        }

        public Task<Category> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
            return category;
        }

        public async Task<Category?> GetQueryableAsync(string name, QueryObject query)
        {
            var category = await _context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
            if (category == null)
                return null;

            if (!query.IsDecSending)
            {
                switch (query.SortBy)
                {
                    case "price":
                        category.Products = category.Products
                            .OrderByDescending(x => x.Price)
                            .ToList();
                        break;

                    case "quantity":
                        category.Products = category.Products
                            .OrderByDescending(x => x.Quantity)
                            .ToList();
                        break;

                    case "name":
                        category.Products = category.Products
                            .OrderByDescending(x => x.Name)
                            .ToList();
                        break;

                    default:

                        break;
                }
            }
            else
            {
                switch (query.SortBy)
                {
                    case "price":
                        category.Products = category.Products
                            .OrderBy(x => x.Price)
                            .ToList();
                        break;

                    case "quantity":
                        category.Products = category.Products
                            .OrderBy(x => x.Quantity)
                            .ToList();
                        break;

                    case "name":
                        category.Products = category.Products
                            .OrderBy(x => x.Name)
                            .ToList();
                        break;

                    default:

                        break;
                }

            }

            return category;
        }

        public Task<Category> UpdateAsync(Category category)
        {
            throw new NotImplementedException();
        }
    }
}