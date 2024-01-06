using System;
using System.Collections.Generic;

namespace ProyectoGYM.Models;

public partial class ClienteMembresium
{
    public int IdclienteMembresia { get; set; }

    public int IdcategoraMembresia { get; set; }

    public DateOnly FechaProxRenovacion { get; set; }

    public DateOnly FechaInicio { get; set; }

    public DateOnly FechaFin { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<FacturaCliente> FacturaClientes { get; set; } = new List<FacturaCliente>();

    public virtual CategoriaMembresium IdcategoraMembresiaNavigation { get; set; }
}
