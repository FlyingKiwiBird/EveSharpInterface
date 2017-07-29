using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EveSharpInterface.SSO;
using EveSharpInterface.SSO.Scopes;


namespace EveSharpInterfaceTests
{
  [TestClass]
  public class SSOTests
  {
    [TestMethod]
    public void TestSignOn()
    {
      Auth a = new Auth();
      var task = a.Authenticate(@"http://localhost:8080/", "db4952f062d94203aa8a69cfdae4f5b2", "riK2p2t4fkl4AzrywIzbMHi528g2Vm04C30OrWPZ");
      task.Wait();
      Assert.IsTrue(task.Result);
    }

    [TestMethod]
    public void TestSignOnWithScope()
    {
      Auth a = new Auth();
      ScopeCollection sc = new ScopeCollection();
      sc.Add(Fittings.Read);
      var task = a.Authenticate(@"http://localhost:8080/", "db4952f062d94203aa8a69cfdae4f5b2", "riK2p2t4fkl4AzrywIzbMHi528g2Vm04C30OrWPZ", sc);
      task.Wait();
      Assert.IsTrue(task.Result);
    }

  }
}
