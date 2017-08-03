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
  public class GetCharacterFittings : BaseAuthenticatedOperation
  {
    private string _characterId;

    public override Scope RequiredScope => Scopes.Fittings.Read;
    public override HttpMethod Verb => HttpMethod.Get;


    public GetCharacterFittings(OAuth authentication, string characterId)
    {
      Authentication = authentication;
      _characterId = characterId;
    }

    public override string GetEndpoint()
    {
      return $"/characters/{_characterId}/fittings/";
    }
  }
}
