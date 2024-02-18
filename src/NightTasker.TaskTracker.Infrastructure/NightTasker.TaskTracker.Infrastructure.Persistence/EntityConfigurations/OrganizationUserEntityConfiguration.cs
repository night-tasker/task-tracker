using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NightTasker.TaskTracker.Core.Domain.Entities;
using NightTasker.TaskTracker.Core.Domain.Enums;

namespace NightTasker.TaskTracker.Infrastructure.Persistence.EntityConfigurations;

public class OrganizationUserEntityConfiguration : IEntityTypeConfiguration<OrganizationUser>
{
    public void Configure(EntityTypeBuilder<OrganizationUser> builder)
    {
        builder.ToTable(name: "organization_users");
        
        builder.HasKey(organizationUser => new { organizationUser.OrganizationId, organizationUser.UserId });

        builder.Property(organizationUser => organizationUser.Role)
            .HasConversion<string>()
            .HasDefaultValue(OrganizationUserRole.Member);
        
        builder.HasIndex(organizationUser => organizationUser.Role);
            
        builder.HasOne(organizationUser => organizationUser.Organization)
            .WithMany(organization => organization.OrganizationUsers)
            .HasForeignKey(organizationUser => organizationUser.OrganizationId);
        
        builder.HasOne(organizationUser => organizationUser.User)
            .WithMany(user => user.OrganizationUsers)
            .HasForeignKey(organizationUser => organizationUser.UserId);
    }
}