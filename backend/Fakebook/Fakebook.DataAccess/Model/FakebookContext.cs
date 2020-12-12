using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Fakebook.DataAccess.Model
{
    public class FakebookContext : DbContext
    {
        public FakebookContext([NotNull] DbContextOptions options) :
            base(options) { }

        public DbSet<UserEntity> UserEntities { get; set; }
        public DbSet<PostEntity> PostEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>(entity => {
                entity.ToTable("User", "Fakebook");

                entity.Property(e => e.FirstName)
                      .IsRequired();

                entity.Property(e => e.LastName)
                      .IsRequired();

                entity.Property(e => e.Email)
                      .IsRequired();
            });

            modelBuilder.Entity<FollowEntity>(entity => {
                entity.ToTable("Follow", "Fakebook");

                entity.HasKey(e => new { e.FollowerId, e.FolloweeId })
                      .HasName("Pk_FollowEntity");
                
                entity.HasOne(e => e.Follower)
                      .WithMany(e => e.Followees)
                      .HasForeignKey(e => e.FollowerId)
                      .HasConstraintName("FK_Follow_FollowerId")
                      .OnDelete(DeleteBehavior.ClientNoAction);

                entity.HasOne(e => e.Followee)
                      .WithMany(e => e.Followers)
                      .HasForeignKey(e => e.FolloweeId)
                      .HasConstraintName("FK_Follow_FolloweeId")
                      .OnDelete(DeleteBehavior.ClientNoAction);
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
                entity.Property(e => e.Picture)
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

                entity.Property(e => e.Content)
                      .IsRequired();
                
                entity.HasOne(e => e.Post)
                      .WithMany(p => p.Comments)
                      .HasForeignKey(e => e.PostId)
                      .HasConstraintName("Fk_Comment_Post");

                entity.HasOne(e => e.ParentComment)
                      .WithMany(pc => pc.ChildrenComments)
                      .HasForeignKey(e => e.ParentId)
                      .HasConstraintName("Fk_Comment_Comment");
                
                entity.HasOne(e => e.User)
                      .WithMany(e => e.Comments)
                      .HasForeignKey(e => e.UserId)
                      .HasConstraintName("FK_COMMENT_USER");
            });

            modelBuilder.Entity<LikeEntity>(entity => {
                entity.ToTable("Like", "Fakebook");

                entity.HasOne(e => e.Post)
                      .WithMany(p => p.Likes)
                      .HasForeignKey(e => e.PostId)
                      .HasConstraintName("FK_Like_Post");

                entity.HasOne(e => e.User)
                      .WithMany(u => u.Likes)
                      .HasForeignKey(e => e.UserId)
                      .HasConstraintName("FK_Like_User");
            });
        }
    }
}