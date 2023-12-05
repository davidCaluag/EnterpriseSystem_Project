﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project_EnterpriseSystem.Services;

#nullable disable

namespace Project_EnterpriseSystem.Migrations
{
    [DbContext(typeof(UserDatabase))]
    partial class DatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("EnterpriseSystem_Project.Models.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("GenreName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("Project_EnterpriseSystem.Models.Artist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("Project_EnterpriseSystem.Models.Playlist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("PlayListTitle")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("PlaylistGenreId")
                        .HasColumnType("TEXT");

                    b.Property<int>("SongCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PlaylistGenreId");

                    b.HasIndex("UserId");

                    b.ToTable("Playlist");
                });

            modelBuilder.Entity("Project_EnterpriseSystem.Models.Song", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ArtistId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GenreId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("PlaylistId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("GenreId");

                    b.HasIndex("PlaylistId");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("Project_EnterpriseSystem.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserPasssword")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Project_EnterpriseSystem.Models.Playlist", b =>
                {
                    b.HasOne("EnterpriseSystem_Project.Models.Genre", "PlaylistGenre")
                        .WithMany()
                        .HasForeignKey("PlaylistGenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_EnterpriseSystem.Models.User", null)
                        .WithMany("ListOfPlaylists")
                        .HasForeignKey("UserId");

                    b.Navigation("PlaylistGenre");
                });

            modelBuilder.Entity("Project_EnterpriseSystem.Models.Song", b =>
                {
                    b.HasOne("Project_EnterpriseSystem.Models.Artist", "Artist")
                        .WithMany()
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EnterpriseSystem_Project.Models.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_EnterpriseSystem.Models.Playlist", null)
                        .WithMany("ListOfSongs")
                        .HasForeignKey("PlaylistId");

                    b.Navigation("Artist");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("Project_EnterpriseSystem.Models.Playlist", b =>
                {
                    b.Navigation("ListOfSongs");
                });

            modelBuilder.Entity("Project_EnterpriseSystem.Models.User", b =>
                {
                    b.Navigation("ListOfPlaylists");
                });
#pragma warning restore 612, 618
        }
    }
}
