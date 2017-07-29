using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EveSharpInterface.Util
{
  public static class ESIClient
  {
    public static HttpClient client { get; }
    static ESIClient()
    {
      client = new HttpClient();
    }

  }
}
