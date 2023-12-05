using MyModels;
using MyTools;


class App {
    static void Main(string[] args)
    {
        YmlTool yt = new();
        Mxfile mxfile = new();
        yt.Deserialize("/tmp/examples/sample.yml", out mxfile);
        XmlTool xt = new();
        xt.Create(mxfile, "/tmp/examples/target/mxfile.xml");
    }
}

