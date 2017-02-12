<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.Internals.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\SMDiagnostics.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.IdentityModel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.DirectoryServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.Transactions.Bridge.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.Services.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.EnterpriseServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.IdentityModel.Selectors.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.ApplicationServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Messaging.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.DurableInstancing.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceProcess.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.Activation.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>Songhay.Net.HttpWebRequest</NuGetReference>
  <NuGetReference>SonghayCore</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Songhay</Namespace>
  <Namespace>Songhay.Extensions</Namespace>
  <Namespace>Songhay.Models</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

void Main()
{
    /*
        Climate Data Online: Web Services Documentation
        https://www.ncdc.noaa.gov/cdo-web/webservices/
    */

    var apiMetadataJsonFile = Path.Combine(
        Util.CurrentQuery.GetLinqPadDirectoryInfo().FullName,
        @"Content\json\noaaApiMeta.json");

    var apiMetadataJson = File.ReadAllText(apiMetadataJsonFile);
    var apiMetadata = JsonConvert.DeserializeObject<RestApiMetadata>(apiMetadataJson);
    apiMetadata.ApiKey = Util.CurrentQuery.GetLinqPadMetaSecret("noaa", "apiKey");
    apiMetadata.Dump("API Meta");

    var uri = apiMetadata.ToUriForLocationsUnitedStates();
    var responseString = (WebRequest.Create(uri) as HttpWebRequest)
        .WithCredentials(apiMetadata)
        .DownloadToString();
    responseString.Dump("response");

    var jO = JObject.Parse(responseString);
    var location = jO.ToJTokenByName("California");
    var stateId = location["id"].Value<string>();
    stateId.Dump("state ID");

    //El Segundo: 33.9192° N, 118.4165° W
    //Santa Monica: 34.0195° N, 118.4912° W
    var extent = "33.8923276,-118.56606,34.1809267,-118.1006187"; //eyeballed from Google Maps!
    uri = apiMetadata.ToUriForStationsByLocationId(stateId, extent);
    responseString = (WebRequest.Create(uri) as HttpWebRequest)
        .WithCredentials(apiMetadata)
        .DownloadToString();
    responseString.Dump("response");

    jO = JObject.Parse(responseString);
    var station = jO.ToJTokenByName("LOS ANGELES INTERNATIONAL AIRPORT, CA US");
    var stationId = station["id"].Value<string>();
    stationId.Dump("station ID");

    //useful categories: TEMP, PRCP, SUN, WIND
    uri = apiMetadata.ToUriForDataTypesByCategory("TEMP");
    responseString = (WebRequest.Create(uri) as HttpWebRequest)
        .WithCredentials(apiMetadata)
        .DownloadToString();
    responseString.Dump("response");

    var datatypeIds = new[] { "TOBS", "TMIN", "TMAX", "TAVG", "HTMX", "LTMN", "HPCP", "PSUN", "AWND", "WDFG" };
    var startDate = "2016-12-09"; //YYYY-MM-DDThh:mm:ss
    var endDate = "2016-12-10";

    uri = apiMetadata.ToUriForDatasetsByDataTypes(datatypeIds);
    responseString = (WebRequest.Create(uri) as HttpWebRequest)
        .WithCredentials(apiMetadata)
        .DownloadToString();
    responseString.Dump("response");

    var datasetId = "GHCND";

    //uri = apiMetadata.ToUriForData(datasetId, stationId, datatypeIds, startDate, endDate);
    uri = new Uri("https://www.ncdc.noaa.gov/cdo-web/api/v2/data?datasetid=GHCND&stationid=COOP:045114&startdate=2010-05-01&enddate=2010-05-31", UriKind.Absolute);
    responseString = (WebRequest.Create(uri) as HttpWebRequest)
        .WithCredentials(apiMetadata)
        .DownloadToString();
    responseString.Dump("response");
}

static class HttpWebRequestExtensions
{
    public static HttpWebRequest WithCredentials(this HttpWebRequest request, RestApiMetadata metadata)
    {
        if (request == null) return null;
        if (metadata == null) throw new ArgumentNullException("metadata", "The expected API metadata is not here.");

        request.Headers.Add("token", metadata.ApiKey);

        return request;
    }
}

static class JObjectExtensions
{
    public static JToken ToJTokenByName(this JObject jObject, string constraint)
    {
        if (jObject == null) return null;
        var jA = jObject.GetJArray("results", throwException: true);
        var single = jA.First(i => i["name"].Value<string>() == constraint);
        return single;
    }
}

static class RestApiMetadataExtensions
{
    public static Uri ToUri(this RestApiMetadata metadata, string key)
    {
        var uri = new Uri(string.Concat(metadata.ApiBase,
            metadata.UriTemplates[key]),
            UriKind.Absolute);

        uri.Dump($"{key} URI");

        return uri;
    }

