using System.Xml.Serialization;

namespace MyModels
{
  [XmlRoot("mxfile")]
  public class Mxfile
  {
    [XmlElement("diagram")]
    public Diagram[]? diagrams { get; set; }
  }

  public class Diagram
  {
    [XmlElement("mxGraphModel")]
    public MxGraphModel[]? mxGraphModels { get; set; }
    [XmlAttribute("name")]
    public string? name { get; set; }
  }

  public class MxGraphModel
  {
    [XmlElement("root")]
    public Root[]? roots { get; set; }
  }

  public class Root
  {
    [XmlElement("mxCell")]
    public MxCell[]? mxcells { get; set; }
  }

  public class MxCell
  {
    [XmlAttribute("id")]
    public string? id { get; set; }
    [XmlAttribute("parent")]
    public string? parent { get; set; }
    [XmlAttribute("value")]
    public string? @value { get; set; }
    [XmlAttribute("style")]
    public string? style { get; set; }
    [XmlAttribute("vertex")]
    public string? vertex { get; set; }
    [XmlAttribute("edge")]
    public string? edge { get; set; }
    [XmlAttribute("source")]
    public string? source { get; set; }
    [XmlAttribute("target")]
    public string? target { get; set; }
    [XmlElement("mxGeometry")]
    public MxGeometry? mxGeometry { get; set; }
  }
  
  public class MxGeometry
  {
    [XmlAttribute("x")]
    public string? x { get; set; }
    [XmlAttribute("y")]
    public string? y { get; set; }
    [XmlAttribute("width")]
    public string? width { get; set; }
    [XmlAttribute("height")]
    public string? height { get; set; }
    [XmlAttribute("as")]
    public string? @as { get; set; }
    [XmlAttribute("relative")]
    public string? relative { get; set; }
    [XmlElement("mxPoint")]
    public MxPoint[]? mxPoint { get; set; }
  }
  
  public class MxPoint
  {
    [XmlAttribute("as")]
    public string? @as { get; set; }
  }
}
