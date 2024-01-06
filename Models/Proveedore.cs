using System;
using System.Collections.Generic;

namespace ProyectoGYM.Models;

public partial class Proveedore
{
    public int Idprovedores { get; set; }

    public string Nombre { get; set; }

    public string Telefono { get; set; }

    public string CorreoElectronico { get; set; }

    public string Estado { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ICollection<ProductosVentum> ProductosVenta { get; set; } = new List<ProductosVentum>();
}
