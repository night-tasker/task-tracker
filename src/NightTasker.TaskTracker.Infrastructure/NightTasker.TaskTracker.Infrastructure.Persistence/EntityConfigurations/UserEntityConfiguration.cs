using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NightTasker.TaskTracker.Core.Domain.Entities;

namespace NightTasker.TaskTracker.Infrastructure.Persistence.EntityConfigurations;

internal sealed class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(name: "users");
        
        builder.HasKey(user => user.Id);

        builder.Property(user => user.UserName)
            .HasMaxLength(32);

        builder.Property(user => user.CreatedDateTimeOffset);
        
        builder.Property(user => user.UpdatedDateTimeOffset);
    }
}