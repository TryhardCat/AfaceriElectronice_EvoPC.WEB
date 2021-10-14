using EvoPC.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using EvoPC.WEB;
using System.Collections.Generic;

namespace EvoPC.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProcesorServices procesorService;

        public HomeController(IProcesorServices procesorService)
        {
            this.procesorService = procesorService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var procesor = procesorService.GetAllProcesors();
            return View(procesor);
        }

        [HttpGet]
        [Route("Details/{id}")]
        public IActionResult Details(int id)
        {
            var procesor = procesorService.GetProcesor(id);
            return View(procesor);
        }

        [HttpPost]
        [Route("Add/{id}")]

        public IActionResult Add(int id)
        {
            var shopList = HttpContext.Session.Get<List<int>>(SessionHelper.ShoppingCart);

            if (shopList == null)
                shopList = new List<int>();

            if (!shopList.Contains(id))
                shopList.Add(id);

            HttpContext.Session.Set(SessionHelper.ShoppingCart, shopList);

            return RedirectToAction("Index", "Home", procesorService.GetAllProcesors());
        }

        [HttpPost]
        [Route("Remove/{id}")]

        public IActionResult Remove(int id)
        {
            var shopList = HttpContext.Session.Get<List<int>>(SessionHelper.ShoppingCart);

            if (shopList == null)
                return RedirectToAction("Index", "Home", procesorService.GetAllProcesors());

            if (shopList.Contains(id))
                shopList.Remove(id);

            HttpContext.Session.Set(SessionHelper.ShoppingCart, shopList);

            return RedirectToAction("Index", "Home", procesorService.GetAllProcesors());
        }

    }
}
