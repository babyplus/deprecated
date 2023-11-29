docker run -it --rm -v `pwd`:/tmp -w /tmp registry.cn-hangzhou.aliyuncs.com/babyplus/get:a23112456183.mono.6_12_0_182 mcs -r:YamlDotNet.13.7.1/lib/net47/YamlDotNet.dll test.cs
