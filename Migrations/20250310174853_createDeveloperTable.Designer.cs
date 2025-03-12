﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyApp.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyApp.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20250310174853_createDeveloperTable")]
    partial class createDeveloperTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MyApp.Model.Developer", b =>
                {
                    b.Property<int>("DeveloperId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("developer_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("DeveloperId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<int?>("Gender")
                        .HasColumnType("integer")
                        .HasColumnName("jenis_kelamin");

                    b.Property<string>("Nama")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("nama");

                    b.Property<string>("Phone")
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)")
                        .HasColumnName("phone");

                    b.Property<string>("Role")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("role");

                    b.Property<string>("Status")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("status");

                    b.Property<DateTime?>("TanggalLahir")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("tanggal_lahir");

                    b.HasKey("DeveloperId");

                    b.ToTable("developers", "public");
                });

            modelBuilder.Entity("MyApp.Model.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("project_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProjectId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime>("End_Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_date");

                    b.Property<string>("Nama")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("nama");

                    b.Property<DateTime>("Start_Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_date");

                    b.Property<string>("Status")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("status");

                    b.HasKey("ProjectId");

                    b.ToTable("project", "public");
                });

            modelBuilder.Entity("MyApp.Model.ProjectDeveloper", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("integer")
                        .HasColumnName("project_id")
                        .HasColumnOrder(0);

                    b.Property<int>("DeveloperId")
                        .HasColumnType("integer")
                        .HasColumnName("developer_id")
                        .HasColumnOrder(1);

                    b.HasKey("ProjectId", "DeveloperId");

                    b.HasIndex("DeveloperId");

                    b.ToTable("project_developer", "public");
                });

            modelBuilder.Entity("MyEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("MyEntities", "public");
                });

            modelBuilder.Entity("TaskItem", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("task_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TaskId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<int?>("DeveloperId")
                        .HasColumnType("integer")
                        .HasColumnName("developer_id");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("end_date");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("priority");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer")
                        .HasColumnName("project_id");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("start_date");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("TaskId");

                    b.HasIndex("DeveloperId");

                    b.HasIndex("ProjectId");

                    b.ToTable("task", "public");
                });

            modelBuilder.Entity("MyApp.Model.ProjectDeveloper", b =>
                {
                    b.HasOne("MyApp.Model.Developer", "Developers")
                        .WithMany("ProjectDevelopers")
                        .HasForeignKey("DeveloperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyApp.Model.Project", "Project")
                        .WithMany("ProjectDeveloper")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Developers");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TaskItem", b =>
                {
                    b.HasOne("MyApp.Model.Developer", "Developers")
                        .WithMany("TaskItem")
                        .HasForeignKey("DeveloperId");

                    b.HasOne("MyApp.Model.Project", "Project")
                        .WithMany("TaskItem")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Developers");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("MyApp.Model.Developer", b =>
                {
                    b.Navigation("ProjectDevelopers");

                    b.Navigation("TaskItem");
                });

            modelBuilder.Entity("MyApp.Model.Project", b =>
                {
                    b.Navigation("ProjectDeveloper");

                    b.Navigation("TaskItem");
                });
#pragma warning restore 612, 618
        }
    }
}
