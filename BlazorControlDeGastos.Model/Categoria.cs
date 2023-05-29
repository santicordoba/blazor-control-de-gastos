using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorControlDeGastos.Model
{
    public class Categoria
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe ingresar una categoria")]
        [MinLength(4, ErrorMessage = "Debe tener al menos 4 caracteres")]
        public string DescCategoria { get; set; }
    }
}
