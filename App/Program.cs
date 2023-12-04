using MyModels;
using MyTools;

class App {
    static void Main(string[] args)
    {

        Mxfile mxfile = new Mxfile();
        Diagram diagram = new Diagram();
        MxGraphModel mxGraphModel = new MxGraphModel();
        Root root = new Root();
        Mxcell mxcell = new Mxcell();
        
        mxfile.diagrams = new Diagram[]{diagram};
        diagram.mxGraphModels = new MxGraphModel[]{mxGraphModel};
        mxGraphModel.roots = new Root[]{root};
        root.mxcells = new Mxcell[]{mxcell};
        mxcell.id = "20";
        mxcell.parent = "1";

        XmlTool xt = new XmlTool();
        xt.Create(mxfile, "/tmp/mxfile.xml");
    }
}

