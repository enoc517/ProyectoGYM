using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoGYM.Models;

namespace ProyectoGYM.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public  DbSet<Actividade> Actividades { get; set; }

        public  DbSet<CategoriaMembresium> CategoriaMembresia { get; set; }

        public  DbSet<CategoriaProducto> CategoriaProductos { get; set; }

        public   DbSet<Clase> Clases { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<ClienteMembresium> ClienteMembresia { get; set; }

        public DbSet<DetalleFactura> DetalleFacturas { get; set; }

        public DbSet<Empleado> Empleados { get; set; }

        public DbSet<FacturaCliente> FacturaClientes { get; set; }

        public DbSet<FacturaPedido> FacturaPedidos { get; set; }

        public DbSet<GeneracionPago> GeneracionPagos { get; set; }

        public DbSet<HistortialPago> HistortialPagos { get; set; }

        public DbSet<Jornada> Jornadas { get; set; }

        public DbSet<Martricula> Martriculas { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<ProductosVentum> ProductosVenta { get; set; }

        public DbSet<Proveedore> Proveedores { get; set; }

        public DbSet<Puesto> Puestos { get; set; }

        public DbSet<Sala> Salas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Actividade>(entity =>
            {
                entity.HasKey(e => e.Idactividades);

                entity.Property(e => e.Idactividades).HasColumnName("IDActividades");
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Idsala).HasColumnName("IDSala");

                entity.HasOne(d => d.IdsalaNavigation).WithMany(p => p.Actividades)
                    .HasForeignKey(d => d.Idsala)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Actividades_Sala");
            });

            modelBuilder.Entity<CategoriaMembresium>(entity =>
            {
                entity.HasKey(e => e.IdcategoriaMembresia);

                entity.Property(e => e.IdcategoriaMembresia).HasColumnName("IDCategoriaMembresia");
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<CategoriaProducto>(entity =>
            {
                entity.HasKey(e => e.IdcategoriaProducto);

                entity.ToTable("CategoriaProducto");

                entity.Property(e => e.IdcategoriaProducto).HasColumnName("IDCategoriaProducto");
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Clase>(entity =>
            {
                entity.HasKey(e => e.Idclase);

                entity.Property(e => e.Idclase).HasColumnName("IDClase");
                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsFixedLength();
                entity.Property(e => e.Idactividad).HasColumnName("IDActividad");
                entity.Property(e => e.Idempleado).HasColumnName("IDEmpleado");
                entity.Property(e => e.Idmatricula).HasColumnName("IDMatricula");

                entity.HasOne(d => d.IdactividadNavigation).WithMany(p => p.Clases)
                    .HasForeignKey(d => d.Idactividad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Clases_Actividades");

                entity.HasOne(d => d.IdempleadoNavigation).WithMany(p => p.Clases)
                    .HasForeignKey(d => d.Idempleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Clases_Empleados");

                entity.HasOne(d => d.IdmatriculaNavigation).WithMany(p => p.Clases)
                    .HasForeignKey(d => d.Idmatricula)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Clases_Martricula");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Idcliente);

                entity.ToTable("Cliente");

                entity.Property(e => e.Idcliente).HasColumnName("IDCliente");
                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(75);
                entity.Property(e => e.Dirrecion)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.EstadoCliente)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.IdclienteMembresia).HasColumnName("IDClienteMembresia");
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.SesionesRayosUva)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SesionesRayosUVA");

                entity.HasOne(d => d.IdclienteMembresiaNavigation).WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.IdclienteMembresia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cliente_ClienteMembresia");
            });

            modelBuilder.Entity<ClienteMembresium>(entity =>
            {
                entity.HasKey(e => e.IdclienteMembresia);

                entity.Property(e => e.IdclienteMembresia).HasColumnName("IDClienteMembresia");
                entity.Property(e => e.IdcategoraMembresia).HasColumnName("IDCategoraMembresia");

                entity.HasOne(d => d.IdcategoraMembresiaNavigation).WithMany(p => p.ClienteMembresia)
                    .HasForeignKey(d => d.IdcategoraMembresia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClienteMembresia_CategoriaMembresia");
            });

            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.HasKey(e => e.IdDetalleFactura);

                entity.ToTable("DetalleFactura");

                entity.Property(e => e.IdDetalleFactura).HasColumnName("ID_DetalleFactura");
                entity.Property(e => e.Descuento).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.PrecioUnidad).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.Idempleado);

                entity.Property(e => e.Idempleado).HasColumnName("IDEmpleado");
                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(75);
                entity.Property(e => e.CategoriaProfesional)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Dirreccion)
                    .IsRequired()
                    .HasMaxLength(80);
                entity.Property(e => e.Idactivididades).HasColumnName("IDActivididades");
                entity.Property(e => e.Idpuesto).HasColumnName("IDPuesto");
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.RetencionCcss).HasColumnName("RetencionCCSS");
            });

            modelBuilder.Entity<FacturaCliente>(entity =>
            {
                entity.HasKey(e => e.Idfactura);

                entity.ToTable("FacturaCliente");

                entity.Property(e => e.Idfactura).HasColumnName("IDFactura");
                entity.Property(e => e.IdclienteMembresia).HasColumnName("IDClienteMembresia");
                entity.Property(e => e.MetodoPago)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdclienteMembresiaNavigation).WithMany(p => p.FacturaClientes)
                    .HasForeignKey(d => d.IdclienteMembresia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FacturaCliente_ClienteMembresia");
            });

            modelBuilder.Entity<FacturaPedido>(entity =>
            {
                entity.HasKey(e => e.IdfacturaPedidos);

                entity.Property(e => e.IdfacturaPedidos).HasColumnName("IDFacturaPedidos");
                entity.Property(e => e.IdDetalleFactura).HasColumnName("ID_DetalleFactura");
                entity.Property(e => e.Idempleado).HasColumnName("IDEmpleado");

                entity.HasOne(d => d.IdDetalleFacturaNavigation).WithMany(p => p.FacturaPedidos)
                    .HasForeignKey(d => d.IdDetalleFactura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FacturaPedidos_DetalleFactura");

                entity.HasOne(d => d.IdempleadoNavigation).WithMany(p => p.FacturaPedidos)
                    .HasForeignKey(d => d.Idempleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FacturaPedidos_Empleados");
            });

            modelBuilder.Entity<GeneracionPago>(entity =>
            {
                entity.HasKey(e => e.IdgeneracionPago);

                entity.Property(e => e.IdgeneracionPago).HasColumnName("IDGeneracionPago");
                entity.Property(e => e.DescripcionPago)
                    .IsRequired()
                    .HasMaxLength(250);
                entity.Property(e => e.EstadoDelPago)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Idjornadas).HasColumnName("IDJornadas");
                entity.Property(e => e.TipoPago)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdjornadasNavigation).WithMany(p => p.GeneracionPagos)
                    .HasForeignKey(d => d.Idjornadas)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GeneracionPagos_Jornadas");
            });

            modelBuilder.Entity<HistortialPago>(entity =>
            {
                entity.HasKey(e => e.IdhistorialPago);

                entity.Property(e => e.IdhistorialPago).HasColumnName("IDHistorialPago");
                entity.Property(e => e.IdgeneracionPagos).HasColumnName("IDGeneracionPagos");
                entity.Property(e => e.MontoTotal).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdgeneracionPagosNavigation).WithMany(p => p.HistortialPagos)
                    .HasForeignKey(d => d.IdgeneracionPagos)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HistortialPagos_GeneracionPagos");
            });

            modelBuilder.Entity<Jornada>(entity =>
            {
                entity.HasKey(e => e.Idjornadas);

                entity.Property(e => e.Idjornadas).HasColumnName("IDJornadas");
                entity.Property(e => e.HorasTrabajadas).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Idempleado).HasColumnName("IDEmpleado");

                entity.HasOne(d => d.IdempleadoNavigation).WithMany(p => p.Jornada)
                    .HasForeignKey(d => d.Idempleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Jornadas_Empleados");
            });

            modelBuilder.Entity<Martricula>(entity =>
            {
                entity.HasKey(e => e.Idmatricula);

                entity.ToTable("Martricula");

                entity.Property(e => e.Idmatricula).HasColumnName("IDMatricula");
                entity.Property(e => e.Idclase).HasColumnName("IDClase");
                entity.Property(e => e.Idcliente).HasColumnName("IDCliente");

                entity.HasOne(d => d.IdclienteNavigation).WithMany(p => p.Martriculas)
                    .HasForeignKey(d => d.Idcliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Martricula_Cliente");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.Idpedido);

                entity.Property(e => e.Idpedido).HasColumnName("IDPedido");
                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(10);
                entity.Property(e => e.Idempleado).HasColumnName("IDEmpleado");
                entity.Property(e => e.Idprovedores).HasColumnName("IDProvedores");

                entity.HasOne(d => d.IdempleadoNavigation).WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.Idempleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pedidos_Empleados");

                entity.HasOne(d => d.IdprovedoresNavigation).WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.Idprovedores)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pedidos_Proveedores");
            });

            modelBuilder.Entity<ProductosVentum>(entity =>
            {
                entity.HasKey(e => e.Idproductos);

                entity.Property(e => e.Idproductos).HasColumnName("IDProductos");
                entity.Property(e => e.IdcategoriaProducto).HasColumnName("IDCategoriaProducto");
                entity.Property(e => e.Idprovedores).HasColumnName("IDProvedores");
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdcategoriaProductoNavigation).WithMany(p => p.ProductosVenta)
                    .HasForeignKey(d => d.IdcategoriaProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductosVenta_CategoriaProducto");

                entity.HasOne(d => d.IdprovedoresNavigation).WithMany(p => p.ProductosVenta)
                    .HasForeignKey(d => d.Idprovedores)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductosVenta_Proveedores");
            });

            modelBuilder.Entity<Proveedore>(entity =>
            {
                entity.HasKey(e => e.Idprovedores);

                entity.Property(e => e.Idprovedores).HasColumnName("IDProvedores");
                entity.Property(e => e.CorreoElectronico)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Puesto>(entity =>
            {
                entity.HasKey(e => e.Idpuesto);

                entity.ToTable("Puesto");

                entity.Property(e => e.Idpuesto).HasColumnName("IDPuesto");
                entity.Property(e => e.CategoriaPuesto)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Idempleado).HasColumnName("IDEmpleado");
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.PagoHora).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdempleadoNavigation).WithMany(p => p.Puestos)
                    .HasForeignKey(d => d.Idempleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Puesto_Empleados");
            });

            modelBuilder.Entity<Sala>(entity =>
            {
                entity.HasKey(e => e.Idsala);

                entity.ToTable("Sala");

                entity.Property(e => e.Idsala).HasColumnName("IDSala");
            });

          
        }
    }
}
