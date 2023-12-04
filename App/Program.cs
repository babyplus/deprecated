using MyModels;
using MyTools;

class App {
    static void Main(string[] args)
    {

        Mxfile mxfile = new Mxfile();
        Diagram diagram0 = new Diagram();
        Diagram diagram1 = new Diagram();
        MxGraphModel mxGraphModel00 = new MxGraphModel();
        MxGraphModel mxGraphModel10 = new MxGraphModel();
        Root root000 = new Root();
        Root root100 = new Root();
        Mxcell mxcell0000 = new Mxcell();
        Mxcell mxcell0001 = new Mxcell();
        Mxcell mxcell1000 = new Mxcell();
        Mxcell mxcell1001 = new Mxcell();
        
        mxfile.diagrams = new Diagram[]{diagram0,diagram1};
        diagram0.mxGraphModels = new MxGraphModel[]{mxGraphModel00};
        diagram1.mxGraphModels = new MxGraphModel[]{mxGraphModel10};
        mxGraphModel00.roots = new Root[]{root000};
        mxGraphModel10.roots = new Root[]{root100};
        root000.mxcells = new Mxcell[]{mxcell0000,mxcell0001};
        root100.mxcells = new Mxcell[]{mxcell1000,mxcell1001};
        mxcell0000.id = "0000";
        mxcell0000.parent = "0";
        mxcell0001.id = "0001";
        mxcell0001.parent = "0";
        mxcell1000.id = "1000";
        mxcell1000.parent = "1";
        mxcell1001.id = "1001";
        mxcell1001.parent = "1";

        XmlTool xt = new XmlTool();
        xt.Create(mxfile, "/tmp/mxfile.xml");
    }
}

