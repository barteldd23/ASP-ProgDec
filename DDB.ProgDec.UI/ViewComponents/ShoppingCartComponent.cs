using Microsoft.AspNetCore.Mvc;

namespace DDB.ProgDec.UI.ViewComponents
{
    // no 's' in ViewComponent
    public class ShoppingCartComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (HttpContext.Session.GetObject<ShoppingCart>("cart") != null)
            {
                return View(HttpContext.Session.GetObject<ShoppingCart>("cart"));
            }
            else
            {
                return View(new ShoppingCart());
            }
        }
    }
}
