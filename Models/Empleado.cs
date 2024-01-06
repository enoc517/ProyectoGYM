using System;
using System.Collections.Generic;

namespace ProyectoGYM.Models;

public partial class Empleado
{
    public int Idempleado { get; set; }

    public int Idactivididades { get; set; }

    public int Idpuesto { get; set; }

    public string Nombre { get; set; }

    public string Apellidos { get; set; }

    public string Dirreccion { get; set; }

    public int Telefono { get; set; }

    public int Cedula { get; set; }

    public DateOnly FechaContratacion { get; set; }

    public DateOnly FechaPago { get; set; }

    public string CategoriaProfesional { get; set; }

    public int NumSeguridadSocial { get; set; }

    public int CuentaBancaria { get; set; }

    public int RetencionCcss { get; set; }

    public virtual ICollection<Clase> Clases { get; set; } = new List<Clase>();

    public virtual ICollection<FacturaPedido> FacturaPedidos { get; set; } = new List<FacturaPedido>();

    public virtual ICollection<Jornada> Jornada { get; set; } = new List<Jornada>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<Puesto> Puestos { get; set; } = new List<Puesto>();
}
