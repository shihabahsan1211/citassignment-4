using Business;
using Persistence;

public static class ServiceCollectionExtensions
{

    public static void AddUserDefinedServices(this IServiceCollection services)
    {
        services.AddPersistanceServices();
        services.AddBusinessServices();
    }
}
