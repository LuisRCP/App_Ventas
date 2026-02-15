using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PanelPrincipal.Data;

namespace PanelPrincipal.Controllers.Api
{
    [Route("api/productos")]
    [ApiController]
    public class ProductosApiController : ControllerBase
    {
        private readonly TiendaDbContext _context;

        public ProductosApiController(TiendaDbContext context)
        {
            _context = context;
        }

        // GET: api/productos
        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            var productos = await _context.Productos
                .Where(p => p.Activo && p.Cantidad_Stock > 0)
                .ToListAsync();

            return Ok(productos);
        }
    }
}