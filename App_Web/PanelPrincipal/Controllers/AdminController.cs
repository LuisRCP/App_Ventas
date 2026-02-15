using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PanelPrincipal.Data;

namespace PanelPrincipal.Controllers
{
    public class AdminController : Controller
    {
        private readonly TiendaDbContext _context;

        public AdminController(TiendaDbContext context)
        {
            _context = context;
        }

        // DASHBOARD
        public async Task<IActionResult> Index()
        {
            var totalProductos = await _context.Productos.CountAsync();
            var totalVentas = await _context.Ventas.CountAsync();
            var totalIngresos = await _context.Ventas.SumAsync(v => (decimal?)v.Total) ?? 0;

            ViewBag.TotalProductos = totalProductos;
            ViewBag.TotalVentas = totalVentas;
            ViewBag.TotalIngresos = totalIngresos;

            return View();
        }

        // HISTORIAL DE VENTAS
        public async Task<IActionResult> Ventas()
        {
            var ventas = await _context.Ventas
                .Include(v => v.VentaDetalles)
                .ThenInclude(d => d.Producto)
                .OrderByDescending(v => v.Fecha)
                .ToListAsync();

            return View(ventas);
        }

        // DETALLE DE UNA VENTA
        public async Task<IActionResult> VentaDetalle(int id)
        {
            var venta = await _context.Ventas
                .Include(v => v.VentaDetalles)
                .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(v => v.VentaId == id);

            if (venta == null)
                return NotFound();

            return View(venta);
        }
    }
}