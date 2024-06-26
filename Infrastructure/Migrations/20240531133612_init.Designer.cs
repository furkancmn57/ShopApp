﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Domain.Models;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ShopAppDbContext))]
    [Migration("20240531133612_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.AddressAggregate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasColumnName("address");

                    b.Property<string>("AddressTitle")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("address_title");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("date")
                        .HasColumnName("created_date");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("date")
                        .HasColumnName("deleted_date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("boolean");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("date")
                        .HasColumnName("updated_date");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("address", (string)null);
                });

            modelBuilder.Entity("Domain.Models.OrderAggregate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("date")
                        .HasColumnName("created_date");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasColumnName("customer_name");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("date")
                        .HasColumnName("deleted_date");

                    b.Property<double>("DiscountAmount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("discount_amount");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("boolean");

                    b.Property<Guid>("OrderNumber")
                        .HasColumnType("uuid")
                        .HasColumnName("order_number");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasColumnName("status");

                    b.Property<double>("TotalAmount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("total_amount");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("date")
                        .HasColumnName("updated_date");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("UserId");

                    b.ToTable("order", (string)null);
                });

            modelBuilder.Entity("Domain.Models.ProductAggregate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("int");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("date")
                        .HasColumnName("created_date");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("date")
                        .HasColumnName("deleted_date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasColumnName("description");

                    b.Property<List<Ingredients>>("Ingredients")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("ingredients");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasColumnName("product_name");

                    b.Property<double>("Price")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("price");

                    b.Property<double>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("double precision")
                        .HasDefaultValue(0.0)
                        .HasColumnName("quantity");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("date")
                        .HasColumnName("updated_date");

                    b.HasKey("Id");

                    b.ToTable("product", (string)null);
                });

            modelBuilder.Entity("Domain.Models.UserAggregate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("date")
                        .HasColumnName("created_date");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("date")
                        .HasColumnName("deleted_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasColumnName("first_name");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasColumnName("password");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("date")
                        .HasColumnName("updated_date");

                    b.HasKey("Id");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("OrderAggregateProductAggregate", b =>
                {
                    b.Property<int>("OrdersId")
                        .HasColumnType("int");

                    b.Property<int>("ProductsId")
                        .HasColumnType("integer");

                    b.HasKey("OrdersId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("OrderAggregateProductAggregate");
                });

            modelBuilder.Entity("Domain.Models.AddressAggregate", b =>
                {
                    b.HasOne("Domain.Models.UserAggregate", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.OrderAggregate", b =>
                {
                    b.HasOne("Domain.Models.AddressAggregate", "Address")
                        .WithMany("Orders")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.UserAggregate", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OrderAggregateProductAggregate", b =>
                {
                    b.HasOne("Domain.Models.OrderAggregate", null)
                        .WithMany()
                        .HasForeignKey("OrdersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.ProductAggregate", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.AddressAggregate", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Domain.Models.UserAggregate", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
