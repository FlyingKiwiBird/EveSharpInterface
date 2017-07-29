using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveSharpInterface.Util
{
  public static class JParser
  {
    public static JContainer parse(string json)
    {
      try
      {
        dynamic jsonArray = JArray.Parse(json);
        return jsonArray as JContainer;
      }
      catch { }
      try
      {
        dynamic jsonObj = JObject.Parse(json);
        return jsonObj as JContainer;
      }
      catch { }

      return null;
    }
  }
}
