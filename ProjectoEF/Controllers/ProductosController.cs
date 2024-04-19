//Rodrigo Herrera 256423
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ProjectoEF.Models;


namespace ProjectoEF.Controllers
{
    public class ProductosController : Controller
    {
        mitiendaEntities contexto = new mitiendaEntities();
        // GET: Productos
        public ActionResult Productos()
        {

            if (TempData["msj"]   != null)
            {
                ViewBag.msj = TempData["msj"];
            }
            //llamar los productos
            List<productos> data = contexto.productos.ToList();
            //enviar a la vista
            ViewBag.data = data;

            //llamar las categorias
            ViewBag.combo = contexto.categorias.Select(x => new SelectListItem
            {
                Text = x.nombrecat,
                Value = x.codcat.ToString()
            });

            return View();
        }
        [HttpPost]
        public ActionResult Productos(productos p)
        {
            string accion = Request.Form["boton"].ToString();
            string eliminacion = Request.Form["eliminacion"].ToString();
            string modificacion = Request.Form["modificacion"].ToString();


            switch (accion)
            {
                case "Guardar":
                    contexto.productos.Add(p);
                    contexto.SaveChanges();
                    TempData["msj"] = "Guardado";
                    break;

                case "Modificar":
                    if (modificacion.Equals("si"))
                    {
                        productos temp = contexto.productos.FirstOrDefault(x => x.productId == p.productId);
                        temp.nombre = p.nombre;
                        temp.precio = p.precio;
                        temp.existencia = p.existencia;
                        temp.codcat = p.codcat;

                        contexto.SaveChanges();
                        TempData["msj"] = "Modificado";
                    }

                    break;


                case "Eliminar":
                    if (eliminacion.Equals("si"))
                    {
                        contexto.productos.Remove(contexto.productos.FirstOrDefault(x => x.productId == p.productId));
                        contexto.SaveChanges();
                        TempData["msj"] = "Eliminado";
                    }
                    
                    break;
            }
            return RedirectToAction("Productos");
        }

    }
}