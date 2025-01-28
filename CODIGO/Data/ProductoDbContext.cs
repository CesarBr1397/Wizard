using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using wizard.Entities;

    public class ProductoDbContext : DbContext
    {
        public ProductoDbContext (DbContextOptions<ProductoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Producto { get; set; } = default!;
    }
