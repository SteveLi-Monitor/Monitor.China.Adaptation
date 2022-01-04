using Application.Common;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    internal class ApplicationUserEtc : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.UiComponents).HasJsonConversion();

            builder.HasIndex(x => x.Username).IsUnique();
        }
    }
}
