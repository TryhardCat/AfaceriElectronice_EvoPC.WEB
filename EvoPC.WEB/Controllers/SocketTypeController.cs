using EvoPC.Models.DTOs.VM;
using EvoPC.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EvoPC.WEB.Controllers
{
    [Route("[Controller]")]
    public class SocketTypeController : Controller
    {
        private readonly ISocketTypeService service;

        public SocketTypeController(ISocketTypeService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var list = service.GetAllSocketType();
            return View(list);
        }

        [HttpGet]
        [Route("New")]
        public IActionResult New()
        {
            var dto = new SocketTypeVM();
            return View(dto);
        }

        [HttpPost]
        [Route("New")]
        public IActionResult New(SocketTypeVM dto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Exista o eroare");
                return View(dto);
            }

            service.AddSocketType(dto);
            return View("Index", service.GetAllSocketType());
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var dto = service.GetSocketType(id);
            return View(dto);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        public IActionResult Edit(int id, SocketTypeVM dto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Exista o eroare!");
                return View(dto);
            }

            service.UpdateSocketType(id, dto);
            return View("Index", service.GetAllSocketType());
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public JsonResult Delete(int id)
        {
            service.DeleteSocketType(id);
            return Json(new { succes = true, message = "Stergere cu succes!" });
        }
    }
}
