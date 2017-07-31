using EveSharpInterface.Enums;
using EveSharpInterface.Operations;
using EveSharpInterface.SSO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EveSharpInterface.Util
{
  public static class ESIConnection
  {

    public static HttpRequestMessage getRequest(IOperation operation, Server server, OAuth authentication = null, string version = "latest")
    {
      string hostname = @"https://esi.tech.ccp.is/" + version;
      string datasource = "";

      switch(server)
      {
        case Server.Tranquility:
          datasource = "?datasource=tranquility";
          break;
        case Server.Singularity:
          datasource = "?datasource=singularity";
          break;
      }

      string Url = hostname + operation.GetEndpoint() + datasource;

      HttpRequestMessage request = new HttpRequestMessage
      {
        RequestUri = new Uri(Url),
        Method = operation.Verb,
      };

      if(authentication != null)
      {
        request.Headers.Add("Authorization", $"Bearer {authentication.AccessToken}");
      }

      return request;
    }
  }
}
