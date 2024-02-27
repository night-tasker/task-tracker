#pragma warning disable CA2255
using System.Runtime.CompilerServices;

namespace NightTasker.TaskTracker.Infrastructure.Persistence.Configuration;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}

