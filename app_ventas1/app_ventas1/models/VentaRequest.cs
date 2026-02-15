using Newtonsoft.Json;

namespace app_ventas1.Models
{
    public class VentaRequest
    {
        [JsonProperty("metodo_Pago")]
        public string MetodoPago { get; set; } = "Efectivo";

        [JsonProperty("estado")]
        public string Estado { get; set; } = "PAGADA";

        [JsonProperty("ventaDetalles")]
        public List<VentaDetalleRequest> VentaDetalles { get; set; } = new();
    }

    public class VentaDetalleRequest
    {
        [JsonProperty("productoId")]
        public int ProductoId { get; set; }

        [JsonProperty("cantidad")]
        public int Cantidad { get; set; }
    }
}