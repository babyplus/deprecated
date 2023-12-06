using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using MyModels;

namespace MyTools 
{
    public class YmlTool
    {
       public string Print<T>(T t)
       {
            var serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
            var yaml = serializer.Serialize(t);
            System.Console.WriteLine(yaml);
            return yaml;
       }

       public void Deserialize<T>(string path, out T t)
       {
            using(var reader = File.OpenText(path))
            {
                var deserializer = new Deserializer();
                t = deserializer.Deserialize<T>(reader);
            }
       }
    }
}
