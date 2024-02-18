using Microsoft.Extensions.DependencyInjection;

namespace NightTasker.TaskTracker.IntegrationTests.Framework;

public record ServiceForRegister<T1>(Func<IServiceProvider, object>? Factory = null, ServiceLifetime? Lifetime = null);

public record ServiceForRegister<T1, T2>(Func<IServiceProvider, object>? Factory = null, ServiceLifetime? Lifetime = null)
    where T2 : T1;
