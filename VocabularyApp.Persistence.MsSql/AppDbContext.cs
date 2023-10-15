using VocabularyApp.Persistence.MsSql.Entities;
using Microsoft.EntityFrameworkCore;

namespace VocabularyApp.Persistence.MsSql;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Word> Words => Set<Word>();
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));

        base.OnModelCreating(modelBuilder);
    }
}

