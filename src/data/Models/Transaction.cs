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
        const int KeyMaxLength = 36;
        const int NameMaxLength = 256;
        const int DescMaxLength = 2048;
        const int NoteMaxLength = 4096;

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
