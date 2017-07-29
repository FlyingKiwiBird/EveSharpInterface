using EveSharpInterface.SSO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveSharpInterface.Operations
{
  class ScopeRequiredException : Exception
  {
    public Scope RequiredScope {get; private set;}
    public ScopeRequiredException(Scope required) : base(getMessage(required))
    {
      RequiredScope = required;
    }

    private static string getMessage(Scope s)
    {
      return $"The following scope is required but was not found: {s.Identifier}";
    }
  }
}
