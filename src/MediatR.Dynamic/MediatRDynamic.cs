using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace MediatR.Dynamic
{
    public static class MediatRDynamic
    {
        public delegate INotificationHandler<T> ServiceResolver<T>(T type)
            where T : INotification;

        public static void AddMediatRDynamic(this IServiceCollection services)
        {
            // Register the DynamicNotificationRegistrar as an INotificationHandler<>
            var existing = services.FirstOrDefault(x => x.ServiceType == typeof(IDynamicNotificationRegistrar<>));
            if (existing == null)
                services.AddSingleton(typeof(IDynamicNotificationRegistrar<>), typeof(DynamicNotificationRegistrar<>));
        }

        public static void AddDynamicNotificationHandler<T>(this IServiceCollection services)
            where T : INotification
        {
            services.AddSingleton<INotificationHandler<T>>(sp => sp.GetRequiredService<IDynamicNotificationRegistrar<T>>());
        }
    }
}
