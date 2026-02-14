using Newtonsoft.Json;

namespace app_ventas1.Models
{
    // Clase principal de la venta (Cabecera)
    public class VentaRequest
    {
        [JsonProperty("total")]
        public decimal Total { get; set; }

        [JsonProperty("metodo_pago")]
        public string MetodoPago { get; set; } = "Efectivo"; // Valor por defecto

        [JsonProperty("detalles")]
        public List<VentaDetalle> Detalles { get; set; } = new List<VentaDetalle>();
    }

    // Clase para los renglones de la venta (Detalle)
    public class VentaDetalle
    {
        [JsonProperty("productoId")]
        public int ProductoId { get; set; }

        [JsonProperty("cantidad")]
        public int Cantidad { get; set; }

        [JsonProperty("precio_unitario")]
        public decimal PrecioUnitario { get; set; }
    }
}