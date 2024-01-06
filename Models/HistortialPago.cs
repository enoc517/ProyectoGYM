using System;
using System.Collections.Generic;

namespace ProyectoGYM.Models;

public partial class HistortialPago
{
    public int IdhistorialPago { get; set; }

    public int IdgeneracionPagos { get; set; }

    public DateOnly FechaPago { get; set; }

    public decimal MontoTotal { get; set; }

    public virtual GeneracionPago IdgeneracionPagosNavigation { get; set; }
}
