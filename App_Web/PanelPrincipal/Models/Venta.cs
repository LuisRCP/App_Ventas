using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Venta
{
    [Key]
    public int VentaId { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Total { get; set; }

    [StringLength(50)]
    public string? Metodo_Pago { get; set; }

    [StringLength(30)]
    public string Estado { get; set; } = "PAGADA";

    // Relación 1:N
    public ICollection<VentaDetalle>? VentaDetalles { get; set; }
}