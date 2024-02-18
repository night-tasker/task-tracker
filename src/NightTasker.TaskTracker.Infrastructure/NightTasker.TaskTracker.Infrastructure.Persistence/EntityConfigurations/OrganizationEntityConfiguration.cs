using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NightTasker.TaskTracker.Core.Domain.Entities;

namespace NightTasker.TaskTracker.Infrastructure.Persistence.EntityConfigurations;

internal sealed class OrganizationEntityConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.ToTable(name: "organizations");
        
        builder.HasKey(organization => organization.Id);

        builder.Property(organization => organization.CreatedDateTimeOffset);
        
        builder.Property(organization => organization.UpdatedDateTimeOffset);
    }
}