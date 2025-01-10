using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using dairyFarm.Entity;

namespace dairyFarm.TypeConfig
{
    public class LoginEntityTypeConfiguration : IEntityTypeConfiguration<Login>
    {
        private readonly string _tableName;
        private readonly string _schemaName;

        public LoginEntityTypeConfiguration(string tableName, string schemaName)
        {
            _tableName = tableName;
            _schemaName = schemaName;
        }
        public void Configure(EntityTypeBuilder<Login> builder)
        {
            builder.ToTable(_tableName, _schemaName);
            builder.Property(k => k.Id).ValueGeneratedOnAdd();
        }
    }
}
