using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveSharpInterface.SSO.Scopes
{
  public static class Fittings
  {
    public static Scope Read => new Scope("esi-fittings.read_fittings.v1");
    public static Scope Write => new Scope("esi-fittings.write_fittings.v1");
  }
}
