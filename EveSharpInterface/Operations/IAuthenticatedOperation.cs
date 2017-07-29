using EveSharpInterface.SSO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveSharpInterface.Operations
{
  public interface IAuthenticatedOperation : IOperation
  {
    Scope RequiredScope { get; }
    Auth GetAuthorization();
  }
}
