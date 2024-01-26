﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PROIV_PROYECTO.Contexts;

#nullable disable

namespace PROIV_PROYECTO.Migrations.Proyecto
{
    [DbContext(typeof(ProyectoContext))]
    partial class ProyectoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PROIV_PROYECTO.Models.Estado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("NombreEstado")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Estados");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            NombreEstado = "Completado"
                        },
                        new
                        {
                            Id = 2,
                            NombreEstado = "Cancelado"
                        },
                        new
                        {
                            Id = 3,
                            NombreEstado = "Retrasado"
                        },
                        new
                        {
                            Id = 4,
                            NombreEstado = "En Proceso"
                        });
                });

            modelBuilder.Entity("PROIV_PROYECTO.Models.Proyecto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EstadoId")
                        .HasColumnType("int");

                    b.Property<string>("FechaInicio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.ToTable("Proyectos");
                });

            modelBuilder.Entity("PROIV_PROYECTO.Models.Tarea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EstadoId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProyectoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EstadoId");

                    b.HasIndex("ProyectoId");

                    b.ToTable("Tareas");
                });

            modelBuilder.Entity("PROIV_PROYECTO.Models.TareaUsuario", b =>
                {
                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TareaId")
                        .HasColumnType("int");

                    b.HasKey("UsuarioId", "TareaId");

                    b.HasIndex("TareaId");

                    b.ToTable("TareasUsuarios");
                });

            modelBuilder.Entity("PROIV_PROYECTO.Models.Proyecto", b =>
                {
                    b.HasOne("PROIV_PROYECTO.Models.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Estado");
                });

            modelBuilder.Entity("PROIV_PROYECTO.Models.Tarea", b =>
                {
                    b.HasOne("PROIV_PROYECTO.Models.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("EstadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PROIV_PROYECTO.Models.Proyecto", "Proyecto")
                        .WithMany("Tareas")
                        .HasForeignKey("ProyectoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Estado");

                    b.Navigation("Proyecto");
                });

            modelBuilder.Entity("PROIV_PROYECTO.Models.TareaUsuario", b =>
                {
                    b.HasOne("PROIV_PROYECTO.Models.Tarea", "Tarea")
                        .WithMany("TareasUsuarios")
                        .HasForeignKey("TareaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PROIV_PROYECTO.Models.Usuario", "Usuario")
                        .WithMany("TareasUsuarios")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tarea");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("PROIV_PROYECTO.Models.Proyecto", b =>
                {
                    b.Navigation("Tareas");
                });

            modelBuilder.Entity("PROIV_PROYECTO.Models.Tarea", b =>
                {
                    b.Navigation("TareasUsuarios");
                });

            modelBuilder.Entity("PROIV_PROYECTO.Models.Usuario", b =>
                {
                    b.Navigation("TareasUsuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
