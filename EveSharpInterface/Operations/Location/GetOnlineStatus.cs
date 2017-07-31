using EveSharpInterface.SSO;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace EveSharpInterface.Operations.Location
{
  public class GetOnlineStatus : BaseAuthenticatedOperation
  {
    private string _characterId;

    public override Scope RequiredScope => Scopes.Location.ReadOnline;
    public override HttpMethod Verb => HttpMethod.Get;


    public GetOnlineStatus(OAuth authorization, string characterId)
    {
      _authorization = authorization;
      _characterId = characterId;
    }

    public override async Task<JContainer> ExecuteAsync()
    {
      var results = await base.GetRawResults();
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
