﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductsAPI.Models;

#nullable disable

namespace ProductsAPI.Migrations
{
    [DbContext(typeof(ProductsContext))]
    [Migration("20240914143040_InitalCreate")]
    partial class InitalCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("ProductsAPI.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            IsActive = true,
                            Price = 6000m,
                            ProductName = "IPhone 15"
                        },
                        new
                        {
                            ProductId = 2,
                            IsActive = true,
                            Price = 7000m,
                            ProductName = "IPhone 16"
                        },
                        new
                        {
                            ProductId = 3,
                            IsActive = true,
                            Price = 8000m,
                            ProductName = "IPhone 17"
                        },
                        new
                        {
                            ProductId = 4,
                            IsActive = false,
                            Price = 9000m,
                            ProductName = "IPhone 18"
                        },
                        new
                        {
                            ProductId = 5,
                            IsActive = true,
                            Price = 10000m,
                            ProductName = "IPhone 19"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
