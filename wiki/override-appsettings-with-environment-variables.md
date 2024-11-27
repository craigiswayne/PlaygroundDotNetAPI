# Override App Settings with Environment Variables

Goal: to inject secrets or keys at build time so that they're not committed into the vcs

###  Why
Suppose you need a private key or public key in your app,

But you don't want to store in appsettings.json

let's work with the variable "MY_SECRET"

### Checks:
* [ ] Inject when using dotnet cli
* [ ] Inject when using docker build
* [ ]
* [X] Configuration preference order:
  1. Dockerfile ENV (if using docker)
  2. System Environment variable
  3. `appsettings.{ENV}.json`
  4. `appsettings.json`
* [ ] Must cater for nested properties

----

How to achieve this in powershell

```powershell
[Environment]::SetEnvironmentVariable('MY_SECRET', 'from-environment-variables');
clear; 
dotnet build; 
dotnet run --project .\PlaygroundDotNetAPI\PlaygroundDotNetAPI.csproj --environment=UAT;
```

```http request
GET https://localhost:7068/environment
```

notice the value of "mySecret"

---

### With Dockerfile
Whilst you can transmit environment specific values in the Dockerfile

it is not a recommended procedure for secrets, because they persist in the image

better yet, use docker secrets

https://docs.docker.com/build/building/secrets/

The environment variable is already added to the docker file,

Run the docker file and browse to the endpoint

### With Docker CLI

```shell
docker run -e MY_SECRET="from-docker-cli"
```

----

### Logging

you might want to use this to write info to your console

```csharp
var mySecret = builder.Configuration.GetValue<string>("MY_SECRET");
System.Diagnostics.Debug.WriteLine($"MY_SECRET: {mySecret}");
Console.Out.WriteLine($"MY_SECRET: {mySecret}");
Console.WriteLine($"MY_SECRET: {mySecret}");
```

---

#### References:
* https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-9.0



Can use dotnet run command, and pass env variables as parameter using -p flag. For example  dotnet run -p:Environment=Development -p:EnvVar=value1
????