using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveSharpInterface.SSO
{
  public class ScopeCollection
  {
    private List<Scope> _scopes = new List<Scope>();
    public string GetScopeString()
    {
      string scopeString = "";
      foreach (var scope in _scopes)
      {
        scopeString += scope.Identifier + ",";
      }
      scopeString = scopeString.TrimEnd(',');
      return scopeString;
    }

    public void Add(Scope scope)
    {
      _scopes.Add(scope);
    }

    public void Remove(Scope scope)
    {
      _scopes.Remove(scope);
    }

    public List<Scope> GetScopes()
    {
      return _scopes;
    }

    public bool HasScope(Scope scope)
    {
      return _scopes.Contains(scope);
    }
  }
}
