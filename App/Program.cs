using System;
using System.Xml;
using System.Xml.Serialization;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using MyModels;

[XmlRoot("Person", Namespace="huang", IsNullable = false)]

public class XmlTest
{
  public void Create(string filename)
  {
      XmlSerializer serializer = new XmlSerializer(typeof(Person));
      TextWriter writer = new StreamWriter(filename);
      Person p = new Person();
      p.Name = "huang han jia";
      p.Age = 18;
      Person p1 = new Person();
      p1.Name = "123";
      p1.Age = 456;
      Person p2 = new Person();
      p2.Name = "789";
      p2.Age = 10;
      p.P = new Person[] {p1,p2};
      serializer.Serialize(writer, p);
      writer.Close();
  }

//  public void Read(string filename)
//  {
//      XmlSerializer serializer = new XmlSerializer(typeof(Person));
//      FileStream fs = new FileStream(filename, FileMode.Open);
//      Person p = (Person) serializer.Deserialize(fs);
//      //... read properties of p
//  }
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


        XmlDocument doc = new XmlDocument();
        try
        {
          doc.Load("/tmp/temp.xml");
          if (doc.DocumentElement is not null)
          {
            XmlNode? node = doc.DocumentElement.SelectSingleNode("/book/title");
            if (node is not null)
            {
              string text = node.InnerText;
              System.Console.WriteLine(text);
              XmlTest xt = new XmlTest();
              xt.Create("/tmp/person.xml");
//              xt.Read("/tmp/person.xml");
            }
          }
        }
        catch (FileNotFoundException)
        {
          System.Console.Write("FileNotFoundException!\n");
        }
    }
}

