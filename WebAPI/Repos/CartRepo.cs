using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Dtos.Cart;
using WebAPI.Interface;
using WebAPI.Models;

namespace WebAPI.Repos
{
    public class CartRepo : ICartRepo
    {
        private readonly AppDBContext _context;
        public CartRepo(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Cart?> AddCartItem(string appUserId, CartItemDto cartItem)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(x => x.AppUserId == appUserId);
            if (cart == null)
            {
                return null;
            }
            if (!cart.CartItems.Any(x => x.ProductId == cartItem.ProductId))
            {
                cart.CartItems.Add(new CartItem
                {
                    CartId = cart.Id,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                });
                _context.Carts.Update(cart);
            }
            else
            {
                return null;
            }


            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart> CreateCart(string appUserId)
        {
            var cart = await _context.Carts.AddAsync(new Cart
            {
                AppUserId = appUserId
            });
            await _context.SaveChangesAsync();
            return cart.Entity;
        }

        public async Task<Cart?> DeleteCart(string appUserId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(x => x.AppUserId == appUserId);
            if (cart == null)
            {
                return null;
            }
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<CartItem?> DeleteCartItem(int cartId, int ProductId)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(x => x.ProductId == ProductId && x.CartId == cartId);
            if (cartItem == null)
            {
                return null;
            }
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task<Cart?> GetCartByAppUserId(string appUserId)
        {
            return await _context.Carts
                .AsNoTracking().Include(x => x.CartItems)
                .FirstOrDefaultAsync(x => x.AppUserId == appUserId);


        }

        public async Task<List<CartItem>> GetCartItems(string appUserId)
        {
            var cart = await _context.Carts.AsNoTracking().FirstOrDefaultAsync(x => x.AppUserId == appUserId);
            if (cart == null)
                return new List<CartItem>();

            var cartItems = await _context.CartItems.Include(x => x.Product).Where(x => x.CartId == cart.Id).ToListAsync();

            return cartItems;
        }
        public async Task<CartItem?> UpdateCartItem(int cartId, CartItemDto cartItem)
        {
            var Item = await _context.CartItems.FirstOrDefaultAsync(x => x.CartId == cartId && x.ProductId == cartItem.ProductId);
            if (Item == null)
            {
                return null;
            }
            Item.Quantity = cartItem.Quantity;
            await _context.SaveChangesAsync();
            return Item;
        }
    }
}