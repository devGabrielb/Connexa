﻿// <auto-generated />
using System;
using Connexa.Infra.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Connexa.Infra.Migrations
{
    [DbContext(typeof(ConnexaContext))]
    partial class ConnexaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Connexa.Domain.Entities.Chore", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("AssignTo")
                        .HasColumnType("uuid")
                        .HasColumnName("assign_to");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("due_date");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uuid")
                        .HasColumnName("group_id");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid")
                        .HasColumnName("owner_id");

                    b.Property<int>("State")
                        .HasColumnType("integer")
                        .HasColumnName("state");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_chores");

                    b.HasIndex("GroupId")
                        .HasDatabaseName("ix_chores_group_id");

                    b.HasIndex("OwnerId")
                        .HasDatabaseName("ix_chores_owner_id");

                    b.ToTable("chores", (string)null);
                });

            modelBuilder.Entity("Connexa.Domain.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("ChoreId")
                        .HasColumnType("uuid")
                        .HasColumnName("chore_id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_comments");

                    b.HasIndex("ChoreId")
                        .HasDatabaseName("ix_comments_chore_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_comments_user_id");

                    b.ToTable("comments", (string)null);
                });

            modelBuilder.Entity("Connexa.Domain.Entities.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid")
                        .HasColumnName("owner_id");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_groups");

                    b.HasIndex("OwnerId")
                        .HasDatabaseName("ix_groups_owner_id");

                    b.ToTable("groups", (string)null);
                });

            modelBuilder.Entity("Connexa.Domain.Entities.MemberGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uuid")
                        .HasColumnName("group_id");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_member_groups");

                    b.HasIndex("GroupId")
                        .HasDatabaseName("ix_member_groups_group_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_member_groups_user_id");

                    b.ToTable("member_groups", (string)null);
                });

            modelBuilder.Entity("Connexa.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Connexa.Domain.Entities.Chore", b =>
                {
                    b.HasOne("Connexa.Domain.Entities.Group", null)
                        .WithMany("Chores")
                        .HasForeignKey("GroupId")
                        .HasConstraintName("fk_chores_family_groups_group_id");

                    b.HasOne("Connexa.Domain.Entities.User", null)
                        .WithMany("Tasks")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_chores_users_owner_id");
                });

            modelBuilder.Entity("Connexa.Domain.Entities.Comment", b =>
                {
                    b.HasOne("Connexa.Domain.Entities.Chore", null)
                        .WithMany("Comments")
                        .HasForeignKey("ChoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_comments_chores_chore_id");

                    b.HasOne("Connexa.Domain.Entities.User", null)
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_comments_users_user_id");
                });

            modelBuilder.Entity("Connexa.Domain.Entities.Group", b =>
                {
                    b.HasOne("Connexa.Domain.Entities.User", null)
                        .WithMany("FamilyGroups")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_groups_users_owner_id");
                });

            modelBuilder.Entity("Connexa.Domain.Entities.MemberGroup", b =>
                {
                    b.HasOne("Connexa.Domain.Entities.Group", null)
                        .WithMany("Members")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_member_groups_groups_group_id");

                    b.HasOne("Connexa.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_member_groups_users_user_id");
                });

            modelBuilder.Entity("Connexa.Domain.Entities.Chore", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("Connexa.Domain.Entities.Group", b =>
                {
                    b.Navigation("Chores");

                    b.Navigation("Members");
                });

            modelBuilder.Entity("Connexa.Domain.Entities.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("FamilyGroups");

                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
