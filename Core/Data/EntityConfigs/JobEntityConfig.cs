using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TWJobs.Core.Models;

namespace TWJobs.Core.Data.EntityConfigs;

public class JobEntityConfig : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("Jobs");

        builder.HasKey(j => j.Id);

        builder.Property(j => j.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(j => j.Salary)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(j => j.Requirements)
            .IsRequired()
            .HasMaxLength(500);
    }
}