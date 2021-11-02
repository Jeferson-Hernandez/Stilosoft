﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Stilosoft.Model.DAL;

namespace Stilosoft.Model.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.AbonoCompra", b =>
                {
                    b.Property<int>("AbonoCompraId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CantAbono")
                        .HasColumnType("int");

                    b.Property<int>("CompraId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaPago")
                        .HasColumnType("Date");

                    b.HasKey("AbonoCompraId");

                    b.HasIndex("CompraId");

                    b.ToTable("AbonoCompra");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.Cita", b =>
                {
                    b.Property<int>("CitaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClienteId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("Date");

                    b.Property<string>("Hora")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<long>("Total")
                        .HasColumnType("bigint");

                    b.HasKey("CitaId");

                    b.HasIndex("ClienteId");

                    b.ToTable("Cita");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.Cliente", b =>
                {
                    b.Property<string>("ClienteId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Apellido")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Documento")
                        .HasColumnType("nvarchar(15)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Numero")
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("ClienteId");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.Compra", b =>
                {
                    b.Property<int>("CompraId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cuotas")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaFactura")
                        .HasColumnType("Date");

                    b.Property<DateTime>("FechaInicioPago")
                        .HasColumnType("Date");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("Date");

                    b.Property<string>("FormaPago")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("NoFactura")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Periodicidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("ProveedorId")
                        .HasColumnType("int");

                    b.Property<string>("RutaImagen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompraId");

                    b.HasIndex("ProveedorId");

                    b.ToTable("Compra");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.DetalleCita", b =>
                {
                    b.Property<int>("DetalleCitaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CitaId")
                        .HasColumnType("int");

                    b.Property<int?>("EstilistaId")
                        .HasColumnType("int");

                    b.Property<int>("ServicioId")
                        .HasColumnType("int");

                    b.HasKey("DetalleCitaId");

                    b.HasIndex("CitaId");

                    b.HasIndex("EstilistaId");

                    b.HasIndex("ServicioId");

                    b.ToTable("DetalleCita");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.DetalleCompra", b =>
                {
                    b.Property<int>("DetalleCompraId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CantInsumo")
                        .HasColumnType("int");

                    b.Property<int>("CantProducto")
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("CompraId")
                        .HasColumnType("int");

                    b.Property<long>("Costo")
                        .HasColumnType("bigint");

                    b.Property<int>("InsumoId")
                        .HasColumnType("int");

                    b.Property<int>("Iva")
                        .HasColumnType("int");

                    b.Property<string>("Medida")
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<long>("SubTotal")
                        .HasColumnType("bigint");

                    b.Property<long>("Total")
                        .HasColumnType("bigint");

                    b.HasKey("DetalleCompraId");

                    b.HasIndex("CompraId")
                        .IsUnique();

                    b.HasIndex("ProductoId");

                    b.ToTable("DetalleCompra");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.DetalleServicioProductos", b =>
                {
                    b.Property<int>("DetalleServProductoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<long>("Precio")
                        .HasColumnType("bigint");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<int>("SolicitudServicioId")
                        .HasColumnType("int");

                    b.HasKey("DetalleServProductoId");

                    b.HasIndex("ProductoId");

                    b.HasIndex("SolicitudServicioId");

                    b.ToTable("DetalleServicioProductos");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.DetalleServicioServicios", b =>
                {
                    b.Property<int>("ServicioServiciosId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EstilistaId")
                        .HasColumnType("int");

                    b.Property<long>("Precio")
                        .HasColumnType("bigint");

                    b.Property<int>("ServicioId")
                        .HasColumnType("int");

                    b.Property<int>("SolicitudServicioId")
                        .HasColumnType("int");

                    b.HasKey("ServicioServiciosId");

                    b.HasIndex("EstilistaId");

                    b.HasIndex("ServicioId");

                    b.HasIndex("SolicitudServicioId");

                    b.ToTable("DetalleServicioServicios");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.Estilista", b =>
                {
                    b.Property<int>("EstilistaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("Date");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("EstilistaId");

                    b.ToTable("Estilista");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.Producto", b =>
                {
                    b.Property<int>("ProductoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<long>("Precio")
                        .HasColumnType("bigint");

                    b.Property<string>("RutaImagen")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductoId");

                    b.ToTable("Producto");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.Proveedor", b =>
                {
                    b.Property<int>("ProveedorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Contacto")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nit")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("ProveedorId");

                    b.ToTable("Proveedor");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.Servicio", b =>
                {
                    b.Property<int>("ServicioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Duracion")
                        .HasColumnType("int");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ServicioId");

                    b.ToTable("Servicio");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.SolicitudServicio", b =>
                {
                    b.Property<int>("SolicitudServicioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<string>("ClienteId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("Date");

                    b.Property<string>("Hora")
                        .HasColumnType("nvarchar(20)");

                    b.Property<long>("Total")
                        .HasColumnType("bigint");

                    b.HasKey("SolicitudServicioId");

                    b.HasIndex("ClienteId1");

                    b.ToTable("SolicitudServicio");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.Usuario", b =>
                {
                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Documento")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.AplicationRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("AplicationRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.AbonoCompra", b =>
                {
                    b.HasOne("Stilosoft.Model.Entities.Compra", "Compra")
                        .WithMany()
                        .HasForeignKey("CompraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compra");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.Cita", b =>
                {
                    b.HasOne("Stilosoft.Model.Entities.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId");

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.Cliente", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "IdentityUser")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdentityUser");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.Compra", b =>
                {
                    b.HasOne("Stilosoft.Model.Entities.Proveedor", "Proveedor")
                        .WithMany()
                        .HasForeignKey("ProveedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Proveedor");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.DetalleCita", b =>
                {
                    b.HasOne("Stilosoft.Model.Entities.Cita", "Cita")
                        .WithMany("DetalleCitas")
                        .HasForeignKey("CitaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Stilosoft.Model.Entities.Estilista", "Estilista")
                        .WithMany("DetalleCitas")
                        .HasForeignKey("EstilistaId");

                    b.HasOne("Stilosoft.Model.Entities.Servicio", "Servicio")
                        .WithMany("DetalleCitas")
                        .HasForeignKey("ServicioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cita");

                    b.Navigation("Estilista");

                    b.Navigation("Servicio");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.DetalleCompra", b =>
                {
                    b.HasOne("Stilosoft.Model.Entities.Compra", "Compra")
                        .WithOne("DetalleCompras")
                        .HasForeignKey("Stilosoft.Model.Entities.DetalleCompra", "CompraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Stilosoft.Model.Entities.Producto", "Producto")
                        .WithMany("DetalleCompras")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compra");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.DetalleServicioProductos", b =>
                {
                    b.HasOne("Stilosoft.Model.Entities.Producto", "Producto")
                        .WithMany("DetalleServicioProductos")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Stilosoft.Model.Entities.SolicitudServicio", "SolicitudServicio")
                        .WithMany("DetalleServicioProductos")
                        .HasForeignKey("SolicitudServicioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("SolicitudServicio");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.DetalleServicioServicios", b =>
                {
                    b.HasOne("Stilosoft.Model.Entities.Estilista", "Estilista")
                        .WithMany("DetalleServicioServicios")
                        .HasForeignKey("EstilistaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Stilosoft.Model.Entities.Servicio", "Servicio")
                        .WithMany("DetalleServicioServicios")
                        .HasForeignKey("ServicioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Stilosoft.Model.Entities.SolicitudServicio", "SolicitudServicio")
                        .WithMany("DetalleServicioServicios")
                        .HasForeignKey("SolicitudServicioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estilista");

                    b.Navigation("Servicio");

                    b.Navigation("SolicitudServicio");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.SolicitudServicio", b =>
                {
                    b.HasOne("Stilosoft.Model.Entities.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId1");

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.Usuario", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "IdentityUser")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdentityUser");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.Cita", b =>
                {
                    b.Navigation("DetalleCitas");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.Compra", b =>
                {
                    b.Navigation("DetalleCompras");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.Estilista", b =>
                {
                    b.Navigation("DetalleCitas");

                    b.Navigation("DetalleServicioServicios");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.Producto", b =>
                {
                    b.Navigation("DetalleCompras");

                    b.Navigation("DetalleServicioProductos");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.Servicio", b =>
                {
                    b.Navigation("DetalleCitas");

                    b.Navigation("DetalleServicioServicios");
                });

            modelBuilder.Entity("Stilosoft.Model.Entities.SolicitudServicio", b =>
                {
                    b.Navigation("DetalleServicioProductos");

                    b.Navigation("DetalleServicioServicios");
                });
#pragma warning restore 612, 618
        }
    }
}
