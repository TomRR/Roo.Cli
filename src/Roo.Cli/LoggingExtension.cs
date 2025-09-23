namespace Roo.Cli;

public static class LoggingExtension
{
    public static IServiceCollection AddSerilogLogging(this IServiceCollection services)
    {
        services.AddTransient<IFileService, FileService>();
        
        return services;
    }
}