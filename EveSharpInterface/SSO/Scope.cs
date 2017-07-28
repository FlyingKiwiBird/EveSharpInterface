using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveSharpInterface.SSO
{
  public class Scope : IEquatable<Scope>
  {
    internal Scope(string identifier)
    {
      Identifier = identifier;
    }

    public string Identifier { get; }

    public bool Equals(Scope other)
    {
      return other.Identifier == Identifier;
    }
  }
}
