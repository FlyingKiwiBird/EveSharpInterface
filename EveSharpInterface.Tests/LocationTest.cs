using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EveSharpInterface.SSO;
using EveSharpInterface.Scopes;
using EveSharpInterface.Operations.Location;
using Newtonsoft.Json.Linq;

namespace EveSharpInterface.Tests
{
  [TestClass]
  public class LocationTest
  {
    private static OAuth _authorization;
    private static string _character;
    [ClassInitialize]
    public static void Setup(TestContext context)
    {
      _authorization = new OAuth();
      ScopeCollection sc = new ScopeCollection();
      sc.Add(Location.ReadLocation);
      sc.Add(Location.ReadOnline);
      sc.Add(Location.ReadShip);
      var task = _authorization.Authenticate(@"http://localhost:8080/", "db4952f062d94203aa8a69cfdae4f5b2", "riK2p2t4fkl4AzrywIzbMHi528g2Vm04C30OrWPZ", sc);
      task.Wait();
      var characterTask = _authorization.GetAuthCharacterId();
      characterTask.Wait();
      _character = characterTask.Result;
    }

    [TestMethod]
    public void GetLocationTest()
    {
      GetCharacterLocation gcl = new GetCharacterLocation(_authorization, _character);
      var locationTask = gcl.ExecuteAsync();
      locationTask.Wait();
      var result = locationTask.Result;

      Assert.IsNotNull(result["solar_system_id"]);
    }

    [TestMethod]
    public void GetOnlineTest()
    {
      GetCharacterOnline online = new GetCharacterOnline(_authorization, _character);
      var locationTask = online.ExecuteAsync();
      locationTask.Wait();
      var result = locationTask.Result;

      Assert.IsNotNull(result["online"]);
    }

    [TestMethod]
    public void GetShipTest()
    {
      GetCurrentShip ship = new GetCurrentShip(_authorization, _character);
      var shipTask = ship.ExecuteAsync();
      shipTask.Wait();
      var result = shipTask.Result;

      Assert.IsNotNull(result["ship_type_id"]);
    }
  }
}

