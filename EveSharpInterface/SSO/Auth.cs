using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveSharpInterface.Enums;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Net.Http;
using EveSharpInterface.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EveSharpInterface.SSO
{
  public class Auth
  {

    private HttpListener _authServer = new HttpListener();
    private bool isAuthorized = false;
    private string _state;
    private ScopeCollection _scopes;

    private string _clientID;
    private string _clientSecret;
    private string _characterId;

    public delegate void AuthorizationHandler(Auth e);
    public event AuthorizationHandler onAuthorizationSuccess;
    public event AuthorizationHandler onAuthorizationFailure;

    public string AuthorizationCode { get; private set; }
    public string AccessToken { get; private set; }
    public string RefreshToken { get; private set; }
    public Server EveServer { get; private set; }
    public string Base64Login { get; private set; }

    public Auth()
    {
      EveServer = Server.Tranquility;
    }

    public async Task<bool> Authenticate(string callbackURL, string clientID, string clientSecret, Server server = Server.Tranquility)
    {
      return await Authenticate(callbackURL, clientID, clientSecret, new ScopeCollection(), server);
    }

    public async Task<bool> Authenticate(string callbackURL, string clientID, string clientSecret, ScopeCollection scopes, Server server = Server.Tranquility)
    {

      isAuthorized = false;

      EveServer = server;
      _clientID = clientID;
      _clientSecret = clientSecret;

      var textBytes = Encoding.UTF8.GetBytes($"{_clientID}:{_clientSecret}");
      Base64Login = Convert.ToBase64String(textBytes);

      string loginURL = "";
      _state = "ESI_";
      Random r = new Random();
      _state += r.Next(10000, 99999).ToString();
      switch (server)
      {
        case Server.Tranquility:
          loginURL = @"https://login.eveonline.com/oauth/authorize/";
          break;
      }

      loginURL += @"?response_type=code&redirect_uri=";
      string callbackURL_encoded = Uri.EscapeDataString(callbackURL);
      loginURL += callbackURL_encoded;
      loginURL += "&client_id=" + clientID;

      _scopes = scopes;
      string scopeStr = scopes.GetScopeString();
      if (!string.IsNullOrEmpty(scopeStr))
      {
        loginURL += "&scope=" + scopeStr;
      }

      loginURL += "&state=" + _state;

      _authServer.Prefixes.Add(callbackURL);
      _authServer.Start();
      Process.Start(loginURL);
      var result = await Task.Factory.FromAsync((callback, state) => _authServer.BeginGetContext(callback, state), _authServer.EndGetContext, null);
      return await GetAuthorization(result);      
    }

    public async Task<string> GetAuthCharacterId()
    {
      if (!string.IsNullOrEmpty(_characterId))
      {
        return _characterId;
      }
      var request = new HttpRequestMessage()
      {
        RequestUri = new Uri(@"https://login.eveonline.com/oauth/verify"),
        Method = HttpMethod.Get,
      };
      request.Headers.Add("Authorization", $"Bearer {AccessToken}");
      HttpClient client = ESIClient.client;
      var tokenResult = await client.SendAsync(request);
      var responseResult = await tokenResult.Content.ReadAsStringAsync();

      dynamic jsonResult = JObject.Parse(responseResult);
      _characterId = jsonResult.CharacterID;
      return _characterId;

    }

    public bool HasScope(Scope s)
    {
      return _scopes.HasScope(s);
    }

    private async Task<bool> GetTokens(string authCode)
    {
      var values = new Dictionary<string, string>
        {
          {"grant_type",  "authorization_code"},
          { "code", authCode}
        };

      var request = new HttpRequestMessage()
      {
        RequestUri = new Uri(@"https://login.eveonline.com/oauth/token"),
        Method = HttpMethod.Post,
      };
      var content = new FormUrlEncodedContent(values);
      request.Headers.Add("Authorization", $"Basic {Base64Login}");
      request.Content = content;
      HttpClient client = ESIClient.client;
      var tokenResult = await client.SendAsync(request);
      var responseResult = await tokenResult.Content.ReadAsStringAsync();

      dynamic jsonResult = JObject.Parse(responseResult);

      try
      {
        AccessToken = jsonResult.access_token;
        RefreshToken = jsonResult.refresh_token;
      }
      catch
      {
        return false;
      }
      return true;
    }

    private async Task<bool> GetAuthorization(HttpListenerContext result)
    {
      bool success = false;
      Console.Out.WriteLine("Got context");
      var queryString = result.Request.QueryString;
      string responseString = "Auth failed!";
      if (queryString["state"].Equals(_state))
      {      }
      else
      {
        responseString = "Auth failed!  State incorrect";
      }

      if(string.IsNullOrEmpty(queryString["code"]))
      {      }
      else
      {
        AuthorizationCode = queryString["code"];
        if(await GetTokens(AuthorizationCode))
        {
          success = true;
          isAuthorized = true;
          responseString = "Success!  You may now close this page";
        }
        else
        {
          responseString = "Auth failed!";
        }        
      }

      byte[] buffer = Encoding.UTF8.GetBytes(responseString);
      var response = result.Response;
      response.ContentType = "text/html";
      response.ContentLength64 = buffer.Length;
      response.StatusCode = 200;
      response.OutputStream.Write(buffer, 0, buffer.Length);
      response.OutputStream.Close();

      return success;
    }
  }
}
