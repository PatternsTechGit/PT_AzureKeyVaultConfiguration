# Azure Key Vault Configuration using Asp.Net core

## What is Azure App Configuration?

[Azure App Configuration](https://docs.microsoft.com/en-us/azure/azure-app-configuration/overview) is part of Azure Development Services and it is also an Azure service that allows users to manage configuration within the cloud. ASP.NET Core builds a single, key-value-based configuration object using settings from one or more data sources specified by an app. These data sources are known as configuration providers.

For other languages and framework [click here](https://docs.microsoft.com/en-us/azure/azure-app-configuration/overview)


## What is Key Vault Configuration
 Azure Key Vault is a cloud service that provides a secure store for secrets. You can securely store keys, passwords, certificates, and other secrets. Azure key vaults may be created and managed through the Azure portal.

## About this exercise


Previously we have developed an **API** solution in `asp.net` core in which we have

* EF Code first approach to generate database of a fictitious bank application called **BBBank**.
* We have developed two api functions `GetLast12MonthBalances` & `GetLast12MonthBalances/{userId}` which returns data of the last 12 months total balances from database using data seeding.

For more details see [data seeding](https://github.com/PatternsTechGit/PT_AzureSql_EFDataSeeding) lab.



## **In this exercise**


**Backend Codebase**

In this exercise we will be working on **backend** codebase only.

In this exercise we will 

*  Setup **App Configuration** in Azure portal.
*  Read **App Configuration** value in code using DI.
*  Setup  secret value in **Azure key-vault**.
*  Setup **App Configuration** with key-Vault reference in Azure portal.
*  Read **key-Vault** value in code.
*  Access **Appsettings** endpoint in production environment. 

-----------------

## Step 1: Setup App Configuration

To create a new App Configuration:
 - Sign in to the [Azure portal](https://portal.azure.com/). 
 - To create a resource go to the upper-left corner of the home page, select **Create**.
 - In the Search services and marketplace box, enter App Configuration and select **Enter**.

![1](https://user-images.githubusercontent.com/100709775/183475224-da10e916-10c8-4924-8411-a13f63099b55.png)

Select App Configuration from the search results, and then select **Create**.

![2](https://user-images.githubusercontent.com/100709775/183475219-89e89979-03fa-4a2c-81e0-b7a4b3f852c7.png)

Enter the relevant information and click **`Review + Create`** button.

Once the configuration is created, Click **Access Keys** from left menu and turn on **Enable access keys** option. **Copy** the connection string which will be used in code later.

![3](https://user-images.githubusercontent.com/100709775/183476059-859c1e5a-f8db-4bd3-b7a6-213e726bae69.png)




Go to **Configuration explorer** under **Operations** section, Click **Create** button and select `key-value`, Enter the relevant key and value information and then click **Apply** button.

![5](https://user-images.githubusercontent.com/100709775/183476763-d23e0af2-b77a-4681-b032-eccd613490ba.png)


------

## Step 2 : Read App configuration

### What is Secret Manager?

Secret Manager stores sensitive data for development work outside of your project tree. This approach helps prevent the accidental sharing of app secrets within source code.

To initialize the secret manager navigate to the **BBBankAPI** project's root directory where **.csproj** file is placed, open the cmd prompt and run the following command to enable secrets storage in the project.

```
dotnet user-secrets init
```


We will add reference of **Microsoft.Azure.AppConfiguration.AspNetCore**  in our BBBankAPI project. This nuget allows developers to use Microsoft Azure App Configuration service as a configuration source in their applications. This package adds additional features for ASP.NET Core applications to the existing package Microsoft.Extensions.Configuration.AzureAppConfiguration.

To add this package open the **package console manager** or terminal, select BBBankAPI project and run the following command

```
dotnet add package Microsoft.Azure.AppConfiguration.AspNetCore
```

After secret manager initialization we will **setup the azure configuration app connecting string** in our secret manager, to do this copy the connection string from azure portal (step above) and paste it in following command and run it in **BBBankAPI** project's root directory where **.csproj** file is placed: 

```
dotnet user-secrets set ConnectionStrings:AppConfig "<your_connection_string>"
```

Open the `program.cs` and add the following code to **read the connection string** from secret manager. 


```cs
// Reading Azure App Config's Connection String.
var azureAppConfigConString = builder.Configuration.GetConnectionString("AppConfig");
```

After reading the connection string we will **configure the AzureAppConfiguration** as below : 

```cs
// In production use appSettingsFileSettings["AppConfig] in place of azureAppConfigConString
builder.Host.ConfigureAppConfiguration(builder =>
{
    //Connect to your App Config Store using the connection string
    appConfig = builder.Build();
    builder.AddAzureAppConfiguration(azureAppConfigConString);
})
            .ConfigureServices(services =>
            {
                services.AddControllersWithViews();
            });
```

Go to `TransactionService` and add dependency injection of `IConfiguration configuration` as below : 

```cs
using Microsoft.Extensions.Configuration
using Azure.Identity;


IConfigurationRoot appConfig = null;
 private readonly IConfiguration _configuration;
        public TransactionService(BBBankContext BBBankContext, IConfiguration configuration)
        {
            _bbBankContext = BBBankContext;
            _configuration = configuration;
        }
```
After DI we can use the configuration object to read the relevant values. 
We will read the **myName** value in `GetLast12MonthBalances` method. 
To read the value use the following command as below :

```cs
 var myname = _configuration["MyName"];
```

Here variable **myname** will get the value received from azure app configuration.  

---------------------

##  Step 3: Setup **Azure key-vault**
To create a new key-vault, sign in to the [Azure portal](https://portal.azure.com/). In the upper-left corner of the home page, select Create a resource. In the Search services and marketplace box, enter **key vault** and select **Enter** and click on the create button.

![6](https://user-images.githubusercontent.com/100709775/183485778-858790a6-857c-449d-a792-909d166df64c.png)

Enter the relevant information and click `Review + Create` button.

![7](https://user-images.githubusercontent.com/100709775/183486137-9fcf3c86-a34e-4c17-89d8-3020a75cae3a.png)

To create a new secret, Select **Secrets** option under the settings section and click Generate/Import button.

![8](https://user-images.githubusercontent.com/100709775/183486703-0518c69c-68b4-40fc-b0a9-e8ac8e98f9f0.png)

Enter the relevant information and click **Create** button.
![9](https://user-images.githubusercontent.com/100709775/183486696-72718810-535c-42d8-a96e-7820bc600a4e.png)


Go to secrets and select the newly create secret and copy the **secret identifier**. Make sure newly created secret is enabled as well.

![10](https://user-images.githubusercontent.com/100709775/183487539-83d235ef-0216-419b-8232-ffb9049d1dc8.png)

------------------------

## Step 4 : Configure key-vault reference

Go to App Configuration  and then select **Configuration explorer** under **Operations** section, Click **Create** button and select `key Vault reference`
![12](https://user-images.githubusercontent.com/100709775/183703942-06258ce8-4bc9-4e16-928d-29702525f368.png)


On a create window enter the key name and the relevant information, select the newly created secret under secret dropdown and then click Apply button. 


![11](https://user-images.githubusercontent.com/100709775/183703877-2b92b69c-2c15-429d-ac84-367f97182b69.png)

-----------------

## Step 5: Read key-Vault value 

We will add reference of **Azure.Identity** in our BBBankAPI project. The Key Vault SDK is using Azure Identity client library, which allows seamless authentication to Key Vault across environments with the code.

To add this package open the **package console manager**, select BBBankAPI project and run the following command

```cs
Install-Package Azure.Identity -Version 1.6.1
// for VScode
dotnet add package Azure.Identity --version 1.6.1
// also add this package for later steps
dotnet add package Microsoft.Azure.AppConfiguration.AspNetCore
```

Open the `program.cs` and add the following code to read the connection string from secret manager. 

```cs
// Reading Azure App Config's Connection String.
var azureAppConfigConString =builder.Configuration.GetConnectionString("AppConfig");
```

After reading the connection string we will configure the AzureAppConfiguration as below :

```cs
IConfigurationRoot azureAppConfigSettings = null;
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    //Configuration Builder based on Azure App Config
    azureAppConfigSettings = config.Build();

    config.AddAzureAppConfiguration(options =>
    {
        options.Connect(azureAppConfigConString)
                .ConfigureKeyVault(kv =>
                {
                    kv.SetCredential(new DefaultAzureCredential());
                });
    });
});
```

Once the azure configuration as done then we will read the **key-vault** value by its key name as below :

```cs
// example of reading KeyVault reference from Azure App Config inside Program.cs
var connectionString = azureAppConfigSettings["BBBankAPI:Settings:BBBankDBConnectionString"];
```

Here variable **connectionString** will get the value received from azure app configuration through key vault secret.

As we are getting the connection string from Azure configuration so we don't need the connection string from **appsettings.json** so we will comment/remove the connection string part from **appsettings.json**

-----------

## Step 6: Production environment Setup

Go to **appsettings.json** and add a new key named **AppConfig** with connection string as value. 

```json
  "AppConfig": "Endpoint=https://bbbankconfigs.azconfig.io;Id=ihvR-l5-s0:8IEbEMHt4vKu/JdP4/BY;Secret=exdCCJBDOQvsO1An0Zzr9627cJ/h6orm/XXXXXX-XXXX"
```

After setting the app config go to `program.cs` file and use the configurations as below : 

```cs
// Reading App Config's with key-vault in Production environment.
// In production use configuration["AppConfig] in place of azureAppConfigConString
IConfigurationRoot azureAppConfigSettings = null;
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    //Configuration Builder based on Azure App Config
    azureAppConfigSettings = config.Build();

    config.AddAzureAppConfiguration(options =>
    {
        options.Connect(configuration["AppConfig"])
                .ConfigureKeyVault(kv =>
                {
                    kv.SetCredential(new DefaultAzureCredential());
                });
    });
});
```

## Final Output 
**Run** the application as see its working. 

But before that make sure you are **logged in** as a user which have **permissions** to access Azure App config and your subscription.
![](/Before/1.jpg)

 



