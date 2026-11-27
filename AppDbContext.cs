using Microsoft.EntityFrameworkCore;

namespace PvStudentsWebApi;

public class AppDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // optionsBuilder.UseSqlite("Data source=students.db");
        optionsBuilder.UseNpgsql("Host=my-postgres;Port=5432;Username=postgres;Password=mysecretpassword;Database=postgres");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seeding
        modelBuilder.Entity<Student>(e =>
        {
            e.HasData(
                new Student { Id = 1, Name = "Nikita", Age = 24 },
                new Student { Id = 2, Name = "Oleg", Age = 42 },
                new Student { Id = 3, Name = "Anna", Age = 20 },
                new Student { Id = 4, Name = "Maria", Age = 22 },
                new Student { Id = 5, Name = "Dmytro", Age = 19 },
                new Student { Id = 6, Name = "Olena", Age = 31 },
                new Student { Id = 7, Name = "Andrii", Age = 27 },
                new Student { Id = 8, Name = "Sofia", Age = 21 },
                new Student { Id = 9, Name = "Taras", Age = 35 },
                new Student { Id = 10, Name = "Iryna", Age = 28 }       
            );
        });
    }
}
