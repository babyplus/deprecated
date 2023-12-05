using MyModels;
using MyTools;

class App {
    static void Main(string[] args)
    {
        Mxfile mxfile = new(){
            diagrams = new Diagram[]{
                new() {
                    mxGraphModels = new MxGraphModel[]{
                        new() {
                            roots = new Root[]{
                                new(){
                                    mxcells = new Mxcell[]{
                                        new(){
                                            id = "0000",
                                            parent = "0",
                                            _value = "value",
                                            style = "style",
                                            vertex = "vertex",
                                            mxGeometry = new MxGeometry(){
                                                x = "0",
                                                y = "0",
                                                width = "0",
                                                height = "0",
                                                _as = "0",
                                                relative = "1",
                                                mxPoint = new MxPoint[]{
                                                    new(){_as="target"},
                                                    new(){_as="source"}
                                                }
                                            }
                                        },
                                        new(){
                                            id = "0001",
                                            parent = "0",
                                            _value = "value",
                                            style = "style",
                                            vertex = "vertex",
                                            mxGeometry = new MxGeometry(){
                                                x = "0",
                                                y = "0",
                                                width = "0",
                                                height = "0",
                                                _as = "0",
                                                relative = "0",
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new() {
                    mxGraphModels = new MxGraphModel[]{
                        new() {
                            roots = new Root[]{
                                new(){
                                    mxcells = new Mxcell[]{
                                        new(){
                                            id = "1000",
                                            parent = "1",
                                            _value = "value",
                                            style = "style",
                                            vertex = "vertex",
                                            mxGeometry = new MxGeometry(){
                                                x = "0",
                                                y = "0",
                                                width = "0",
                                                height = "0",
                                                _as = "0",
                                                relative = "0",
                                            }
                                        },
                                        new(){
                                            id = "1001",
                                            parent = "1",
                                            _value = "value",
                                            style = "style",
                                            vertex = "vertex",
                                            mxGeometry = new MxGeometry(){
                                                x = "0",
                                                y = "0",
                                                width = "0",
                                                height = "0",
                                                _as = "0",
                                                relative = "0",
                                            }
                                        }
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

