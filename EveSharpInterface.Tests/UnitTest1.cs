using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EveSharpInterface.SSO;
using EveSharpInterface.SSO.Scopes;


namespace EveSharpInterfaceTests
{
  [TestClass]
  public class SSO
  {
    bool isauth = false;
    bool callback = false;
    [TestMethod]
    public void TestSignOn()
    {
      callback = false;
      isauth = false;
      Auth a = new Auth();
      a.Authenticate(@"http://localhost:8080/", "db4952f062d94203aa8a69cfdae4f5b2");
      a.onAuthorizationSuccess += A_onAuthorization;
      a.onAuthorizationFailure += A_onAuthorizationFailure;
      while(!callback)
      { }
      Assert.IsTrue(isauth);
    }

    [TestMethod]
    public void TestSignOnWithScope()
    {
      callback = false;
      isauth = false;
      Auth a = new Auth();
      ScopeCollection sc = new ScopeCollection();
      sc.Add(Fittings.Read);
      a.Authenticate(@"http://localhost:8080/", "db4952f062d94203aa8a69cfdae4f5b2", sc);
      a.onAuthorizationSuccess += A_onAuthorization;
      a.onAuthorizationFailure += A_onAuthorizationFailure;
      while (!callback)
      { }
      Assert.IsTrue(isauth);
    }

      private void A_onAuthorizationFailure(Auth e)
    {
      callback = true;
      isauth = false;
    }

    private void A_onAuthorization(Auth e)
    {
      callback = true;
      isauth = true;
    }

  }
}
