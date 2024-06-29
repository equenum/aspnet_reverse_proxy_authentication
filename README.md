# aspnet_reverse_proxy_authentication

### YARP: Yet Another Reverse Proxy:

- Repo: https://github.com/microsoft/reverse-proxy.
- Docs: https://microsoft.github.io/reverse-proxy/articles/getting-started.html.
- Packages: https://microsoft.github.io/reverse-proxy/articles/packages-refs.html.

### Features used in the project:

- [Reverse Proxy destination forwarding](https://microsoft.github.io/reverse-proxy/articles/config-files.html).
- [Authentication and Authorization](https://microsoft.github.io/reverse-proxy/articles/authn-authz.html).
- [Session Affinity](https://microsoft.github.io/reverse-proxy/articles/session-affinity.html).
- [Load Balancing](https://microsoft.github.io/reverse-proxy/articles/load-balancing.html).
- [Request Transforms](https://microsoft.github.io/reverse-proxy/articles/transforms.html).

### Interesting articles:

- [What is a reverse proxy? | Proxy servers explained](https://www.cloudflare.com/learning/cdn/glossary/reverse-proxy/)
- [What Is a Reverse Proxy Server?](https://www.nginx.com/resources/glossary/reverse-proxy-server/)
- [What is a Reverse Proxy vs. Load Balancer?](https://www.nginx.com/resources/glossary/reverse-proxy-vs-load-balancer/)

### Other useful materials:

- [How To Build a Load Balancer In .NET With YARP Reverse Proxy](https://www.youtube.com/watch?v=0RaH9hhOF4g) by Milan Jovanović.
- [Configure YARP as a Reverse Proxy with .NET](https://www.youtube.com/watch?v=-SiYAYp5AOI&t=1s) by James Montemagno.
- [Reverse proxying is easy with YARP | .NET Conf 2023](https://www.youtube.com/watch?v=P8y8NAroVKk) by dotnet.
- [Introduction to YARP a .NET Reverse Proxy](https://www.youtube.com/watch?v=EfVVvEtfgpI) by Raw Coding.
- [ASP.NET Core Authentication with YARP](https://www.youtube.com/watch?v=9mFNTpugB6E) by Raw Coding.

### Run locally

To imitate a distributed microservice infrastructure I spun up several api service instances as Docker containers.

Build Docker image:

```shell
docker build -t media-api -f Dockerfile .
```

Run containers:

```shell
docker run --name media-api-1 -p 5000:8080 media-api:latest
docker run --name media-api-2 -p 6000:8080 media-api:latest
```
