using CommandLine;
using MyModels;

namespace MyJobs{
    public class OptionsJob
    {
        static public void parse(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<Options>(args)
            .MapResult(
                (Options opts) => App.RunAndReturnExitCode(opts),
                errs => 1
            );
        }
    }

}
