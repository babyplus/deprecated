docker run -it -v `pwd`/App:/tmp -v `pwd`/App/bin/Debug/net8.0:/App --entrypoint /App/DotNet.Docker --rm mcr.microsoft.com/dotnet/runtime:8.0 yml2draw -i /tmp/examples/source/sample_01.yml -o /tmp/examples/target/sample_01.xml
docker run -it -v `pwd`/App:/tmp -v `pwd`/App/bin/Debug/net8.0:/App --entrypoint /App/DotNet.Docker --rm mcr.microsoft.com/dotnet/runtime:8.0 csv2draw -i /tmp/examples/source/sample_03.csv -o /tmp/examples/target/sample_03.xml
