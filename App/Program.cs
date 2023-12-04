using MyModels;
using MyTools;

class App {
    static void Main(string[] args)
    {
        Person p0 = new Person();
        p0.Name = "huang han jia";
        p0.Age = 18;
        Person p1 = new Person();
        p1.Name = "123";
        p1.Age = 456;
        Person p2 = new Person();
        p2.Name = "789";
        p2.Age = 10;
        p0.P = new Person[] {p1,p2};

        YmlTool yt = new YmlTool();
        string yml = yt.Print(p0);
        yt.Print(yml);

        XmlTool xt = new XmlTool();
        xt.Create(p0, "/tmp/person.xml");
    }
}

