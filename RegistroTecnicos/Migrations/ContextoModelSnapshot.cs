﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RegistroTecnicos.DAL;

#nullable disable

namespace RegistroTecnicos.Migrations
{
    [DbContext(typeof(Contexto))]
    partial class ContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("RegistroTecnicos.Models.Tecnicos", b =>
                {
                    b.Property<int>("TecnicosId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("TecnicosName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("TecnicosId");

                    b.ToTable("Tecnicos");
                });
#pragma warning restore 612, 618
        }
    }
}
