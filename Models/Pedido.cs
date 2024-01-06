using System;
using System.Collections.Generic;

namespace ProyectoGYM.Models;

public partial class Pedido
{
    public int Idpedido { get; set; }

    public int Idempleado { get; set; }

    public int Idprovedores { get; set; }

    public string Estado { get; set; }

    public DateOnly FechaCompra { get; set; }

    public DateOnly FechaRecibido { get; set; }

    public int Cantidad { get; set; }

    public virtual Empleado IdempleadoNavigation { get; set; }

    public virtual Proveedore IdprovedoresNavigation { get; set; }
}
