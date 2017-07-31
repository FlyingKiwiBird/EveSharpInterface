using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveSharpInterface.SSO;

namespace EveSharpInterface.Scopes
{
  public static class Location
  {
    public static Scope ReadLocation => new Scope("esi-location.read_location.v1");
    public static Scope ReadOnline => new Scope("esi-location.read_online.v1");
    public static Scope ReadShip => new Scope("esi-location.read_ship_type.v1");
  }
}
