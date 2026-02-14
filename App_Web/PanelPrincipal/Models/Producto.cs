using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Producto
{
    [Key]
    public int ProductoId { get; set; }

    [Required]
    [StringLength(255)]
    public string Producto_Nombre { get; set; }

    [Required]
    [StringLength(255)]
    public string Marca_Articulo { get; set; } = "GENERICO";

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    [Range(0, double.MaxValue)]
    public decimal Precio { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Cantidad_Stock { get; set; }

    public string? Descripcion { get; set; }

    public bool Activo { get; set; } = true;

    [StringLength(500)]
    public string? Imagen_Url { get; set; }

    // Relación
    public ICollection<VentaDetalle>? VentaDetalles { get; set; }
}