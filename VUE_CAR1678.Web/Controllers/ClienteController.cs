using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using VUE_CAR1678.Datos;

namespace VUE_CAR1678.Web.Controllers {
    [EnableCors("Todos")]
    [Route("[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase {
        private readonly IConfiguration _configuration;
        private readonly Context _context;

        public ClienteController(IConfiguration configuration, Context context) {
            _configuration = configuration;
            _context = context;
        }
        
        /// <summary>
        /// Retorna todos los clientes
        /// </summary>
        // POST: api/Cliente/GetClientes
        [HttpGet("[action]")]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(String), 200)]
        public async Task<IActionResult> GetClientes() {
            var clientes = _context.CLIENTES.ToList();

            return await Task.FromResult(Ok(clientes));
        }
    }
}