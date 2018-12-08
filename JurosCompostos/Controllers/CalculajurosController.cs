using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CalculoJuros;

namespace JurosCompostos.Controllers
{
    /// <summary>
    /// classe calculo juros.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CalculajurosController : ControllerBase
    {
        /// <summary>
        /// Realizar o cálculo de juros.
        /// </summary>
        /// <param name="valor">valor inicial</param>
        /// <param name="mes">quantidade de meses</param>
        /// <returns>Objeto contendo todos os valores informados
        /// e o resuldado do cálculo do juros.</returns>
        [HttpGet]
        public IActionResult Get(float valor, int mes)
        {
            Calculo calculo = new Calculo() { valorinicial = valor, meses = mes };
            calculo.Validate();

            if (calculo.Invalid)
            {
                return BadRequest(calculo.Notifications);
            }
            else
            {
                calculo.resultado = Math.Round(calculo.valorinicial * Math.Pow((1 + calculo.juros), (double)calculo.meses), 2);
                return Ok(calculo.resultado);
            }
        }
    }
}
