using Microsoft.EntityFrameworkCore;
using ExpenseControl.Domain.Entities;

namespace ExpenseControl.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // PERSON
            modelBuilder.Entity<Person>()
                .Property(p => p.Name)
                .HasMaxLength(200)
                .IsRequired();

            // CATEGORY
            modelBuilder.Entity<Category>()
                .Property(c => c.Description)
                .HasMaxLength(400)
                .IsRequired();

            // TRANSACTION
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Description)
                .HasMaxLength(400)
                .IsRequired();

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Amount)
                .IsRequired();

            // RELACIONAMENTOS

            modelBuilder.Entity<Transaction>()
                .HasOne<Person>()
                .WithMany(p => p.Transactions)
                .HasForeignKey(t => t.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>()
                .HasOne<Category>()
                .WithMany()
                .HasForeignKey(t => t.CategoryId);
        }
    }
}