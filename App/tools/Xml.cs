using System.Xml.Serialization;
using MyModels; 

namespace MyTools
{
    public class XmlTool
    {
        static public void Create<T>(T t, string? filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            if(filename is not null)
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
                TextWriter writer = new StreamWriter(filename);
                serializer.Serialize(writer, t);
                writer.Close();
            }else{
                serializer.Serialize(Console.Out, t);
            }
        }
    }
}
