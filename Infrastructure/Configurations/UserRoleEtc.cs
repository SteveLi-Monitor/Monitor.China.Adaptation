using Application.Entities;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Infrastructure.Configurations
{
    internal class UserRoleEtc : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.UiComponents).HasJsonConversion();

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasData(
                new UserRole
                {
                    Id = 1,
                    Name = "Admin",
                    UiComponents = new List<UiComponent>
                    {
                        new UiComponent
                        {
                            Section = "Procedures",
                            Module = "Settings",
                            Page = "UserRoles",
                            IsAuthorized = true,
                        },
                    },
                });
        }
    }
}
