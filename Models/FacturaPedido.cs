using System;
using System.Collections.Generic;

namespace ProyectoGYM.Models;

public partial class FacturaPedido
{
    public int IdfacturaPedidos { get; set; }

    public int IdDetalleFactura { get; set; }

    public int Idempleado { get; set; }

    public DateOnly FechaPedido { get; set; }

    public DateOnly FechaRecibido { get; set; }

    public virtual DetalleFactura IdDetalleFacturaNavigation { get; set; }

    public virtual Empleado IdempleadoNavigation { get; set; }
}
