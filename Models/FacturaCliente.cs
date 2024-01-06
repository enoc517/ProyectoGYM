using System;
using System.Collections.Generic;

namespace ProyectoGYM.Models;

public partial class FacturaCliente
{
    public int Idfactura { get; set; }

    public int IdclienteMembresia { get; set; }

    public DateOnly FechaEmicion { get; set; }

    public string MetodoPago { get; set; }

    public virtual ClienteMembresium IdclienteMembresiaNavigation { get; set; }
}
