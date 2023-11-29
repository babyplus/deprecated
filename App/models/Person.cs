namespace MyModels{
  public class Person
  {
    public string Name;
    public int Age;
    public Person[]? P { get; set;}

    public Person()
    {
      Name = "Unknowned";
      Age = 0;
    }
  }
}
