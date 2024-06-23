using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WebAPI.Data;
using WebAPI.Dtos.Cart;
using WebAPI.Extensions;
using WebAPI.Interface;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepo _cartRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IProductRepo _productRepo;
        public CartController(UserManager<AppUser> userManager, ICartRepo cartRepo, IProductRepo productRepo)
        {
            _cartRepo = cartRepo;
            _userManager = userManager;
            _productRepo = productRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetCart()
        {

            var user = await _userManager.FindByNameAsync(User.GetUserName());
            if (user == null) return Unauthorized();

            var userId = await _userManager.GetUserIdAsync(user);
            return Ok(await _cartRepo.GetCartItems(userId));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddItemToCart(CartItemDto cartItemDto)
        {   
            if(!ModelState.IsValid) return BadRequest(ModelState);
            // check if product exists
            var product = await _productRepo.GetById(cartItemDto.ProductId);
            if(product == null) return NotFound("Product not found");
            if(product.Quantity == 0 || product.Quantity < cartItemDto.Quantity) return BadRequest("Product is out of stock");


            var user = await _userManager.FindByNameAsync(User.GetUserName());
            if (user == null) return Unauthorized();
            var userId = await _userManager.GetUserIdAsync(user);
            var cart = await _cartRepo.GetCartByAppUserId(userId); 
            if(cart != null)
            {
                cart = await _cartRepo.AddCartItem(userId, cartItemDto);
                return Ok(cart);
            }
            // server error
            return StatusCode(500);
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> RemoveItemFromCart(int productId)
        {
            var user = await _userManager.FindByNameAsync(User.GetUserName());
            if (user == null) return Unauthorized();
            var userId = await _userManager.GetUserIdAsync(user);
            var cart = await _cartRepo.GetCartByAppUserId(userId);
            if(cart != null)
            {
                var DeletedItem = await _cartRepo.DeleteCartItem(cart.Id, productId);
                if(DeletedItem == null) return NotFound();
                return Ok(DeletedItem);
            }
            return NotFound();
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdateQuantity(CartItemDto cartItemDto)
        {
            var user = await _userManager.FindByNameAsync(User.GetUserName());
            if (user == null) return Unauthorized();
            var userId = await _userManager.GetUserIdAsync(user);
            var cart = await _cartRepo.GetCartByAppUserId(userId);
            if(cart != null)
            {
                var UpdatedItem = await _cartRepo.UpdateCartItem(cart.Id, cartItemDto);
                if(UpdatedItem == null) return NotFound();
                return Ok(UpdatedItem);
            }
            return NotFound();
        }

    }
}