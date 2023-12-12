using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using MyModels;

namespace MyTools 
{
    public class YmlTool
    {
        static public string Print<T>(T t)
        {
             var serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
             var yaml = serializer.Serialize(t);
             System.Console.WriteLine(yaml);
             return yaml;
        }

        static public void Deserialize<T>(string path, out T? t)
        {
             if(File.Exists(path))
             {
                 using(var reader = File.OpenText(path))
                 {
                     var deserializer = new Deserializer();
                     t = deserializer.Deserialize<T>(reader);
                 }
             }
             else
             {
                 t = default;
                 System.Console.WriteLine($"The {path} isn`t existed.");
             }
        }

        static public void Deserialize<T>(out T? t)
        {
             using (StreamReader reader = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding))
             {
                 var deserializer = new Deserializer();
                 t = deserializer.Deserialize<T>(reader.ReadToEnd());
             }
        }
  }
}