    public static Uri ToUriForData(this RestApiMetadata metadata, string datasetId, string stationId, string[] datatypeIds, string startDate, string endDate)
    {
        if (string.IsNullOrEmpty(datasetId)) throw new ArgumentNullException("datasetId", "The expected dataset ID is not here.");
        if (string.IsNullOrEmpty(stationId)) throw new ArgumentNullException("stationId", "The expected station ID is not here.");
        if (datatypeIds == null) throw new ArgumentNullException("datatypeIds", "The expected data-type IDs are not here.");
        if (!datatypeIds.Any()) throw new ArgumentNullException("datatypeIds", "The expected data-type IDs are not here.");
        if (string.IsNullOrEmpty(startDate)) throw new ArgumentNullException("startDate", "The expected start date is not here.");
        if (string.IsNullOrEmpty(endDate)) throw new ArgumentNullException("endDate", "The expected end date is not here.");

        var key = "Data";
        var ids = string.Join("&", datatypeIds);
        var uri = metadata.ToUriTemplate(key)
            .BindByPosition(metadata.ApiBase, datasetId, stationId, ids, startDate, endDate)
            .WithoutEscaping("%26", "&"); //CONVENTION: NOAA expects literal ampersands!

        uri.Dump($"{key} URI");

        return uri;
    }

    public static Uri ToUriForDataTypesByCategory(this RestApiMetadata metadata, string categoryId)
    {
        //see: https://www.ncdc.noaa.gov/cdo-web/webservices/v2#dataCategories
        if (string.IsNullOrEmpty(categoryId)) throw new ArgumentNullException("categoryId", "The expected category ID is not here.");

        var key = "DataTypesByCategory";
        var uri = metadata.ToUriTemplate(key).BindByPosition(metadata.ApiBase, categoryId);

        uri.Dump($"{key} URI");

        return uri;
    }

    public static Uri ToUriForDatasetsByDataTypes(this RestApiMetadata metadata, string[] datatypeIds)
    {
        if (datatypeIds == null) throw new ArgumentNullException("datatypeIds", "The expected data-type IDs are not here.");
        if (!datatypeIds.Any()) throw new ArgumentNullException("datatypeIds", "The expected data-type IDs are not here.");

        var key = "DatasetsByDataTypes";
        var ids = string.Join("&", datatypeIds);
        var uri = metadata.ToUriTemplate(key)
            .BindByPosition(metadata.ApiBase, ids)
            .WithoutEscaping("%26", "&"); //CONVENTION: NOAA expects literal ampersands!

        uri.Dump($"{key} URI");

        return uri;
    }

    public static Uri ToUriForStationsByLocationId(this RestApiMetadata metadata, string locationId, string extent)
    {
        if (string.IsNullOrEmpty(locationId)) throw new ArgumentNullException("locationId", "The expected location ID is not here.");
        if (string.IsNullOrEmpty(extent)) throw new ArgumentNullException("extent", "The expected geographic extent is not here.");

        var key = "StationsByLocationId";
        var uri = metadata.ToUriTemplate(key).BindByPosition(metadata.ApiBase, locationId, extent);

        uri.Dump($"{key} URI");

        return uri;
    }

    public static Uri ToUriForLocationsUnitedStates(this RestApiMetadata metadata)
    {
        var key = "Locations";
        var uri = metadata.ToUri(key);
        uri = new Uri(string.Concat(uri.OriginalString, "?locationcategoryid=ST&limit=52"), UriKind.Absolute);
        return uri;
    }

    public static UriTemplate ToUriTemplate(this RestApiMetadata metadata, string key)
    {
        if (metadata == null) throw new ArgumentNullException("metadata", "The expected REST API metadata is not here.");
        if (string.IsNullOrEmpty(key)) throw new ArgumentNullException("key", "The expected REST API metadata key is not here.");

        var template = metadata.UriTemplates[key];

        return new UriTemplate(template);
    }
}

static class UriExtensions
{
    public static Uri WithoutEscaping(this Uri uri, string escapedValue, string unescapedValue)
    {
        if (uri == null) throw new ArgumentNullException("uri", "The expected URI is not here.");
        if (string.IsNullOrEmpty(escapedValue)) throw new ArgumentNullException("escapedValue", "The expected escaped value is not here.");
        if (string.IsNullOrEmpty(unescapedValue)) throw new ArgumentNullException("unescapedValue", "The expected un-escaped value is not here.");
        
        return new Uri(uri.OriginalString.Replace(escapedValue, unescapedValue), UriKind.Absolute);
    }
}