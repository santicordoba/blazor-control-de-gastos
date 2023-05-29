using BlazorControlDeGastos.Model.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorControlDeGastos.Model
{
    public class Gastos 
    {
        public int Id { get; set; }
        [Required]
        [ControlDeGastosFechaValidator(DiasEnElFuturo = 30)]
        public DateTime Fecha { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0")]
        public decimal Monto { get; set; }
        [Required]
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        public TipoGasto TipoGasto { get; set; }

        public event Action EventoSetearGastoSeleccionado;

        public void SetearGastoSeleccionado(Gastos gasto)
        {
            /**
             * Se setean los valores del gasto seleccionado al 
             * cascading parameter que comparte la lista de gastos 
             * con el formulario de edición
             */
            Id = gasto.Id;
            Fecha = gasto.Fecha;
            Monto = gasto.Monto;
            CategoriaId = gasto.CategoriaId;
            TipoGasto = gasto.TipoGasto;
            NotificarSeteoGastoSeleccionado();
        }

        private void NotificarSeteoGastoSeleccionado()
        {
            EventoSetearGastoSeleccionado?.Invoke();
        }

    }
}
