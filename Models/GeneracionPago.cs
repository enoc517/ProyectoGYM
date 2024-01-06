using System;
using System.Collections.Generic;

namespace ProyectoGYM.Models;

public partial class GeneracionPago
{
    public int IdgeneracionPago { get; set; }

    public int Idjornadas { get; set; }

    public string DescripcionPago { get; set; }

    public string TipoPago { get; set; }

    public double SalarioBruto { get; set; }

    public string EstadoDelPago { get; set; }

    public int Feriados { get; set; }

    public int HorasExtras { get; set; }

    public virtual ICollection<HistortialPago> HistortialPagos { get; set; } = new List<HistortialPago>();

    public virtual Jornada IdjornadasNavigation { get; set; }
}
