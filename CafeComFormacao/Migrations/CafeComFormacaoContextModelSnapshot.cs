﻿// <auto-generated />
using System;
using CafeComFormacao.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CafeComFormacao.Migrations
{
    [DbContext(typeof(CafeComFormacaoContext))]
    partial class CafeComFormacaoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CafeComFormacao.Models.CredenciaisAdm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("LoginEmail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("CredenciaisAdm");
                });

            modelBuilder.Entity("CafeComFormacao.Models.CredenciaisParticipante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("LoginEmail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("CredenciaisParticipante");
                });

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

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("CursoLidere")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

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
