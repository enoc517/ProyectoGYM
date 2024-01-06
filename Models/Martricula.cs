using System;
using System.Collections.Generic;

namespace ProyectoGYM.Models;

public partial class Martricula
{
    public int Idmatricula { get; set; }

    public int Idclase { get; set; }

    public int Idcliente { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<Clase> Clases { get; set; } = new List<Clase>();

    public virtual Cliente IdclienteNavigation { get; set; }
}
