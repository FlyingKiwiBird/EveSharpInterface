using EveSharpInterface.SSO;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using EveSharpInterface.Util;

namespace EveSharpInterface.Operations.Location
{
  public class GetCharacterOnline : BaseAuthenticatedOperation
  {
    private string _characterId;

    public override Scope RequiredScope => Scopes.Location.ReadOnline;
    public override HttpMethod Verb => HttpMethod.Get;


    public GetCharacterOnline(OAuth authentication, string characterId)
    {
      Authentication = authentication;
      _characterId = characterId;
    }

    public override async Task<JContainer> ExecuteAsync()
    {
      HttpRequestMessage request = ESIConnection.getRequest(this, Authentication.EveServer, Authentication);
      var results = await base.GetRawResults(request);
      JProperty isOnline = new JProperty("online", results);
      JObject onlineObject = new JObject();
      onlineObject.Add(isOnline);
      return onlineObject;
    }


    public override string GetEndpoint()
    {
      return $"/characters/{_characterId}/online/";
    }
  }
}
