using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.Interface
{
    public interface IProductRepo
    {
        Task<List<Product>> GetAll();
        Task<Product?> GetById(int id);
        Task<Product> Add(Product product);

        Task<Product?> Delete(int id);

        Task<List<Product>?> GetQueryableAsync(int categoryId, QueryObject query);

        Task<string?> ChangeImage(int id, UploadImage file);

    }
}