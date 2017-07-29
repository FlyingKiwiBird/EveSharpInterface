using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EveSharpInterface.SSO;
using EveSharpInterface.SSO.Scopes;
using EveSharpInterface.Operations.Fittings;
using Newtonsoft.Json.Linq;

namespace EveSharpInterface.Tests
{
 
  [TestClass]
  public class FittingsTest
  {
    private Auth _authorization;
    [TestInitialize]
    public void Setup()
    {
      _authorization = new Auth();
      ScopeCollection sc = new ScopeCollection();
      sc.Add(Fittings.Read);
      sc.Add(Fittings.Write);
      var task = _authorization.Authenticate(@"http://localhost:8080/", "db4952f062d94203aa8a69cfdae4f5b2", "riK2p2t4fkl4AzrywIzbMHi528g2Vm04C30OrWPZ", sc);
      task.Wait();
    }

    [TestMethod]
    public void ReadFittingsTest()
    {
      var charIdTask = _authorization.GetAuthCharacterId();
      charIdTask.Wait(); 
      GetCharacterFittings fitting = new GetCharacterFittings(_authorization, charIdTask.Result);
      var fittingTask = fitting.ExecuteAsync();
      fittingTask.Wait();
      JContainer json = fittingTask.Result;
      string name = (string)json[0]["name"];
      Assert.IsNotNull(name);
    }
  }
}
