# .NET Microservice and Docker

This is a sample project for a microservice written in .NET Core 2.2 and C# that accesses a Postgres DB. The goal of this project is to establish a smooth process for test drive development (TDD) of the microservice leveraging the power of Docker container and the `docker-compose` tool.

Please read my blog post describing the details: http://gabrielschenker.com/index.php/2019/10/04/a-docker-workflow-for-net-developers/

## Getting Started
To build the container, from the `./api` directory

`build -t auth-api:1.0 .`


To run the container

`docker run -d -p 5002:5001 auth-api:1.0`

## Ryan's Notes
I'm trying to get identity working using Cognito as Prashanth and I discussed. This repo is based on Gabriel`s .net boilerplate: http://gabrielschenker.com/index.php/2019/10/04/a-docker-workflow-for-net-developers/

Gabriel's article will be very helpful to find commands for starting and stopping docker compose files and for general workflow.

I'm working on getting it working using this approach: https://aws.amazon.com/blogs/compute/hosting-asp-net-core-applications-in-amazon-ecs-using-aws-fargate/

To try and get this to work I added `docker-compose-run-watch.yml` file that contains a compose that attempts to start

1. ASP.Net Core API
2. nginx proxy
3. postgres db (note db isn't currently used by api)

### Current Status ###
Below is the command line flow and I'm getting an error as shown at the end.

```
~/code/BOASvNext.Identity [master] $ cd api
~/code/BOASvNext.Identity/api [master] $ docker image build -t boas-vnext-identity/api:1.0 .Sending build context to Docker daemon  14.85kB
Step 1/6 : FROM mcr.microsoft.com/dotnet/core/sdk:2.2
2.2: Pulling from dotnet/core/sdk
092586df9206: Already exists 
ef599477fae0: Already exists 
4530c6472b5d: Already exists 
d34d61487075: Already exists 
642aea159562: Already exists 
8853a497ba65: Already exists 
1f9d522432e0: Already exists 
Digest: sha256:a0e1def091c695ad2c6cd3cdb0359193246a4f8f4093d0030c74ec2a3edc0ae3
Status: Downloaded newer image for mcr.microsoft.com/dotnet/core/sdk:2.2
 ---> f13ac9d68148
Step 2/6 : WORKDIR /app
 ---> Running in ae147ee72af2
Removing intermediate container ae147ee72af2
 ---> 16add0253dd6
Step 3/6 : COPY api.csproj ./
 ---> 8b64dfb8cdf7
Step 4/6 : RUN dotnet restore
 ---> Running in 5c494cf7ad0b
  Restore completed in 739.74 ms for /app/api.csproj.
Removing intermediate container 5c494cf7ad0b
 ---> ef9e4d7cbb2e
Step 5/6 : COPY . .
 ---> 19a60f0278a3
Step 6/6 : CMD dotnet run
 ---> Running in 85bc491558bd
Removing intermediate container 85bc491558bd
 ---> 7dfbd7c26e8f
Successfully built 7dfbd7c26e8f
Successfully tagged boas-vnext-identity/api:1.0
~/code/BOASvNext.Identity/api [master] $ cd ..
~/code/BOASvNext.Identity [master] $ docker-compose -f docker-compose-run-watch.yml up -d
Pulling db (postgres:12.0-alpine)...
12.0-alpine: Pulling from library/postgres
9d48c3bd43c5: Already exists
f112202a5fec: Already exists
e2827e7bbe4a: Already exists
5ce43a1630c4: Already exists
13772e4e58b6: Already exists
a9c3c1abc664: Already exists
b8495f782617: Already exists
8ba4145edc35: Already exists
Digest: sha256:fe9a6bf89c50fb3c6755c0c2d67cb09dc8e90ac468b3212167700b155c902a5d
Status: Downloaded newer image for postgres:12.0-alpine
Building reverseproxy
Step 1/2 : FROM nginx
latest: Pulling from library/nginx
b8f262c62ec6: Already exists
e9218e8f93b1: Already exists
7acba7289aa3: Already exists
Digest: sha256:aeded0f2a861747f43a01cf1018cf9efe2bdd02afd57d2b11fcc7fcadc16ccd1
Status: Downloaded newer image for nginx:latest
 ---> f949e7d76d63
Step 2/2 : COPY nginx.conf /etc/nginx/nginx.conf
 ---> 34e931a16197

Successfully built 34e931a16197
Successfully tagged boasvnextidentity_reverseproxy:latest
WARNING: Image for service reverseproxy was built because it did not already exist. To rebuild this image you must use `docker-compose build` or `docker-compose up --build`.
Starting boasvnextidentity_db_1 ... 
Recreating boasvnextidentity_api_1 ... error

ERROR: for boasvnextidentity_api_1  no such image: sha256:b78dc7f40bfb21e10ebcdadac28191c2087ab4cd5b2fb54150e323077be2f08d: No such image: sha256:b78dc7f40bfb21e10ebcdadac28191c2087ab4cd5b2fb54150e323077be2f08d
```

### Some helpful commands
Here are some helpful commmands

`docker image build -t boas-vnext-identity/api:1.0 .`

Will build the api app. Note that is must be run from `./api` directory and note the `.` on the end of the command, very import.

`docker-compose -f docker-compose-run-watch.yml up --build`

Will build the containers needed by the docker compose `docker-compose-run-watch.yml`

`docker rmi $(docker images -q)`

Will delete all images if you want to try and start from a clean slate

`docker images`

See all images

`docker ps`

See all running containers

`docker rm <container id>`

removes a container

`docker-compose -f docker-compose-run-watch.yml down -v`

Will stop all containers started by `docker-compose-run-watch.yml`

`docker-compose -f docker-compose-run-watch.yml up -d`

Will run containers in `docker-compose-run-watch.yml` in discounnected mode so that you can still use the console

`docker `

