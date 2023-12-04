namespace MyModels
{
  public class Mxfile
  {
    public Diagram[]? diagrams { get; set; }
    public void Print()
    {
        System.Console.WriteLine("1236547");
    }
  }

  public class Diagram
  {
    public MxGraphModel[]? mxGraphModels { get; set; }
  }

  public class MxGraphModel
  {
    public Root[]? roots { get; set; }
  }

  public class Root
  {
    public Mxcell[]? mxcells { get; set; }
  }

  public class Mxcell
  {
    public string? id { get; set;}
    public string? parent { get; set; }
  }
}
