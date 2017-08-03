using EveSharpInterface.SSO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace EveSharpInterface.Operations.Location
{
  public class GetCurrentShip : BaseAuthenticatedOperation
  {
    private string _characterId;
    public GetCurrentShip(OAuth authentication, string characterId)
    {
      Authentication = authentication;
      _characterId = characterId;
    }

    public override Scope RequiredScope => Scopes.Location.ReadShip;
    public override HttpMethod Verb => HttpMethod.Get;
    public override string GetEndpoint()
    {
      return $"/characters/{_characterId}/ship/";
    }
  }
}
