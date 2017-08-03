using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EveSharpInterface.SSO;
using Newtonsoft.Json.Linq;
using EveSharpInterface.Util;

namespace EveSharpInterface.Operations.Location
{
  public class GetCharacterLocation : BaseAuthenticatedOperation
  {
    private string _characterId;

    public override Scope RequiredScope => Scopes.Location.ReadLocation;
    public override HttpMethod Verb => HttpMethod.Get;


    public GetCharacterLocation(OAuth authentication, string characterId)
    {
      Authentication = authentication;
      _characterId = characterId;
    }


    public override string GetEndpoint()
    {
      return $"/characters/{_characterId}/location/";
    }
  }
}
