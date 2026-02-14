using Newtonsoft.Json; // <--- Importante para que funcione el mapeo

namespace app_ventas1.Models
{
    public class Producto
    {
        [JsonProperty("productoId")]
        public int Id { get; set; }

        [JsonProperty("producto_Nombre")] // La API manda "producto_Nombre", tú usas "Nombre"
        public string Nombre { get; set; }

        [JsonProperty("marca_Articulo")]
        public string Marca { get; set; }

        [JsonProperty("precio")]
        public decimal Precio { get; set; }

        [JsonProperty("cantidad_Stock")]
        public int Stock { get; set; }

        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("imagen_url")]
        public string ImagenUrl { get; set; } // Nuevo campo que vi en tu BD

        [JsonProperty("activo")]
        public bool Activo { get; set; } // BIT en SQL es bool en C#

        // Propiedad LOCAL (no va a la BD, solo para tu carrito)
        [JsonIgnore]
        public int CantidadSolicitada { get; set; }
    }
}