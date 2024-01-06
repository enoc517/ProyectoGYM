using System;
using System.Collections.Generic;

namespace ProyectoGYM.Models;

public partial class Puesto
{
    public int Idpuesto { get; set; }

    public int Idempleado { get; set; }

    public decimal PagoHora { get; set; }

    public string Nombre { get; set; }

    public string CategoriaPuesto { get; set; }

    public virtual Empleado IdempleadoNavigation { get; set; }
}
