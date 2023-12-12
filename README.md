# Quick start

Compile the code.

```
bash compile.sh
```

Execute the program.

```
bash run.sh
```

Get the target file.

```
cat App/examples/target/sample_01.xml
```

## Execute via pipe

```
cat App/examples/source/sample_01.yml | docker run -i --rm  -a stdin -a stdout -a stderr  -v `pwd`/App:/tmp -v `pwd`/App/bin/Debug/net8.0:/App --entrypoint /App/DotNet.Docker --rm mcr.microsoft.com/dotnet/runtime:8.0 yml2draw -o /tmp/examples/target/sample_01.xml
```
