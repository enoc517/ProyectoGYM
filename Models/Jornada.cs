using System;
using System.Collections.Generic;

namespace ProyectoGYM.Models;

public partial class Jornada
{
    public int Idjornadas { get; set; }

    public int Idempleado { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public decimal HorasTrabajadas { get; set; }

    public virtual ICollection<GeneracionPago> GeneracionPagos { get; set; } = new List<GeneracionPago>();

    public virtual Empleado IdempleadoNavigation { get; set; }
}
