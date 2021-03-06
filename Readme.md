# Eve Sharp Interface

Eve Sharp Interface is a wrapper for the ESI API for Eve Online.  Documentation for that API can be found here:

https://esi.tech.ccp.is/latest/

This library is aimed at facilitating the retrieval and formatting of data as well as preform common tasks, such as SSO.

## Requirements

[JSON.Net](http://www.newtonsoft.com/json):

  - `Install-Package Newtonsoft.Json`

[Eve 3rd party application](https://developers.eveonline.com/applications)

## Authentication

This library provides a class that handles authentication through Eve's SSO/OAuth interface to retrieve the Access Token utilized in authenticated ESI calls including easy addition of scopes.

## Operations

### Fittings

* GetCharacterFittings

### Location

* GetCharacterLocation
* GetCharacterOnline
* GetCurrentShip
