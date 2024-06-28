using Microsoft.AspNetCore.Mvc;

namespace Сосновский.UI.Components
{
    public class CartViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            //var cart = HttpContext.Session.Get<Cart>("cart");
            return View(/*cart*/);
        }
    }
}
