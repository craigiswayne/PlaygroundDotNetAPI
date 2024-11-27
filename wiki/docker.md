# Docker

## Goal:
* [X] docker build in github pipeline
* [X] save docker image as artifact
* [X] save docker image to docker hub

```shell
docker build -t craigiswayne/playground-dotnet-api:latest -f .\Dockerfile .
docker run -it --rm -p 4201:8080 --name playgrounddotnetapi_sample craigiswayne/playground-dotnet-api:latest
# open http://localhost:4201/environment
```

---
#### References
* https://www.lastweekinaws.com/blog/the-17-ways-to-run-containers-on-aws/ 