<Query Kind="Program">
  <Connection>
    <ID>cd884946-dbf0-42d1-ac90-d3a615f50162</ID>
    <Driver>AstoriaAuto</Driver>
    <Server>http://services.odata.org/AdventureWorksV3/AdventureWorks.svc</Server>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <NuGetReference>EntityFramework</NuGetReference>
  <NuGetReference>Microsoft.AspNet.OData</NuGetReference>
  <NuGetReference>Microsoft.AspNet.WebApi.Client</NuGetReference>
  <NuGetReference>Microsoft.AspNet.WebApi.Owin</NuGetReference>
  <NuGetReference>Microsoft.Net.Http</NuGetReference>
  <NuGetReference>Microsoft.Owin</NuGetReference>
  <NuGetReference>Microsoft.Owin.SelfHost</NuGetReference>
  <NuGetReference Prerelease="true">morelinq</NuGetReference>
  <NuGetReference>Owin</NuGetReference>
  <Namespace>Microsoft.Owin.Hosting</Namespace>
  <Namespace>MoreLinq</Namespace>
  <Namespace>Owin</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Web.Http</Namespace>
  <Namespace>System.Web.Http.Dispatcher</Namespace>
  <Namespace>System.Web.OData</Namespace>
  <Namespace>System.Web.OData.Builder</Namespace>
  <Namespace>System.Web.OData.Extensions</Namespace>
  <Namespace>System.Web.OData.Query</Namespace>
  <Namespace>System.Web.OData.Routing</Namespace>
  <Namespace>System.ComponentModel.DataAnnotations.Schema</Namespace>
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

            callOwin("api/$metadata");
            callOwin("api/Client(101)/Accounts");
            //callOwin("api/Client(101)/Accounts(4)");
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
    public IEnumerable<Account> Accounts { get; set; }
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
        clients = new[]
        {
            new Client
            {
                ClientId = 100,
                Name = "Ms. One",
                Accounts = new []
                {
                    new Account { AccountId = 1, AccountName = "Account One" },
                    new Account { AccountId = 2, AccountName = "Account Two" },
                }
            },
            new Client
            {
                ClientId = 101,
                Name = "Mr. Two",
                Accounts = new []
                {
                    new Account { AccountId = 3, AccountName = "Account Three" },
                    new Account { AccountId = 4, AccountName = "Account Four" },
                    new Account { AccountId = 5, AccountName = "Account Five" },
                }
            },
            new Client
            {
                ClientId = 102,
                Name = "Mrs. Three",
                Accounts = new []
                {
                    new Account { AccountId = 6, AccountName = "Account Six" },
                    new Account { AccountId = 7, AccountName = "Account Seven" },
                }
            },
        };
    }

    public IQueryable<Account> GetAccountsByClient(int clientId)
    {
        var data = clients
            .Where(i => i.ClientId ==  clientId)
            .SelectMany(i => i.Accounts)
            .ToArray();

        return data.AsQueryable();
    }

    static IEnumerable<Client> clients;
}

public class ClientController : ODataController
{
    public ClientController()
    {
        this._repository = new Repository();
        this._repository.Dump("controller and repository loaded");
    }

    [EnableQuery(PageSize = 3, AllowedQueryOptions = AllowedQueryOptions.All)]
    public IQueryable<Account> GetAccounts([FromODataUri] int key)
    {
        return this._repository.GetAccountsByClient(key);
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

        var builder = new ODataConventionModelBuilder();
        builder.EntitySet<Account>(nameof(Account));
        builder.EntitySet<Client>(nameof(Client));

        var model = builder.GetEdmModel();

        config.MapODataServiceRoute(routeName: "odata", routePrefix: "api", model: model);

        appBuilder.UseWebApi(config);

        config.EnsureInitialized();
    }
}