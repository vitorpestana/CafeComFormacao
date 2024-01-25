﻿// <auto-generated />
using System;
using CafeComFormacao.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CafeComFormacao.Migrations
{
    [DbContext(typeof(CafeComFormacaoContext))]
    [Migration("20240108134750_nonaMigracao")]
    partial class nonaMigracao
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CafeComFormacao.Models.Evento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataDoEvento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("HoraDoEvento")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NomeDoEvento")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("ValorDoEvento")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("Evento");
                });

            modelBuilder.Entity("CafeComFormacao.Models.Participante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Admin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<bool>("CursoLidere")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("StatusPagamento")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Participante");
                });

            modelBuilder.Entity("CafeComFormacao.Models.UsuarioEvento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("EventoId")
                        .HasColumnType("int");

                    b.Property<int>("ParticipanteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventoId");

                    b.HasIndex("ParticipanteId");

                    b.ToTable("UsuarioEvento");
                });

            modelBuilder.Entity("CafeComFormacao.Models.UsuarioEvento", b =>
                {
                    b.HasOne("CafeComFormacao.Models.Evento", "Evento")
                        .WithMany()
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CafeComFormacao.Models.Participante", "Participante")
                        .WithMany()
                        .HasForeignKey("ParticipanteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evento");

                    b.Navigation("Participante");
                });
#pragma warning restore 612, 618
        }
    }
}