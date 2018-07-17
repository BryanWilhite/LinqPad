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
  <Namespace>Microsoft.AspNet.OData.Routing</Namespace>
  <Namespace>Microsoft.AspNet.OData</Namespace>
  <Namespace>Microsoft.Owin.Hosting</Namespace>
  <Namespace>Owin</Namespace>
  <Namespace>System.ComponentModel.DataAnnotations</Namespace>
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
            callOwin("odata/ClientAccount(ClientId=905,AccountId=95)"); //should return Not Found
            callOwin("odata/ClientAccount(ClientId=5,AccountId=95)");
        }
    }
    finally
    {
        client.Dispose();
    }
}

public class ClientAccount
{
    [Key]
    public int ClientId { get; set; }
    [Key]
    public int AccountId { get; set; }
    public string Notes { get; set; }
}

public class Repository
{
    static Repository()
    {
        clientAccounts = new[] {
            new ClientAccount { ClientId = 1, AccountId = 31 },
            new ClientAccount { ClientId = 2, AccountId = 62 },
            new ClientAccount { ClientId = 3, AccountId = 73 },
            new ClientAccount { ClientId = 4, AccountId = 84 },
            new ClientAccount { ClientId = 5, AccountId = 95 },
            new ClientAccount { ClientId = 6, AccountId = 36 },
            new ClientAccount { ClientId = 7, AccountId = 47 },
        };
    }

    public ClientAccount GetClientAccount(int clientId, int accountId)
    {
        var data = clientAccounts
            .SingleOrDefault(i => (i.ClientId == clientId) && (i.AccountId == accountId));

        return data;
    }

    static IEnumerable<ClientAccount> clientAccounts;
}

[ODataRoutePrefix(nameof(ClientAccount))]
public class ClientAccountController : ODataController
{
    public ClientAccountController()
    {
        this._repository = new Repository();
        this._repository.Dump("controller and repository loaded");
    }

    [EnableQuery]
    [ODataRoute("(ClientId={clientId},AccountId={accountId})")]
    public IHttpActionResult Get(int clientId, int accountId)
    {
        var result = this._repository.GetClientAccount(clientId, accountId);
        if(result == null) return this.NotFound();
        return this.Ok(result);
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
        builder.EntitySet<ClientAccount>(typeof(ClientAccount).Name);

        var model = builder.GetEdmModel();

        config.MapODataServiceRoute(routeName: "ODataRoute", routePrefix: "odata", model: model);

        appBuilder.UseWebApi(config);

        config.EnsureInitialized();
    }
}