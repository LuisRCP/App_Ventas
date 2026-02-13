using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/ventas")]
[ApiController]
public class VentasApiController : ControllerBase
{
    private readonly TiendaDbContext _context;

    public VentasApiController(TiendaDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> RegistrarVenta([FromBody] Venta venta)
    {
        if (venta == null || venta.VentaDetalles == null)
            return BadRequest();

        decimal total = 0;

        foreach (var detalle in venta.VentaDetalles)
        {
            var producto = await _context.Productos.FindAsync(detalle.ProductoId);

            if (producto == null || producto.Cantidad_Stock < detalle.Cantidad)
                return BadRequest("Stock insuficiente");

            producto.Cantidad_Stock -= detalle.Cantidad;
            detalle.Precio_Unitario = producto.Precio;

            total += detalle.Cantidad * producto.Precio;
        }

        venta.Total = total;
        venta.Fecha = DateTime.Now;

        _context.Ventas.Add(venta);
        await _context.SaveChangesAsync();

        return Ok(new { mensaje = "Venta registrada correctamente" });
    }
}