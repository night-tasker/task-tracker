using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NightTasker.TaskTracker.Core.Domain.Entities;

namespace NightTasker.TaskTracker.Infrastructure.Persistence.EntityConfigurations;

public class ProblemEntityConfiguration : IEntityTypeConfiguration<Problem>
{
    public void Configure(EntityTypeBuilder<Problem> builder)
    {
        builder.ToTable("problems");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Text);

        builder.HasOne(problem => problem.Organization)
            .WithMany(organization => organization.Problems)
            .HasForeignKey(problem => problem.OrganizationId)
            .IsRequired();
        
        builder.HasOne(problem => problem.Author)
            .WithMany()
            .HasForeignKey(problem => problem.AuthorId)
            .IsRequired();
        
        builder.HasOne(problem => problem.Assignee)
            .WithMany()
            .HasForeignKey(problem => problem.AssigneeId)
            .IsRequired(false);
        
        builder.Property(x => x.Status)
            .HasConversion<string>();
    }
}