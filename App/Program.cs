using MyModels;
using MyTools;
using MyJobs;

class App {

    static void Main(string[] args)
    {
        OptionsJob.parse(args);
    }

    static public int RunAndReturnExitCode(Options opts)
    {
        if(opts.Input is not null && opts.Output is not null)
        {
            Mxfile? mxfile = new();
            YmlTool.Deserialize($@"{opts.Input}", out mxfile);
            XmlTool.Create(mxfile, $@"{opts.Output}");
        }
        else
            System.Console.WriteLine($"Suggested parameter: -i /tmp/examples/sample.yml -o /tmp/examples/target/mxfile.xml");
        return 0;
    }

}

