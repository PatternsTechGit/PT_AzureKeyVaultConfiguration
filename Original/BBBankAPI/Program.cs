using Azure.Identity;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Reading the appsettings.json from current directory.
var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

#region App Config's without key-vault
// Reading App Config's without key-vault.
////// Reading Azure App Config's Connection String
////IConfigurationRoot appConfig = null;
////var azureAppConfigConString = builder.Configuration.GetConnectionString("AppConfig");
////// In production use appSettingsFileSettings["AppConfig] in place of azureAppConfigConString
////builder.Host.ConfigureAppConfiguration(builder =>
////{
////    //Connect to your App Config Store using the connection string
////    appConfig = builder.Build();
////    builder.AddAzureAppConfiguration(azureAppConfigConString);
////})
////            .ConfigureServices(services =>
////            {
////                services.AddControllersWithViews();
////            });

#endregion

#region App Config's with key-vault
// Reading App Config's with key-vault.
//var azureAppConfigConString =builder.Configuration.GetConnectionString("AppConfig");
//// In production use appSettingsFileSettings["AppConfig] in place of azureAppConfigConString
//IConfigurationRoot azureAppConfigSettings = null;
//builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
//{
//    //Confiuration Builder based on Azure App Config
//    azureAppConfigSettings = config.Build();

//    config.AddAzureAppConfiguration(options =>
//    {
//        //options.Connect(azureAppConfigConString)
//        options.Connect(azureAppConfigConString)
//                .ConfigureKeyVault(kv =>
//                {
//                    kv.SetCredential(new DefaultAzureCredential());
//                });
//    });
//});

#endregion


#region App Config's with key-vault in Production envirnomnet
// Reading App Config's with key-vault in Production envirnomnet.
// In production use appSettingsFileSettings["AppConfig] in place of azureAppConfigConString
IConfigurationRoot azureAppConfigSettings = null;
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    //Confiuration Builder based on Azure App Config
    azureAppConfigSettings = config.Build();

    config.AddAzureAppConfiguration(options =>
    {
        //options.Connect(azureAppConfigConString)
        options.Connect(configuration["AppConfig"])
                .ConfigureKeyVault(kv =>
                {
                    kv.SetCredential(new DefaultAzureCredential());
                });
    });
});

#endregion

// Fetching the value BBBankDBConnString from connectionstring section.
//var connectionString = configuration.GetConnectionString("BBBankDBConnString");

// example of reading KeyVault refrence from Azure App Config inside Program.cs
var connectionString = azureAppConfigSettings["BBBankAPI:Settings:BBBankDBConnectionString"];

// Add services to the container.

builder.Services.AddControllers();


///...Dependency Injection settings
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<DbContext, BBBankContext>();

//Adding EF DBContext in the application services using the connectionString fetched above.
//UseLazyLoadingProxies : Lazy loading means that the related data is transparently loaded from the database when the navigation property is accessed.
builder.Services.AddDbContext<BBBankContext>(
b => b.UseSqlServer(connectionString)
.UseLazyLoadingProxies(true)
);
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
