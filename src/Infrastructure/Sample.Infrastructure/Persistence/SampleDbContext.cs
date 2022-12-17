using Microsoft.EntityFrameworkCore;
using Sample.Domain.Meetings;
using Sample.Infrastructure.Domain.Meetings;
using System.Reflection.Emit;
using System.Reflection;

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
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

        public DbSet<MeetingEntity> Meetings { get; set; }

    }
}
