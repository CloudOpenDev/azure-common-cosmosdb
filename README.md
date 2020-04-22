# Azure Common CosmosDB
This library is for setting up CosmosDB in dotnet core project.

## Usage
To setup dependency injection use the following steps:

### CosmosDBSettings class
```cs
public class CosmosDBSettings
{
    public string AccountEndpoint { get; set; }

    public string AuthKeyOrResourceToken { get; set; }

    public string DatabaseId { get; set; }

    public string CollectionId { get; set; }
}
```

### Extension method

```cs
public static IServiceCollection ConfigureCustomerServices(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        var settings = configuration.GetSection("<configuration_section_name>").Get<CosmosDBSettings>();

        services
            .AddTransient<ICosmosDBRepository<entity_name>>(options => 
                new CosmosDBRepository<entity_name>(
                    settings.AccountEndpoint,
                    settings.AuthKeyOrResourceToken,
                    settings.DatabaseId,
                    settings.CollectionId));
        
        return services;
    }
```
