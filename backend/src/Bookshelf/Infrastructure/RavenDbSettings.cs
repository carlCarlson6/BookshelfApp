using System.Security.Cryptography.X509Certificates;
using Bookshelf.Users;
using Raven.Client.Documents;
using Raven.Client.Documents.Conventions;
using Raven.Client.Documents.Indexes;

namespace Bookshelf.Infrastructure;

public class RavenDatabaseSettings
{
    public string[] Urls { get; set; }
    public string DatabaseName { get; set; }
    public string CertPath { get; set; }
    public string CertPass { get; set; }
    public string Thumbprint { get; set; }   
}

public static class RavenDbExtensions
{
    public static IServiceCollection AddRavenDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var (dbSettings, cert) = configuration.GetRavenDbSettings();
        var store = new DocumentStore
        {
            Urls = dbSettings.Urls,
            Database = dbSettings.DatabaseName,
            Certificate = cert,
            Conventions = services.RavenConventions()
        }.Initialize();
 
        IndexCreation.CreateIndexes(typeof(Startup).Assembly, store);
        
        return services;
    }

    private static (RavenDatabaseSettings, X509Certificate2?) GetRavenDbSettings(this IConfiguration configuration)
    {
        var dbSettings = new RavenDatabaseSettings();
        configuration.Bind(nameof(RavenDatabaseSettings), dbSettings);

        X509Certificate2? certificate;
        if (!string.IsNullOrWhiteSpace(dbSettings.Thumbprint))
        {
            certificate = LoadByThumbprint(dbSettings.Thumbprint);
        }
        else
        {
            certificate = !string.IsNullOrEmpty(dbSettings.CertPath)
                ? new X509Certificate2(dbSettings.CertPath, dbSettings.CertPass)
                : null;
        }

        return (dbSettings, certificate);
    }

    private static X509Certificate2? LoadByThumbprint(string thumbprint)
    {
        using var certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        certStore.Open(OpenFlags.ReadOnly);

        return Enumerable.OfType<X509Certificate2>(certStore.Certificates)
            .FirstOrDefault(x => x.Thumbprint == thumbprint);
    }
    
    private static DocumentConventions RavenConventions(this object _) => new()
    {
        FindIdentityProperty = info => info.Name == "Id",
        FindCollectionName = type => type == typeof(UserDocument)
            ? UserDocument.CollectionName
            : DocumentConventions.DefaultGetCollectionName(type)
    };
}