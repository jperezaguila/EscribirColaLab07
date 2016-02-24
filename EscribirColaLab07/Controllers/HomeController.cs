using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EscribirColaLab07.Models.Utils;

namespace EscribirColaLab07.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(String info)
        {
            var cc = ConfigurationManager.AppSettings["conexioncola"];
            ManejadorColas.Instancia.CrearCola(cc, "incidencias", 1024, 86400);
            var d = new Dictionary<string, string>()
            {
                {"incidencia", info},
                {"fecha", DateTime.Now.ToLongDateString()}
            };
            ManejadorColas.Instancia.Enviar(cc, "incidencias", d, "Indicdencia");

            return RedirectToAction("Index");
        }
    }
}