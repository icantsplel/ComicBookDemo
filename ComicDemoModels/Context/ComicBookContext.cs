using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComicDemoModels
{
    public class ComicBookContext : DbContext
    {
        public ComicBookContext(DbContextOptions<ComicBookContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComicBook>()
                .HasKey(c => c.Id);
        }

        public DbSet<ComicBook> ComicBooks { get; set; }

    }
}
