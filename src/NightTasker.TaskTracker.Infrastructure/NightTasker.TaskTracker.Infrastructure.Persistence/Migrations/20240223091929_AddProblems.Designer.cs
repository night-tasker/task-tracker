﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NightTasker.TaskTracker.Infrastructure.Persistence;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NightTasker.TaskTracker.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240223091929_AddProblems")]
    partial class AddProblems
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("NightTasker.TaskTracker.Core.Domain.Entities.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreatedDateTimeOffset")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date_time_offset");

                    b.Property<DateTimeOffset?>("UpdatedDateTimeOffset")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date_time_offset");

                    b.HasKey("Id")
                        .HasName("pk_organizations");

                    b.ToTable("organizations", (string)null);
                });

            modelBuilder.Entity("NightTasker.TaskTracker.Core.Domain.Entities.OrganizationUser", b =>
                {
                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid")
                        .HasColumnName("organization_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<DateTimeOffset>("CreatedDateTimeOffset")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date_time_offset");

                    b.Property<string>("Role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("Member")
                        .HasColumnName("role");

                    b.Property<DateTimeOffset?>("UpdatedDateTimeOffset")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date_time_offset");

                    b.HasKey("OrganizationId", "UserId")
                        .HasName("pk_organization_users");

                    b.HasIndex("Role")
                        .HasDatabaseName("ix_organization_users_role");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_organization_users_user_id");

                    b.ToTable("organization_users", (string)null);
                });

            modelBuilder.Entity("NightTasker.TaskTracker.Core.Domain.Entities.Problem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("AssigneeId")
                        .HasColumnType("uuid")
                        .HasColumnName("assignee_id");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid")
                        .HasColumnName("author_id");

                    b.Property<DateTimeOffset>("CreatedDateTimeOffset")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date_time_offset");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uuid")
                        .HasColumnName("organization_id");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<string>("Text")
                        .HasColumnType("text")
                        .HasColumnName("text");

                    b.Property<DateTimeOffset?>("UpdatedDateTimeOffset")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date_time_offset");

                    b.HasKey("Id")
                        .HasName("pk_problems");

                    b.HasIndex("AssigneeId")
                        .HasDatabaseName("ix_problems_assignee_id");

                    b.HasIndex("AuthorId")
                        .HasDatabaseName("ix_problems_author_id");

                    b.HasIndex("OrganizationId")
                        .HasDatabaseName("ix_problems_organization_id");

                    b.ToTable("problems", (string)null);
                });

            modelBuilder.Entity("NightTasker.TaskTracker.Core.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreatedDateTimeOffset")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date_time_offset");

                    b.Property<DateTimeOffset?>("UpdatedDateTimeOffset")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_date_time_offset");

                    b.Property<string>("UserName")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("NightTasker.TaskTracker.Core.Domain.Entities.OrganizationUser", b =>
                {
                    b.HasOne("NightTasker.TaskTracker.Core.Domain.Entities.Organization", "Organization")
                        .WithMany("OrganizationUsers")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_organization_users_organizations_organization_id");

                    b.HasOne("NightTasker.TaskTracker.Core.Domain.Entities.User", "User")
                        .WithMany("OrganizationUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_organization_users_user_user_id");

                    b.Navigation("Organization");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NightTasker.TaskTracker.Core.Domain.Entities.Problem", b =>
                {
                    b.HasOne("NightTasker.TaskTracker.Core.Domain.Entities.User", "Assignee")
                        .WithMany()
                        .HasForeignKey("AssigneeId")
                        .HasConstraintName("fk_problems_user_assignee_id");

                    b.HasOne("NightTasker.TaskTracker.Core.Domain.Entities.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_problems_user_author_id");

                    b.HasOne("NightTasker.TaskTracker.Core.Domain.Entities.Organization", "Organization")
                        .WithMany("Problems")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_problems_organizations_organization_id");

                    b.Navigation("Assignee");

                    b.Navigation("Author");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("NightTasker.TaskTracker.Core.Domain.Entities.Organization", b =>
                {
                    b.Navigation("OrganizationUsers");

                    b.Navigation("Problems");
                });

            modelBuilder.Entity("NightTasker.TaskTracker.Core.Domain.Entities.User", b =>
                {
                    b.Navigation("OrganizationUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
