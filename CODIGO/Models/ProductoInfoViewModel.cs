using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace wizard.Models
{
    public class ProductoInfoViewModel
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        public string? nombre { get; set; }

        [Required(ErrorMessage = "El campo precio es obligatorio")]
        public double precio { get; set; }

    }
}