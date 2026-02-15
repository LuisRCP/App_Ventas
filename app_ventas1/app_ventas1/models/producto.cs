using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;

namespace app_ventas1.Models
{
    public partial class Producto : ObservableObject
    {
        [JsonProperty("productoId")]
        public int Id { get; set; }

        [JsonProperty("producto_Nombre")]
        public string Nombre { get; set; }

        [JsonProperty("marca_Articulo")]
        public string Marca { get; set; }

        [JsonProperty("precio")]
        public decimal Precio { get; set; }

        [JsonProperty("cantidad_Stock")]
        public int Stock { get; set; }

        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("imagen_Url")]
        public string ImagenUrl { get; set; }

        [JsonProperty("activo")]
        public bool Activo { get; set; }

        [ObservableProperty]
        private int cantidadSolicitada;
    }
}