using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace JurosCompostos.Controllers
{
    /// <summary>
    /// classe show me the code.
    /// </summary>
    [Route("api/[controller]")]
    public class ShowmethecodeController : Controller
    {
        /// <summary>
        /// Git Hub autor do projeto.
        /// </summary>
        /// <returns>git hub autor do projeto</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("https://github.com/jeancalves/calculoJuros.git");
        }
    }
}
