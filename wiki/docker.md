# Docker

## Goal:
* [ ] docker build in github pipeline
* [ ] save docker image as artifact

```shell
docker build --no-cache --progress=plain -f PlaygroundDotNetAPI/Dockerfile . -t playgrounddotnetapi &> build.log
docker run -it --rm -p 4201:8080 --name playgrounddotnetapi_sample playgrounddotnetapi
# open http://localhost:4201/environment
```

```shell
docker build -t craigiswayne/playground-dotnet-api:latest -f .\Dockerfile .
docker run -it -p 8282:8080 craigiswayne/playground-dotnet-api
docker run --interactive --tty craigiswayne/playground-dotnet-api
docker run --interactive --tty --publish 8282:8080 craigiswayne/playground-dotnet-api
# browse to http://localhost:8282/environment
```

---
#### References
* https://www.lastweekinaws.com/blog/the-17-ways-to-run-containers-on-aws/ 