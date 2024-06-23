using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.Cart;
using WebAPI.Models;

namespace WebAPI.Interface
{
    public interface ICartRepo
    {
        Task<Cart?> GetCartByAppUserId(string appUserId);
        Task<List<CartItem>> GetCartItems(string appUserId);
        Task<Cart> CreateCart(string appUserId);
        Task<Cart?> DeleteCart(string appUserId);
        Task<Cart?> AddCartItem(string appUserId, CartItemDto cartItem);
        Task<CartItem?> UpdateCartItem(int cartId, CartItemDto cartItem);
        Task<CartItem?> DeleteCartItem(int cartId, int ProductId);
    }
}