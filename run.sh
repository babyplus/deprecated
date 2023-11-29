docker run -it -v `pwd`/App:/tmp -v `pwd`/App/bin/Debug/net8.0:/App --entrypoint /App/DotNet.Docker --rm mcr.microsoft.com/dotnet/runtime:8.0
