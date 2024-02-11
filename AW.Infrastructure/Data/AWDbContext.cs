using Microsoft.EntityFrameworkCore;

using AW.Domain.Entities;

namespace AW.Infrastructure.Data;

public partial class AWDbContext : DbContext
{
    public AWDbContext()
    {
    }

    public AWDbContext(DbContextOptions<AWDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Address> Address { get; set; }

    public virtual DbSet<Administrator> Administrator { get; set; }

    public virtual DbSet<Cart> Cart { get; set; }

    public virtual DbSet<Category> Category { get; set; }

    public virtual DbSet<City> City { get; set; }

    public virtual DbSet<Country> Country { get; set; }

    public virtual DbSet<Craft> Craft { get; set; }

    public virtual DbSet<Craftman> Craftman { get; set; }

    public virtual DbSet<Culture> Culture { get; set; }

    public virtual DbSet<Customer> Customer { get; set; }

    public virtual DbSet<CustomerAddress> CustomerAddress { get; set; }

    public virtual DbSet<CustomerType> CustomerType { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<Sale> Sale { get; set; }

    public virtual DbSet<Ticket> Ticket { get; set; }

    public virtual DbSet<UserAccount> UserAccount { get; set; }

    public virtual DbSet<ActiveUserAccountAdministrator> ActiveUserAccountAdministrator { get; set; }

    public virtual DbSet<ActiveUserAccountCustomer> ActiveUserAccountCustomer { get; set; }

    public virtual DbSet<ActiveUserAccountCraftman> ActiveUserAccountCraftman { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            option => option.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery).MigrationsAssembly("AW.Api")
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AWDbContext).Assembly);
    }
}