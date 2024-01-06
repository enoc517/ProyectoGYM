using System;
using System.Collections.Generic;

namespace ProyectoGYM.Models;

public partial class Actividade
{
    public int Idactividades { get; set; }

    public int Idsala { get; set; }

    public string Descripcion { get; set; }

    public virtual ICollection<Clase> Clases { get; set; } = new List<Clase>();

    public virtual Sala IdsalaNavigation { get; set; }
}
