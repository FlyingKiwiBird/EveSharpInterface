using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EveSharpInterface.Enums;
using EveSharpInterface.SSO;
using EveSharpInterface.Util;
using Newtonsoft.Json.Linq;

namespace EveSharpInterface.Operations.Fittings
{
  public class GetCharacterFittings : IAuthenticatedOperation
  {
    public Scope RequiredScope => SSO.Scopes.Fittings.Read;


    HttpMethod IOperation.Verb => HttpMethod.Get;


    private string _characterID;
    private Auth _authorization;

    public GetCharacterFittings(Auth user, string characterId)
    {
      if (user.HasScope(RequiredScope))
      {
        _authorization = user;
      }
      else
      {
        throw new ScopeRequiredException(RequiredScope);
      }
      _characterID = characterId;
    }


    public async Task<dynamic> ExecuteAsync()
    {
      HttpRequestMessage request = ESIConnection.getRequest(this, _authorization.EveServer, _authorization);
      HttpClient client = ESIClient.client;
      var clientResponse = await client.SendAsync(request);
      var jsonResponse = await clientResponse.Content.ReadAsStringAsync();
      dynamic json = JToken.Parse(jsonResponse);
      
      return json;
    }

    public string GetEndpoint()
    {
      return $"/characters/{_characterID}/fittings/";
    }

    public Auth GetAuthorization()
    {
      return _authorization;
    }
  }
}
