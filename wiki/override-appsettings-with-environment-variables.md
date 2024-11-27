# Override App Settings with Environment Variables

**This happens out the box btw**

Goal: to inject secrets or keys at build time so that they're not committed into the vcs

###  Why
Suppose you need a private key or public key in your app,

But you don't want to store in `appsettings.json`

let's work with the variable "MY_SECRET"

### Checks:
* [ ] Inject when using dotnet cli
* [ ] Inject when using docker build
* [ ]
* [X] Configuration preference order:
  1. Dockerfile
  2. System Environment variable
  3. `appsettings.{ENV}.json`
  4. `appsettings.json`
* [ ] Must cater for nested properties

----

### With `dotnet` CLI

```powershell
[Environment]::SetEnvironmentVariable('MY_SECRET', 'from-environment-variables', 'User');
# or
# [Environment]::SetEnvironmentVariable('MY_SECRET', 'from-environment-variables', 'User');
clear; 
dotnet build;
dotnet run --project .\PlaygroundDotNetAPI\PlaygroundDotNetAPI.csproj --environment=UAT;
```

---

### With Dockerfile
Whilst you can transmit environment specific values in the Dockerfile

it is not a recommended procedure for secrets, because they persist in the image

better yet, use docker secrets

https://docs.docker.com/build/building/secrets/

The environment variable is already added to the docker file, however commented out

Run the docker file and browse to the endpoint

----

### With `docker build` CLI

```shell
docker build --build-arg MY_SECRET="from-docker-build-cli" -f PlaygroundDotNetAPI/Dockerfile . -t playgrounddotnetapi
docker run -it --rm -p 4201:8080 --name playgrounddotnetapi_sample playgrounddotnetapi
```

----

### With `docker run` CLI

```shell
# docker build command from above
# you don't need to specify the build arg, it gets overridden anyways
docker run -it --rm -p 4201:8080 --name playgrounddotnetapi_sample playgrounddotnetapi -e MY_SECRET="from-docker-run-cli"
```

----
## Testing

```http request
GET /environment
```

notice the value of "mySecret"

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