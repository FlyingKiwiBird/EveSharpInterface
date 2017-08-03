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
    public OAuth Authentication
    {
      get
      {
        return _authentication;
      }

      protected set
      {
        if (value.HasScope(RequiredScope))
        {
          _authentication = value;
        }
        else
        {
          throw new ScopeRequiredException(RequiredScope);
        }
      }
    }

    private OAuth _authentication;
    protected JContainer _results;

    public virtual Scope RequiredScope => throw new NotImplementedException();
  
    public virtual HttpMethod Verb => throw new NotImplementedException();


    public virtual async Task<JContainer> ExecuteAsync()
    {
      HttpRequestMessage request = ESIConnection.getRequest(this, _authentication.EveServer, _authentication);

      var results = await GetRawResults(request);

      _results = JParser.parse(results);

      return _results;
    }

    internal virtual async Task<string> GetRawResults(HttpRequestMessage request)
    {
      HttpClient client = ESIClient.client;
      var clientResponse = await client.SendAsync(request);
      return await clientResponse.Content.ReadAsStringAsync();
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
