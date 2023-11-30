using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDB.ProgDec.UI.Controllers
{
    public class ShoppingCartController : Controller
    {

        ShoppingCart cart;

        // GET: ShoppingCartController
        public IActionResult Index()
        {
            ViewBag.Title = "Shopping Cart";
            cart = GetShoppingCart();
            return View(cart);
        }

        private ShoppingCart GetShoppingCart()
        {
            if (HttpContext.Session.GetObject<ShoppingCart>("cart") != null)
            {
                return HttpContext.Session.GetObject<ShoppingCart>("cart");
            }
            else
            {
                return new ShoppingCart();
            }
        }

        public IActionResult Remove(int id)
        {
            cart = GetShoppingCart();
            
            //do this way to prevent a hit on the DB!
            Declaration declaration = cart.Items.FirstOrDefault(item => item.Id == id);

            ShoppingCartManager.Remove(cart, declaration);
            HttpContext.Session.SetObject("cart", cart);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Add(int id)
        {
            cart = GetShoppingCart();
            Declaration declaration = DeclarationManager.LoadByID(id);

            ShoppingCartManager.Add(cart, declaration);
            HttpContext.Session.SetObject("cart", cart);
            return RedirectToAction(nameof(Index), "Declaration");
        }

        public IActionResult Checkout()
        {
            cart = GetShoppingCart();
            ShoppingCartManager.Checkout(cart);

            //no more cart after we check out
            HttpContext.Session.SetObject("cart", null);
            return View();
        }

    }

}
        

