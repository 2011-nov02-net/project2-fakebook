using System;
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
            modelBuilder.Entity<PostEntity>(entity =>
            {
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

            modelBuilder.Entity<CommentEntity>(entity => {
                entity.ToTable("Comment", "Fakebook");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.User);

                entity.HasOne(e => e.User)
                        .WithMany(e => e.Comments)
                        .HasForeignKey(e => e.UserId)
                        .HasConstraint("FK_COMMENT_USER");
;

            });
        }
    }
}
