using System.Xml;
using System.Xml.Serialization;
using MyModels; 

namespace MyTools
{
  public class XmlTool
  {
    public void Create<T>(T t, string filename)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        TextWriter writer = new StreamWriter(filename);
        serializer.Serialize(writer, t);
        writer.Close();
    }
  }
}
