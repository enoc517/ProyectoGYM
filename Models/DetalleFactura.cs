using System;
using System.Collections.Generic;

namespace ProyectoGYM.Models;

public partial class DetalleFactura
{
    public int IdDetalleFactura { get; set; }

    public decimal PrecioUnidad { get; set; }

    public int Cantidad { get; set; }

    public decimal Descuento { get; set; }

    public virtual ICollection<FacturaPedido> FacturaPedidos { get; set; } = new List<FacturaPedido>();
}
