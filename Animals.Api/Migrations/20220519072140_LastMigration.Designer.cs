﻿// <auto-generated />
using Animals.EF.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Animals.Api.Migrations
{
    [DbContext(typeof(AnimalDbContext))]
    [Migration("20220519072140_LastMigration")]
    partial class LastMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Animals.Core.Models.Animals.Animal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SpecieId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpecieId");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("Animals.Core.Models.Animals.Specie", b =>
                {
                    b.Property<int>("SpecieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SpecieId"), 1L, 1);

                    b.Property<string>("SpecieName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SpecieId");

                    b.ToTable("Specie");
                });

            modelBuilder.Entity("Animals.Core.Models.Animals.Animal", b =>
                {
                    b.HasOne("Animals.Core.Models.Animals.Specie", "Specie")
                        .WithMany("Animals")
                        .HasForeignKey("SpecieId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Specie");
                });

            modelBuilder.Entity("Animals.Core.Models.Animals.Specie", b =>
                {
                    b.Navigation("Animals");
                });
#pragma warning restore 612, 618
        }
    }
}
