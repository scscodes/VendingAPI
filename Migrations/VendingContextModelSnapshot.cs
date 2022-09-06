﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VendingAPI.Data;

#nullable disable

namespace VendingAPI.Migrations
{
    [DbContext(typeof(VendingContext))]
    partial class VendingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("VendingAPI.Models.Machine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("MachineInventoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MachineInventoryId");

                    b.ToTable("Machine");
                });

            modelBuilder.Entity("VendingAPI.Models.MachineInventory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("MachineInventory");
                });

            modelBuilder.Entity("VendingAPI.Models.MachineInventoryLineItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrentQuantity")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("MachineInventoryId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("ProductId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MachineInventoryId");

                    b.HasIndex("ProductId");

                    b.ToTable("MachineInventoryLineItem");
                });

            modelBuilder.Entity("VendingAPI.Models.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("VendingAPI.Models.Purchase", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("AmountTendered")
                        .HasColumnType("TEXT");

                    b.Property<long>("TransactionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.ToTable("Purchase");
                });

            modelBuilder.Entity("VendingAPI.Models.Transaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("MachineId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("VendingAPI.Models.TransactionLineItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TransactionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.ToTable("TransactionLineItem");
                });

            modelBuilder.Entity("VendingAPI.Models.Machine", b =>
                {
                    b.HasOne("VendingAPI.Models.MachineInventory", "MachineInventory")
                        .WithMany()
                        .HasForeignKey("MachineInventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MachineInventory");
                });

            modelBuilder.Entity("VendingAPI.Models.MachineInventoryLineItem", b =>
                {
                    b.HasOne("VendingAPI.Models.MachineInventory", null)
                        .WithMany("MachineInventoryLineItem")
                        .HasForeignKey("MachineInventoryId");

                    b.HasOne("VendingAPI.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("VendingAPI.Models.Purchase", b =>
                {
                    b.HasOne("VendingAPI.Models.Transaction", "Transaction")
                        .WithMany()
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("VendingAPI.Models.TransactionLineItem", b =>
                {
                    b.HasOne("VendingAPI.Models.Transaction", null)
                        .WithMany("TransactionLineItem")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VendingAPI.Models.MachineInventory", b =>
                {
                    b.Navigation("MachineInventoryLineItem");
                });

            modelBuilder.Entity("VendingAPI.Models.Transaction", b =>
                {
                    b.Navigation("TransactionLineItem");
                });
#pragma warning restore 612, 618
        }
    }
}
