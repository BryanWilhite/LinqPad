<Query Kind="Program">
  <NuGetReference>Microsoft.Azure.KeyVault</NuGetReference>
  <NuGetReference>Microsoft.IdentityModel.Clients.ActiveDirectory</NuGetReference>
  <NuGetReference>Microsoft.Net.Http</NuGetReference>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>Microsoft.IdentityModel.Clients.ActiveDirectory</Namespace>
  <Namespace>Microsoft.Azure.KeyVault</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

/*
    https://stackoverflow.com/questions/36896703/how-do-i-access-azure-key-vault-using-user-credentials
    https://github.com/Azure/azure-sdk-for-net/tree/master/src/KeyVault/Microsoft.Azure.KeyVault.Samples
*/
void Main()
{
    var keyVaultName = Util.CurrentQuery.GetLinqPadMetaSecret("azure", "key-vault-name");
    var secretName = "noaa";
    var authCallback = new KeyVaultClient.AuthenticationCallback(GetAccessToken);
    var client = new KeyVaultClient(authCallback);
    var secret = client.GetSecretAsync(keyVaultName, secretName).Result;
    secret.Dump();
}

static async Task<string> GetAccessToken(string authority, string resource, string scope)
{
    var clientId = Util.CurrentQuery.GetLinqPadMetaSecret("azure", "key-vault-ad-app-id");
    var adUser = Util.CurrentQuery.GetLinqPadMetaSecret("azure", "ad-user");
    var adPassword = Util.CurrentQuery.GetLinqPadMetaSecret("azure", "ad-password");

    var user = new UserPasswordCredential(adUser, adPassword);
    var context = new AuthenticationContext(authority, TokenCache.DefaultShared);
    var authResult = await context.AcquireTokenAsync(resource, clientId, user);
    return authResult.AccessToken;
}
