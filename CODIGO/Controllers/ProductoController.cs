using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using wizard.Entities;
using wizard.Model.DAO;
using wizard.Models;
using wizard.Utils;

namespace wizard.Controllers
{

    public class ProductoController : Controller
    {
        private readonly ProductoDbContext context;

        public ProductoController(ProductoDbContext context)
        {
            this.context = context;
        }

        private Producto GetProducto()
        {
            if (HttpContext.Session.GetObject<Producto>("DataObject") == null)
            {
                HttpContext.Session.SetObject("DataObject", new Producto());
            }
            return HttpContext.Session.GetObject<Producto>("DataObject");
        }

        private void RemoveProducto()
        {
            HttpContext.Session.SetObject("DataObject", null);
        }


        public async Task<IActionResult> Index()
        {
            DAOProducto dao = new DAOProducto(context);
            List<Producto> productos = await dao.GetProductos();
            return View(productos);
        }

        public IActionResult Agregar()
        {
            return View("InformacionPrincipal");
        }

        [HttpPost]
        public ActionResult BasicInfo(ProductoInfoViewModel personal, string BtnNext)
        {
            if (BtnNext != null)
            {
                if (ModelState.IsValid)
                {
                    Producto produ = GetProducto();
                    produ.nombre = personal.nombre;
                    produ.precio = personal.precio;

                    HttpContext.Session.SetObject("DataObject", produ);

                    return View("InformacionSecundaria");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult LaboralInfo(ProductoInfo2ViewModel detalle, string? BtnPrevious, string? BtnNext, string? BtnCancel)
        {
            DAOProducto dao = new DAOProducto(context);
            Producto producto = GetProducto();

            if (BtnPrevious != null)
            {
                ProductoInfoViewModel info = new ProductoInfoViewModel
                {
                    nombre = producto.nombre,
                    precio = producto.precio
                };

                return View("InformacionPrincipal", info);
            }

            if (BtnNext != null)
            {
                if (ModelState.IsValid)
                {
                    // Convertir fecha_registro desde string a DateOnly
                    if (detalle.fecha_registro != null)
                    {
                        producto.fecha_registro = detalle.fecha_registro.Value;
                    }
                    else
                    {
                        // Asignar una fecha predeterminada si no se proporciona
                        producto.fecha_registro = DateOnly.FromDateTime(DateTime.Now);
                    }

                    producto.cantidad = detalle.cantidad;
                    producto.estado = detalle.estado = true;

                    dao.AddProducto(producto);
                    RemoveProducto();

                    return View("Completado", producto);
                }

                // Extraer errores del ModelState y enviarlos a la vista
                ViewData["Error"] = string.Join("; ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                return View(detalle);
            }

            if (BtnCancel != null)
            {
                RemoveProducto();
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Detalles(int id)
        {
            DAOProducto dao = new DAOProducto(context);
            var detalles = dao.GetById(id);
            return View(detalles);
        }

        public ActionResult Actualizar(int id)
        {
            DAOProducto dao = new DAOProducto(context);
            var producto = dao.GetById(id);
            return View(producto);
        }

        public IActionResult Update(Producto producto)
        {
            DAOProducto dao = new DAOProducto(context);
            dao.UpdateProducto(producto);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            DAOProducto dao = new DAOProducto(context);
            var detalles = dao.GetById(id);

            return View(detalles);
        }

        public IActionResult Borrar(Producto producto)
        {
            DAOProducto dao = new DAOProducto(context);
            dao.Delete(producto);

            return RedirectToAction("Index");
        }

    }


}
