using Microsoft.EntityFrameworkCore;

namespace Data.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public double Cost { get; set; }
    }

    internal class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        const int NameMaxLength = 256;

        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Transaction> b)
        {
            b.ToTable("Transaction");
            b.HasKey(e => e.TransactionId);

            b.Property(e => e.TransactionId).ValueGeneratedOnAdd();
            b.Property(e => e.Name).HasMaxLength(NameMaxLength).IsRequired();
            b.Property(e => e.Date).IsRequired();
        }
    }


}
