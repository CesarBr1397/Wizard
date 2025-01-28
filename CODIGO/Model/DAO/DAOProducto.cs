using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wizard.Entities;

namespace wizard.Model.DAO
{
    public class DAOProducto
    {
        private readonly ProductoDbContext context;

        public DAOProducto(ProductoDbContext context)
        {
            this.context = context;
        }

        public void AddProducto(Producto producto)
        {
            context.Producto.Add(producto);
            context.SaveChanges();
        }

        public async Task<List<Producto>> GetProductos()
        {
            return await context.Producto
                .OrderBy(p => p.idproducto)
                .Where(x => x.estado == true)
                .ToListAsync();
        }

        public Producto GetById(int id)
        {
            var producto = context.Producto.Find(id);
            return producto;
        }

        public Producto UpdateProducto(Producto producto)
        {
            context.Producto.Update(producto);
            context.SaveChanges();
            return producto;
        }

        public Producto Delete(Producto producto)
        {
            var produ = context.Producto.Find(producto.idproducto);
            produ.estado = false;
            context.SaveChanges();
            return produ;
        }

    }
}