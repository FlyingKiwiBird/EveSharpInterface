using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EveSharpInterface.SSO;
using Newtonsoft.Json.Linq;
using EveSharpInterface.Util;

namespace EveSharpInterface.Operations
{
  public class BaseAuthenticatedOperation : IAuthenticatedOperation
  {
    internal OAuth _authorization;
    internal JContainer _results;

    public virtual Scope RequiredScope => throw new NotImplementedException();
  
    public virtual HttpMethod Verb => throw new NotImplementedException();


    public virtual async Task<JContainer> ExecuteAsync()
    {
      var results = await GetRawResults();

      _results = JParser.parse(results);

      return _results;
    }

    internal virtual async Task<string> GetRawResults()
    {
      HttpRequestMessage request = ESIConnection.getRequest(this, _authorization.EveServer, _authorization);
      HttpClient client = ESIClient.client;
      var clientResponse = await client.SendAsync(request);
      return await clientResponse.Content.ReadAsStringAsync();
    }


    public virtual OAuth GetAuthorization()
    {
      return _authorization;
    }

    public virtual JContainer GetCachedResults()
    {
      return _results;
    }

    public virtual string GetEndpoint()
    {
      throw new NotImplementedException();
    }
  }
}
