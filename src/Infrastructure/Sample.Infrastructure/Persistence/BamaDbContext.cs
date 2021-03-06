using Sample.Domain.Meeting;
using Sample.Infrastructure.Domain.Meeting;
using Microsoft.EntityFrameworkCore;

namespace Sample.Infrastructure.Persistence
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MeetingTypeConfiguration());
        }

        public DbSet<MeetingEntity> Meetings { get; set; }

    }
}
