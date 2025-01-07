using dairyFarm.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dairyFarm.TypeConfig
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        private readonly string _tableName;
        private readonly string _schemaName;

        public ProductEntityTypeConfiguration(string tableName, string schemaName)
        {
            _tableName = tableName;
            _schemaName = schemaName;
        }
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(_tableName, _schemaName);
            builder.HasKey(k => new { k.Id, k.Name });
        }
    }
}

