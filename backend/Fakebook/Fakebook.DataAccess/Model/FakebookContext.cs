﻿using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.EntityFrameworkCore;

namespace Fakebook.DataAccess.Model
{
    public class FakebookContext : DbContext
    {
        public FakebookContext([NotNull] DbContextOptions options) :
            base(options) {
        }

        public DbSet<UserEntity> UserEntities { get; set; }
        public DbSet<PostEntity> PostEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>(entity => {
                entity.ToTable("User");

                entity.Property(e => e.FirstName)
                    .IsRequired();

                entity.Property(e => e.LastName)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .IsRequired();
            });

            modelBuilder.Entity<FollowEntity>(entity => {
                entity.ToTable("Follow");

                entity.Property(e => e.FolloweeId)
                    .IsRequired();

                entity.Property(e => e.FollowerId)
                    .IsRequired();

                entity.HasOne(e => e.Follower)
                    .WithMany()
                    .HasForeignKey(e => e.FollowerId);

                entity.HasOne(e => e.Followee)
                    .WithMany()
                    .HasForeignKey(e => e.FolloweeId);
            });

            modelBuilder.Entity<PostEntity>(entity => {
                entity.ToTable("Post", "Fakebook");
                entity.HasOne(e => e.User)
                    .WithMany(e => e.Posts)
                    .HasForeignKey(e => e.UserId)
                    .HasConstraintName("FK_Post_UserId");
                entity.Property(e => e.Content)
                    .HasColumnType("string")
                    .IsRequired();
                entity.Property(e => e.CreatedAt)
                     .HasColumnType("datetime2")
                    .HasDefaultValueSql("(getdatetime())");
            });
        }
    }
}