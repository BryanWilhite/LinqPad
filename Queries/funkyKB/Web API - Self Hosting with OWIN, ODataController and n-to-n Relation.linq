<Query Kind="Program">
  <NuGetReference Prerelease="true">morelinq</NuGetReference>
  <NuGetReference>EntityFramework</NuGetReference>
  <NuGetReference>Microsoft.AspNet.OData</NuGetReference>
  <NuGetReference>Microsoft.AspNet.WebApi.Owin</NuGetReference>
  <NuGetReference>Microsoft.Net.Http</NuGetReference>
  <NuGetReference>Microsoft.Owin.SelfHost</NuGetReference>
  <NuGetReference>Microsoft.Owin</NuGetReference>
  <NuGetReference>Owin</NuGetReference>
  <NuGetReference>System.Net.Http</NuGetReference>
  <Namespace>Microsoft.AspNet.OData.Builder</Namespace>
  <Namespace>Microsoft.AspNet.OData.Extensions</Namespace>
  <Namespace>Microsoft.AspNet.OData.Query</Namespace>
  <Namespace>Microsoft.AspNet.OData</Namespace>
  <Namespace>Microsoft.Owin.Hosting</Namespace>
  <Namespace>Owin</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Web.Http.Dispatcher</Namespace>
  <Namespace>System.Web.Http</Namespace>
</Query>

void Main()
{
    var baseAddress = "http://localhost:9000/";

    var client = new HttpClient();
    try
    {
        using (WebApp.Start<Startup>(url: baseAddress))
        {
            HttpResponseMessage response;

            Action<string> callOwin = path =>
            {
                response = client.GetAsync(baseAddress + path).Result;
                response.Dump();
                response.Content.ReadAsStringAsync().Result.Dump();
            };

            callOwin("odata/$metadata");
            callOwin("odata/Client/Edm.GetBoundFunctionValue");
            callOwin("odata/Client/Edm.GetAccount(clientId=100,accountId=7)");
        }
    }
    finally
    {
        client.Dispose();
    }
}

public class Client
{
    public int ClientId { get; set; }
    public string Name { get; set; }
}

public class ClientAccount
{
    public int ClientId { get; set; }
    public int AccountId { get; set; }
}

public class Account
{
    public int AccountId { get; set; }
    public string AccountName { get; set; }
}

public class Repository
{
    static Repository()
    {
        accounts = new[]
        {
            new Account { AccountId = 1, AccountName = "Account One" },
            new Account { AccountId = 2, AccountName = "Account Two" },
            new Account { AccountId = 3, AccountName = "Account Three" },
            new Account { AccountId = 4, AccountName = "Account Four" },
            new Account { AccountId = 5, AccountName = "Account Five" },
            new Account { AccountId = 6, AccountName = "Account Six" },
            new Account { AccountId = 7, AccountName = "Account Seven" },
        };

        clientAccounts = new[]
        {
            new ClientAccount { AccountId = 2, ClientId = 100 },
            new ClientAccount { AccountId = 4, ClientId = 100 },
            new ClientAccount { AccountId = 7, ClientId = 100 },
            new ClientAccount { AccountId = 2, ClientId = 101 },
            new ClientAccount { AccountId = 5, ClientId = 101 },
            new ClientAccount { AccountId = 7, ClientId = 102 },
        };

        clients = new[]
        {
            new Client { ClientId = 100, Name = "Ms. One" },
            new Client { ClientId = 101, Name = "Mr. Two", },
            new Client { ClientId = 102, Name = "Mrs. Three" },
        };
    }
    
    public Account GetAccount()
    {
        return accounts.ElementAt(3);
    }

    public IQueryable<Account> GetAccountsByClient(int clientId)
    {
        var accountIds = clientAccounts
            .Where(i => i.ClientId == clientId)
            .Select(i => i.AccountId)
            .ToArray();
        var data = accounts
            .Where(i => accountIds.Contains(i.AccountId));

        return data.AsQueryable();
    }

    static IEnumerable<Client> clients;
    static IEnumerable<ClientAccount> clientAccounts;
    static IEnumerable<Account> accounts;
}

public class ClientController : ODataController
{
    public ClientController()
    {
        this._repository = new Repository();
        this._repository.Dump("controller constructed and repository loaded");
    }

    public IHttpActionResult GetAccount(int clientId, int accountId)
    {
        var result = this._repository
            .GetAccountsByClient(clientId)
            ?.SingleOrDefault(i => i.AccountId == accountId);
        if(result == null) return this.NotFound();
        return this.Ok(result);
    }

    public IHttpActionResult GetBoundFunctionValue()
    {
        return this.Ok(this._repository.GetAccount());
    }

    Repository _repository;
}

public class ControllerResolver : DefaultHttpControllerTypeResolver
{
    public override ICollection<Type> GetControllerTypes(IAssembliesResolver assembliesResolver)
    {
        var types = Assembly.GetExecutingAssembly().GetExportedTypes();
        var controllerType = typeof(System.Web.Http.Controllers.IHttpController);
        var list = types.Where(i => controllerType.IsAssignableFrom(i)).ToList();
        list.Add(typeof(MetadataController));
        list.Dump(this.GetType().Name);
        return list;
    }
}

public class Startup
{
    public void Configuration(IAppBuilder appBuilder)
    {
        var config = new HttpConfiguration();

        config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
        config.Services.Replace(typeof(IHttpControllerTypeResolver), new ControllerResolver());

        var builder = new ODataConventionModelBuilder { Namespace = "Edm" };

        var entitySetName = nameof(Client);

        builder.EntitySet<Client>(entitySetName);

        builder
            .EntityType<Account>()
            .Count()
            .Filter(QueryOptionSetting.Allowed);

        var f1 = builder.EntityType<Client>().Collection
            .Function(nameof(ClientController.GetAccount))
            .ReturnsFromEntitySet<Account>($"{entitySetName}_1");
        f1.Parameter<int>("clientId");
        f1.Parameter<int>("accountId");

        builder.EntityType<Client>().Collection
            .Function(nameof(ClientController.GetBoundFunctionValue))
            .ReturnsFromEntitySet<Account>($"{entitySetName}_2"); //see https://stackoverflow.com/a/34881602/22944

        var model = builder.GetEdmModel();

        config.MapODataServiceRoute(routeName: "ODataRoute", routePrefix: "odata", model: model);

        appBuilder.UseWebApi(config);

        config.EnsureInitialized();
    }
}