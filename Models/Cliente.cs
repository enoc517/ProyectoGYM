using System;
using System.Collections.Generic;

namespace ProyectoGYM.Models;

public partial class Cliente
{
    public int Idcliente { get; set; }

    public int IdclienteMembresia { get; set; }

    public string Nombre { get; set; }

    public string Apellidos { get; set; }

    public string Dirrecion { get; set; }

    public DateOnly FechaNacimento { get; set; }

    public int Telefono { get; set; }

    public int Cedula { get; set; }

    public string EstadoCliente { get; set; }

    public string SesionesRayosUva { get; set; }

    public virtual ClienteMembresium IdclienteMembresiaNavigation { get; set; }

    public virtual ICollection<Martricula> Martriculas { get; set; } = new List<Martricula>();
}
