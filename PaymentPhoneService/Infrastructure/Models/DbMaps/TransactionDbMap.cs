using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentPhoneService.Infrastructure.Models.DbModels;

namespace PaymentPhoneService.Infrastructure.Models.DbMaps;

public class TransactionDbMap : IEntityTypeConfiguration<TransactionDbModel>
{
    public void Configure(EntityTypeBuilder<TransactionDbModel> builder)
    {
        builder.Property(p => p.CreateDate).HasColumnType("TIMESTAMP");
    }
}