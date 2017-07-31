using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EveSharpInterface.SSO;
using EveSharpInterface.Scopes;


namespace EveSharpInterfaceTests
{
  [TestClass]
  public class SSOTests
  {
    [TestMethod]
    public void TestSignOn()
    {
      OAuth a = new OAuth();
      var task = a.Authenticate(@"http://localhost:8080/", "db4952f062d94203aa8a69cfdae4f5b2", "riK2p2t4fkl4AzrywIzbMHi528g2Vm04C30OrWPZ");
      task.Wait();
      Assert.IsTrue(task.Result);
    }

    [TestMethod]
    public void TestSignOnWithScope()
    {
      OAuth a = new OAuth();
      ScopeCollection sc = new ScopeCollection();
      sc.Add(Fittings.Read);
      var task = a.Authenticate(@"http://localhost:8080/", "db4952f062d94203aa8a69cfdae4f5b2", "riK2p2t4fkl4AzrywIzbMHi528g2Vm04C30OrWPZ", sc);
      task.Wait();
      Assert.IsTrue(task.Result);
    }

  }
}
