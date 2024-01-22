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
    [Migration("20240105173243_setimamigração")]
    partial class setimamigração
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

                    b.Property<DateTime>("HoraDoEvento")
                        .HasColumnType("datetime(6)");

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
#pragma warning restore 612, 618
        }
    }
}
