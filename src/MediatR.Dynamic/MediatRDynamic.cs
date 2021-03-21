using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MediatR.Dynamic
{
    public static class MediatRDynamic
    {
        public delegate INotificationHandler<T> ServiceResolver<T>(T type)
            where T : INotification;

        public static void AddMediatRDynamic(this IServiceCollection services)
        {
            // Register the DynamicNotificationRegistrar as an INotificationHandler<>
            services.TryAddSingleton(typeof(IDynamicNotificationManager<>), typeof(DynamicNotificationRegistrar<>)); 
            services.TryAddSingleton(typeof(IDynamicFilteredNotificationManager<>), typeof(DynamicFilteredNotificationManager<>));
        }

        public static void AddDynamicNotificationHandlerManager<TNotification>(this IServiceCollection services)
            where TNotification : INotification
        {
            // make sure someone doesnt try an register the same object more than once
            services.TryAddSingleton<INotificationHandler<TNotification>>(sp => sp.GetRequiredService<IDynamicNotificationManager<TNotification>>());
        }

        public static void AddDynamicFilteredNotificationHandlerManager<TFilteredNotification>(this IServiceCollection services)
             where TFilteredNotification : IDynamicFilteredNotification
        {
            // make sure someone doesnt try an register the same object more than once
            services.TryAddSingleton<INotificationHandler<TFilteredNotification>>(sp => sp.GetRequiredService<IDynamicFilteredNotificationManager<TFilteredNotification>>());
        }
    }
}
