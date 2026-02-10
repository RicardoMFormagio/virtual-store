using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VShop.CartApi.Context;
using VShop.CartApi.DTOs;
using VShop.CartApi.Models;

namespace VShop.CartApi.Repositories;

public class CartRepository : ICartRepository
{
    private readonly AppDbContext _context;
    private IMapper _mapper;
    
    public CartRepository(AppDbContext appDbContext, IMapper mapper)
    {
        _context = appDbContext;
        _mapper = mapper;
    }
    
    public async Task<CartDTO> GetCartByUserIdAsync(string userId)
    {
        Cart cart = new Cart
        {
            CartHeader = await _context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId)
        };

        cart.CartItems = _context.CartItems.Where(c => c.CartHeaderId == cart.CartHeader.Id).Include(c => c.Product);

        return _mapper.Map<CartDTO>(cart);
    }

    public async Task <bool> DeleteItemCartAsync(int cartItemId)
    {
        try
        {
            CartItem cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == cartItemId);

            int totalItemsCount = _context.CartItems.Count(c => c.CartHeaderId == cartItem.CartHeaderId);
            
            _context.CartItems.Remove(cartItem);

            if (totalItemsCount == 1)
            {
                var cartHeaderToRemove =
                    await _context.CartHeaders.FirstOrDefaultAsync(ch => ch.Id == cartItem.CartHeaderId);
                _context.CartHeaders.Remove(cartHeaderToRemove);
            }
            
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    public async Task <bool> CleanCartAsync(string userId)
    {
        throw new NotImplementedException();
    }
    
    public async Task <CartDTO> UpdateCartAsync(CartDTO cart)
    {
        throw new NotImplementedException();
    }
    

    public async Task <bool> ApplyCouponAsync(string userId, string couponCode)
    {
        throw new NotImplementedException();
    }

    public async Task <bool> DeleteCouponAsync(string userId)
    {
        throw new NotImplementedException();
    }

    
}