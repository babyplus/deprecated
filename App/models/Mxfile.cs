using System.Xml.Serialization;

namespace MyModels
{
  public class Mxfile
  {
    [XmlElement("Diagram")]
    public Diagram[]? diagrams { get; set; }
  }

  public class Diagram
  {
    [XmlElement("MxGraphModel")]
    public MxGraphModel[]? mxGraphModels { get; set; }
  }

  public class MxGraphModel
  {
    [XmlElement("Root")]
    public Root[]? roots { get; set; }
  }

  public class Root
  {
    [XmlElement("Mxcell")]
    public Mxcell[]? mxcells { get; set; }
  }

  public class Mxcell
  {
    public string? id { get; set;}
    public string? parent { get; set; }
  }
}
