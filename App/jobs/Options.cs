using CommandLine;
using MyModels;
using MyTools;
using Microsoft.VisualBasic.FileIO;

namespace MyJobs{
    public class Jobs
    {
        static public void parse(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<JsonOptions, YmlOptions, CsvOptions>(args)
            .MapResult(
                (JsonOptions opts) => RunAndReturnExitCode(opts),
                (YmlOptions opts) => RunAndReturnExitCode(opts),
                (CsvOptions opts) => RunAndReturnExitCode(opts),
                errs => 1
            );
        }

        static public int RunAndReturnExitCode(JsonOptions opts){return 0;}
        static public int RunAndReturnExitCode(CsvOptions opts)
        {
            if(opts.Input is not null || Console.IsInputRedirected)
            {
                Mxfile mxfile = new();
                CsvTool.Convert($@"{opts.Input}", mxfile);
                XmlTool.Create(mxfile, opts.Output);
            }
            else
            {
                System.Console.WriteLine("Input is null.");
                string sample_source = "/tmp/examples/source/sample_03.csv";
                string sample_target = "/tmp/examples/target/sample_03.xml";
                if(File.Exists(sample_source))
                    System.Console.WriteLine($"Suggested parameters: -i {sample_source} -o {sample_target}");
            }
            return 0;
        }
        static public int RunAndReturnExitCode(YmlOptions opts)
        {
            if(opts.Input is not null || Console.IsInputRedirected)
            {
                Mxfile? mxfile = new();
                if (opts.Input is not null) YmlTool.Deserialize($@"{opts.Input}", out mxfile);
                if (Console.IsInputRedirected) YmlTool.Deserialize(out mxfile);
                XmlTool.Create(mxfile, opts.Output);
            }
            else
            {
                System.Console.WriteLine("Input is null.");
                string sample_source = "/tmp/examples/source/sample_01.yml";
                string sample_target = "/tmp/examples/target/sample_01.xml";
                if(File.Exists(sample_source))
                    System.Console.WriteLine($"Suggested parameters: -i {sample_source} -o {sample_target}");
            }
            return 0;
        }
    }
}
