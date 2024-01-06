using System;
using System.Collections.Generic;

namespace ProyectoGYM.Models;

public partial class Sala
{
    public int Idsala { get; set; }

    public DateOnly Dia { get; set; }

    public TimeOnly HoraInicio { get; set; }

    public TimeOnly HoraFinal { get; set; }

    public int Cupo { get; set; }

    public virtual ICollection<Actividade> Actividades { get; set; } = new List<Actividade>();
}
