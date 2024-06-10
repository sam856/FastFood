using FastFoodModels;
using FastFoodRepositry1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Online_Fast_food_Delievery.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        public CartController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var item = await _context.Item
                                     .Include(x => x.Category)
                                     .Include(y => y.SubCategory)
                                     .Where(x => x.Id == id)
                                     .FirstOrDefaultAsync();

            if (item == null)
            {
                return NotFound();
            }

            var cart = new Cart
            {
               Item = item,
               ItemId = item.Id,

        };

            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
       
        public async Task<IActionResult> Details(Cart cart)
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            cart.ApplicationUserId = claim.Value;
            var cartFromDb = await _context.Cart.Where(x =>
                x.ApplicationUserId == cart.ApplicationUserId
                && x.ItemId == cart.ItemId).FirstOrDefaultAsync();
            cart.Id = 0;

            if (cartFromDb == null)
            {
              
                _context.Cart.Add(cart);
            }
            else
            {
                cartFromDb.Count += cart.Count;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            var cartItems = await _context.Cart
                .Include(c => c.Item)
                .Where(c => c.ApplicationUserId == userId)
                .ToListAsync();

            return View(cartItems);
        }


        [HttpPost]
        public async Task<IActionResult> Add(int id)
        {
            var cart = await _context.Cart.FindAsync(id);
            if (cart != null)
            {
                cart.Count++;
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var cart = await _context.Cart.FindAsync(id);
            if (cart != null)
            {
                if (cart.Count > 1)
                {
                    cart.Count--;
                }
                else
                {
                    _context.Cart.Remove(cart);
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var cart = await _context.Cart.FindAsync(id);
            if (cart != null)
            {
              
                    _context.Cart.Remove(cart);
                
                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}

