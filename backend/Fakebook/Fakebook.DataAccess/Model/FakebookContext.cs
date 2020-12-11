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
            base.OnModelCreating(modelBuilder);
        }
    }
}
