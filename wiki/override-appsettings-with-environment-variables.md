# Override App Settings with Environment Variables

Goal: to inject secrets or keys at build time so that they're not committed into the vcs

###  Why
Suppose you need a private key or public key in your app,

But you don't want to store in appsettings.json

let's work with the variable "MY_SECRET"

### Checks:
* [ ] Inject when using dotnet cli
* [ ] Inject when using docker build
* [ ] Environment Variables must override values in `appsettings.json`
* [ ] Configuration preference order:
  * [ ] Environment
  * [ ] `appsettings.{ENV}.json`
  * [ ] `appsettings.json`
* [ ] Must cater for nested properties

----

How to achieve this in powershell

```powershell
[Environment]::SetEnvironmentVariable('MY_SECRET', 'from-environment-variables');
```

---

#### References:
* https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-9.0