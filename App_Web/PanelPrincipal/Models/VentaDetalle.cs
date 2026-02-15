using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PanelPrincipal.Models
{
    public class VentaDetalle
    {
        [Key]
        public int DetalleId { get; set; }

        [ForeignKey("Venta")]
        public int VentaId { get; set; }

        [ForeignKey("Producto")]
        public int ProductoId { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Precio_Unitario { get; set; }

        public Venta? Venta { get; set; }
        public Producto? Producto { get; set; }
    }
}
