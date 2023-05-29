using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorControlDeGastos.Model.Validation
{
    public class ControlDeGastosFechaValidator : ValidationAttribute
    {
        public int DiasEnElFuturo { get; set; }
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            DateTime fechaTransaccion;
            if(DateTime.TryParse(value.ToString(), out fechaTransaccion))
            {
                //Fecha vacia
                if(fechaTransaccion == DateTime.MinValue)
                {
                    return new ValidationResult("Debe ingresar una fecha", new[] {validationContext.MemberName});
                }
                else if(fechaTransaccion > DateTime.Now.AddDays(DiasEnElFuturo)) 
                {
                    return new ValidationResult("La fecha no puede ser mayor a " + DiasEnElFuturo + " dias en el futuro", new[] { validationContext.MemberName });
                } else
                {
                    return null;
                }

            } else
            {
                return new ValidationResult("Debe ingresar una fecha valida", new[] { validationContext.MemberName });
            }
        }
    }
}
