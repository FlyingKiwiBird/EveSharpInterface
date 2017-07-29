using EveSharpInterface.SSO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static EveSharpInterface.Operations.OperationEvents;
using EveSharpInterface.Enums;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace EveSharpInterface
{
  public interface IOperation
  {
    HttpMethod Verb { get; }

    Task<JContainer> ExecuteAsync();
    string GetEndpoint();
  }
}
