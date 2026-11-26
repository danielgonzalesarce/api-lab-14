using api_lab_14.Domain;
using Microsoft.EntityFrameworkCore;

namespace api_lab_14.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Invoice> Invoices { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Detail> Details { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(45);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(45);
                entity.Property(e => e.DocumentNumber).IsRequired().HasMaxLength(45);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(45);
                entity.Property(e => e.Price).IsRequired();
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.InvoiceId);
                entity.Property(e => e.InvoiceNumber).IsRequired().HasMaxLength(45);
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Total).IsRequired();
                entity.HasOne(e => e.Customer)
                    .WithMany(c => c.Invoices)
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Detail>(entity =>
            {
                entity.HasKey(e => e.DetailId);
                entity.Property(e => e.Amount).IsRequired();
                entity.Property(e => e.Price).IsRequired();
                entity.Property(e => e.SubTotal).IsRequired();
                entity.HasOne(e => e.Product)
                    .WithMany(p => p.Details)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(e => e.Invoice)
                    .WithMany(i => i.Details)
                    .HasForeignKey(e => e.InvoiceId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
