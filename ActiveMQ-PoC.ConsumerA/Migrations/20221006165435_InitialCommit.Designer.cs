﻿// <auto-generated />
using ActiveMQ_PoC.ConsumerA.Data.Context;
using ActiveMQ_PoC.ConsumerA.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ActiveMQ_PoC.ConsumerA.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221006165435_InitialCommit")]
    partial class InitialCommit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ActiveMQ_PoC.ConsumerA.Data.Entities.TransportOrderDoc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<TransportOrderAmendedEvent>("OriginalEvent")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<TransportOrder>("TransportOrder")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.HasKey("Id");

                    b.HasIndex("TransportOrder");

                    NpgsqlIndexBuilderExtensions.HasMethod(b.HasIndex("TransportOrder"), "gin");

                    b.ToTable("TransportOrderDocs");
                });
#pragma warning restore 612, 618
        }
    }
}
