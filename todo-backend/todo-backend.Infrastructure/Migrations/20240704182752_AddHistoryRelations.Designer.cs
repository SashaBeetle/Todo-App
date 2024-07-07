﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using todo_backend.Infrastructure;

#nullable disable

namespace todo_backend.Infrastructure.Migrations
{
    [DbContext(typeof(TodoDbContext))]
    [Migration("20240704182752_AddHistoryRelations")]
    partial class AddHistoryRelations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("todo_backend.Domain.Models.Board", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("todo_backend.Domain.Models.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CatalogId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CatalogId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("todo_backend.Domain.Models.Catalog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BoardId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.ToTable("Catalogs");
                });

            modelBuilder.Entity("todo_backend.Domain.Models.HistoryItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("BoardId")
                        .HasColumnType("integer");

                    b.Property<int?>("CardId")
                        .HasColumnType("integer");

                    b.Property<string>("EventDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Timesetup")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.HasIndex("CardId");

                    b.ToTable("HistoryItems");
                });

            modelBuilder.Entity("todo_backend.Domain.Models.Card", b =>
                {
                    b.HasOne("todo_backend.Domain.Models.Catalog", "Catalog")
                        .WithMany("Cards")
                        .HasForeignKey("CatalogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Catalog");
                });

            modelBuilder.Entity("todo_backend.Domain.Models.Catalog", b =>
                {
                    b.HasOne("todo_backend.Domain.Models.Board", "Board")
                        .WithMany("Catalogs")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");
                });

            modelBuilder.Entity("todo_backend.Domain.Models.HistoryItem", b =>
                {
                    b.HasOne("todo_backend.Domain.Models.Board", "Board")
                        .WithMany("HistoryItems")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("todo_backend.Domain.Models.Card", "Card")
                        .WithMany("HistoryItems")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Board");

                    b.Navigation("Card");
                });

            modelBuilder.Entity("todo_backend.Domain.Models.Board", b =>
                {
                    b.Navigation("Catalogs");

                    b.Navigation("HistoryItems");
                });

            modelBuilder.Entity("todo_backend.Domain.Models.Card", b =>
                {
                    b.Navigation("HistoryItems");
                });

            modelBuilder.Entity("todo_backend.Domain.Models.Catalog", b =>
                {
                    b.Navigation("Cards");
                });
#pragma warning restore 612, 618
        }
    }
}
