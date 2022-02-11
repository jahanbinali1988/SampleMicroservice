using Sample.Domain.Meeting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Sample.Infrastructure.Domain.Meeting
{
    internal class MeetingTypeConfiguration : IEntityTypeConfiguration<MeetingEntity>
    {
        public void Configure(EntityTypeBuilder<MeetingEntity> builder)
        {
            builder.HasIndex(x => x.Id)
                .IsUnique();

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("SYSDATETIMEOFFSET()");

            builder.Property<bool>("IsDeleted")
                .HasDefaultValue(false);

            builder.Property<DateTimeOffset?>("DeletedAt")
                .IsRequired(false);

            builder.Ignore(c => c.Duration);

            builder.HasQueryFilter(p => EF.Property<bool>(p, "IsDeleted") == false);

        }
    }
}
