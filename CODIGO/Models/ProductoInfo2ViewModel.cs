using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace wizard.Models
{
    public class ProductoInfo2ViewModel

    {
        [Required(ErrorMessage = "La cantidad es obligatoria")]
        public int cantidad { get; set; }

        [Required(ErrorMessage = "La fecha de registro es obligatoria")]
        public DateOnly? fecha_registro { get; set; }


        public bool estado { get; set; }
    }
}