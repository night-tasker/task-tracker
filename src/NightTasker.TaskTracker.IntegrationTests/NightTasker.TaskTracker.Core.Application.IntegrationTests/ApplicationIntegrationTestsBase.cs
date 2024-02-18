using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NightTasker.TaskTracker.Infrastructure.Persistence;
using NightTasker.TaskTracker.IntegrationTests.Framework;
using Xunit;

namespace NightTasker.TaskTracker.Core.Application.IntegrationTests;

[Collection("NpgSqlTestCollection")]
public abstract class ApplicationIntegrationTestsBase
{
    private readonly IServiceCollection _serviceCollection;
    private IServiceProvider _serviceProvider = null!;
    private readonly TestNpgSql _testNpgSqlFixture;
    
    protected CancellationToken CancellationToken { get; init; }

    protected ApplicationIntegrationTestsBase(TestNpgSql testNpgSql)
    {
        _testNpgSqlFixture = testNpgSql;
        _serviceCollection = new ServiceCollection();
        _serviceCollection.AddDbContext<ApplicationDbContext>(
            (_, option) => option.UseNpgsql($"{_testNpgSqlFixture.NpgSqlContainer.GetConnectionString()};Include Error Detail=true"));
        CancellationToken = new CancellationTokenSource().Token;
    }

    protected void RegisterService<T>(ServiceForRegister<T> serviceForRegister)
    {
        switch (serviceForRegister)
        {
            case { Lifetime: ServiceLifetime.Singleton }:
                if (serviceForRegister.Factory == null)
                    _serviceCollection.AddSingleton(typeof(T));
                else
                    _serviceCollection.AddSingleton(typeof(T), serviceForRegister.Factory);
                break;
            case { Lifetime: ServiceLifetime.Scoped }:
                if (serviceForRegister.Factory == null)
                    _serviceCollection.AddScoped(typeof(T));
                else
                    _serviceCollection.AddScoped(typeof(T), serviceForRegister.Factory);
                break;
            case { Lifetime: ServiceLifetime.Transient }:
                if (serviceForRegister.Factory == null)
                    _serviceCollection.AddTransient(typeof(T));
                else
                    _serviceCollection.AddTransient(typeof(T), serviceForRegister.Factory);
                break;
            case { Lifetime: null }:
                if (serviceForRegister.Factory == null)
                    _serviceCollection.AddScoped(typeof(T));
                else
                    _serviceCollection.AddScoped(typeof(T), serviceForRegister.Factory);
                break;
        }
    }
    
    protected void RegisterService<T1, T2>(ServiceForRegister<T1, T2> serviceForRegister) where T2 : T1
    {
        switch (serviceForRegister)
        {
            case { Lifetime: ServiceLifetime.Singleton }:
                if (serviceForRegister.Factory == null)
                    _serviceCollection.AddSingleton(typeof(T1), typeof(T2));
                else
                    _serviceCollection.AddSingleton(typeof(T1), typeof(T2));
                break;
            case { Lifetime: ServiceLifetime.Scoped }:
                if (serviceForRegister.Factory == null)
                    _serviceCollection.AddScoped(typeof(T1), typeof(T2));
                else
                    _serviceCollection.AddScoped(typeof(T1), typeof(T2));
                break;
            case { Lifetime: ServiceLifetime.Transient }:
                if (serviceForRegister.Factory == null)
                    _serviceCollection.AddTransient(typeof(T1), typeof(T2));
                else
                    _serviceCollection.AddTransient(typeof(T1), typeof(T2));
                break;
            case { Lifetime: null }:
                if (serviceForRegister.Factory == null)
                    _serviceCollection.AddScoped(typeof(T1), typeof(T2));
                else
                    _serviceCollection.AddScoped(typeof(T1), typeof(T2));
                break;
        }
    }


    protected void BuildServiceProvider()
    {
        _serviceProvider = _serviceCollection.BuildServiceProvider();
    }

    protected T GetService<T>() where T : notnull
    {
        return _serviceProvider.GetRequiredService<T>();
    }

    protected IServiceScope CreateScope()
    {
        return _serviceProvider.CreateScope();
    }

    protected AsyncServiceScope CreateAsyncScope()
    {
        return _serviceProvider.CreateAsyncScope();
    }

    protected void PrepareDatabase()
    {
        _testNpgSqlFixture.DropDatabase();
        GetService<ApplicationDbContext>().Database.Migrate();
    }
}

[CollectionDefinition("NpgSqlTestCollection")]
public class NpgSqlTestCollection : ICollectionFixture<TestNpgSql>;