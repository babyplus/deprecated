using System.Xml.Serialization;
using MyModels; 

namespace MyTools
{
  public class XmlTool
  {
    public void Create<T>(T t, string filename)
    {
        string? directoryPath = Path.GetDirectoryName(filename);
        if (!Directory.Exists(directoryPath) && directoryPath is not null) {
            System.Console.WriteLine($@"{directoryPath} isn`t existed, try to create it.");
            try
            {
                Directory.CreateDirectory(directoryPath);
            }
            catch
            {
                System.Console.WriteLine($@"Create the {directoryPath} failed.");
            }
        }
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        TextWriter writer = new StreamWriter(filename);
        serializer.Serialize(writer, t);
        writer.Close();
    }
  }
}
