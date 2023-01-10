using Microsoft.EntityFrameworkCore;
using PaymentPhoneService.Infrastructure.Models.DbMaps;
using PaymentPhoneService.Infrastructure.Models.DbModels;

namespace PaymentPhoneService.Infrastructure.Models;

public class TransactionDbContext:DbContext
{
    public DbSet<TransactionDbModel> Transactions { get; set; }

    public TransactionDbContext(DbContextOptions<TransactionDbContext> options):base(options)
    {
        Database.Migrate();
    }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new TransactionDbMap());
    }
}