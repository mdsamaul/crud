﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Product_Crud.Data;

#nullable disable

namespace Product_Crud.Migrations
{
    [DbContext(typeof(ProductDbContext))]
    [Migration("20250108120002_ProductDbInit")]
    partial class ProductDbInit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Product_Crud.Models.Color", b =>
                {
                    b.Property<int>("CId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CId"));

                    b.Property<string>("CName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CId");

                    b.ToTable("colors");
                });

            modelBuilder.Entity("Product_Crud.Models.Details", b =>
                {
                    b.Property<int>("DId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DId"));

                    b.Property<int>("CId")
                        .HasColumnType("int");

                    b.Property<int?>("ColorsCId")
                        .HasColumnType("int");

                    b.Property<int>("PId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductsPId")
                        .HasColumnType("int");

                    b.HasKey("DId");

                    b.HasIndex("ColorsCId");

                    b.HasIndex("ProductsPId");

                    b.ToTable("details");
                });

            modelBuilder.Entity("Product_Crud.Models.Product", b =>
                {
                    b.Property<int>("PId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PId"));

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAviable")
                        .HasColumnType("bit");

                    b.Property<DateOnly>("PDate")
                        .HasColumnType("date");

                    b.Property<string>("PName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("PId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("Product_Crud.Models.Details", b =>
                {
                    b.HasOne("Product_Crud.Models.Color", "Colors")
                        .WithMany("Details")
                        .HasForeignKey("ColorsCId");

                    b.HasOne("Product_Crud.Models.Product", "Products")
                        .WithMany("Details")
                        .HasForeignKey("ProductsPId");

                    b.Navigation("Colors");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("Product_Crud.Models.Color", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("Product_Crud.Models.Product", b =>
                {
                    b.Navigation("Details");
                });
#pragma warning restore 612, 618
        }
    }
}