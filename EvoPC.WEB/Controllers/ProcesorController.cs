using EvoPC.Models.DTOs.VM;
using EvoPC.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvoPC.WEB.Controllers
{
    [Route("[Controller]")]
    public class ProcesorController : Controller
    {
        private readonly IProcesorServices service;

        public ProcesorController(IProcesorServices service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var list = service.GetAllProcesors();
            return View(list);
        }

        [HttpGet]
        [Route("New")]
        public IActionResult New()
        {
            var dto = new ProcesorVM();
            dto.SocketTypes = service.GetSocketTypes();
            return View(dto);
        }

        [HttpPost]
        [Route("New")]
        public IActionResult New(ProcesorVM dto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Exista o eroare");
                dto.SocketTypes = service.GetSocketTypes();
                return View(dto);
            }

            service.AddProcesor(dto);
            return View("Index", service.GetAllProcesors());
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var dto = service.GetProcesor(id);
            dto.SocketTypes = service.GetSocketTypes();
            return View(dto);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        public IActionResult Edit(int id, ProcesorVM dto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Exista o eroare!");
                dto.SocketTypes = service.GetSocketTypes();
                return View(dto);
            }

            service.UpdateProcesor(id, dto);
            return View("Index", service.GetAllProcesors());
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public JsonResult Delete(int id)
        {
            service.DeleteProcesor(id);
            return Json(new { succes = true, message = "Stergere cu succes!" });
        }
    }
}
