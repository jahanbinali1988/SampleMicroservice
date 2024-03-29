﻿using Sample.Infrastructure.EventProcessing;
using Sample.Infrastructure.Persistence;
using Sample.SharedKernel.EventProcessing.DomainEvent;
using Sample.SharedKernel.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Domain.Meetings.DomainServices;
using Sample.Domain.Meetings;
using Sample.Infrastructure.DomainService.Meetings;
using Sample.Infrastructure.Domain.Meetings;

namespace Sample.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, string mssqlConnection)
        {
            services.AddDbContextPool<SampleDbContext>(options =>
            {
                options.UseSqlServer(mssqlConnection);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.EnableSensitiveDataLogging(true);
            }, 1024);

            services.AddScoped<IMeetingRepository, MeetingRepository>();

            services.AddScoped<IBookMeetingService, BookMeetingService>();
            services.AddScoped<IReserveMeetingService, ReserveMeetingService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();

            return services;
        }

        public static IServiceCollection AddConnection(this IServiceCollection services, IConfiguration configuration)
        {
            var sqlConnection = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            AddRepository(services, sqlConnection);

            return services;
        }
    }
}
