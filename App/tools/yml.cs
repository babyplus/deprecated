using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using MyModels;

namespace MyTools 
{
    public class YmlTool
    {
       public void Print(string yml)
       {
            var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
            var p = deserializer.Deserialize<Person>(yml);
            System.Console.WriteLine($"{p.Name} is {p.Age} years old" );
       }
       public string Print<T>(T t)
       {
            var serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
            var yaml = serializer.Serialize(t);
            System.Console.WriteLine(yaml);
            return yaml;
       }
    }
}
