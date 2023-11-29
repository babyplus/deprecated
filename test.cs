using System;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

struct Person
{
  public string Name;
  public int Age;
}

class App {
    static void Main(string[] args)
    {
        Person person = new Person();
        person.Name = "huang";
        person.Age = 18;
        var serializer = new SerializerBuilder()
    .WithNamingConvention(CamelCaseNamingConvention.Instance)
    .Build();
        var yaml = serializer.Serialize(person);
        System.Console.WriteLine(yaml);

        string yml = @"
name: huang han jia
age: 18
";
        
        var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
        var p = deserializer.Deserialize<Person>(yml);
        System.Console.WriteLine($"{p.Name} is {p.Age} years old" );
    }
}

