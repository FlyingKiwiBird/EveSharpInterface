using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveSharpInterface.Enums;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace EveSharpInterface.SSO
{
  public class Auth
  {

    private HttpListener _authServer = new HttpListener();
    private bool isAuthorized = false;
    private string _state;
    private ScopeCollection _scopes;

    public delegate void AuthorizationHandler(Auth e);
    public event AuthorizationHandler onAuthorizationSuccess;
    public event AuthorizationHandler onAuthorizationFailure;

    public string SSOKey { get; private set; }

    public void Authenticate(string callbackURL, string clientID, Server server = Server.Tranquility)
    {
      Authenticate(callbackURL, clientID, new ScopeCollection(), server);
    }

    public void Authenticate(string callbackURL, string clientID, ScopeCollection scopes, Server server = Server.Tranquility)
    {
      isAuthorized = false;

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
        loginURL += "&scope=" + Uri.EscapeDataString(scopeStr);
      }

      loginURL += "&state=" + _state;

      _authServer.Prefixes.Add(callbackURL);
      _authServer.Start();
      _authServer.BeginGetContext(AuthCallback, null);

      Process.Start(loginURL);
    }

    private void AuthCallback(IAsyncResult result)
    {
      bool success = false;
      Console.Out.WriteLine("Got context");
      var context = _authServer.EndGetContext(result);
      var queryString = context.Request.QueryString;
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
        SSOKey = queryString["code"];
        success = true;
        responseString = "Success!  You may now close this page";
      }

      byte[] buffer = Encoding.UTF8.GetBytes(responseString);
      var response = context.Response;
      response.ContentType = "text/html";
      response.ContentLength64 = buffer.Length;
      response.StatusCode = 200;
      response.OutputStream.Write(buffer, 0, buffer.Length);
      response.OutputStream.Close();

      if (success)
      {
        onAuthorizationSuccess?.Invoke(this);
      }
      else
      {
        onAuthorizationFailure?.Invoke(this);
      }
    }
  }
}
