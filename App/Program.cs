using MyModels;
using MyTools;

class App {
    static void Main(string[] args)
    {
        Mxfile mxfile = new(){
            diagrams = new[]{
                new Diagram() {
                    mxGraphModels = new[]{
                        new MxGraphModel() {
                            roots = new[]{
                                new Root(){
                                    mxcells = new []{
                                        new Mxcell(){
                                            id = "0000",
                                            parent = "0"
                                        },
                                        new(){
                                            id = "0001",
                                            parent = "0"
                                        },
                                    }
                                }
                            }
                        }
                    }
                },
                new Diagram() {
                    mxGraphModels = new[]{
                        new MxGraphModel() {
                            roots = new[]{
                                new Root(){
                                    mxcells = new []{
                                        new Mxcell(){
                                            id = "1000",
                                            parent = "1"
                                        },
                                        new(){
                                            id = "1001",
                                            parent = "1"
                                        },
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };

        XmlTool xt = new();
        xt.Create(mxfile, "/tmp/mxfile.xml");
        YmlTool yt = new();
        string yml = yt.Print(mxfile);
    }
}

