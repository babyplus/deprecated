using CommandLine;

namespace MyModels
{
    public class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }
    }

    [Verb("yml2draw", HelpText = "Convert the yaml file to the draw xml.")]
    public class YmlOptions : Options
    {
        [Option('i', "input", Required = false, HelpText = "Input file to read.")]
        public string? Input { get; set; }

        [Option('o', "output", Required = false, HelpText = "Output file to write.")]
        public string? Output { get; set; }
    }

    [Verb("json2draw", HelpText = "Convert the json file to the draw xml.")]
    public class JsonOptions : Options
    {
        [Option('i', "input", Required = false, HelpText = "Input file to read.")]
        public string? Input { get; set; }

        [Option('o', "output", Required = false, HelpText = "Output file to write.")]
        public string? Output { get; set; }
    }

    [Verb("csv2draw", HelpText = "Convert the csv file to the draw xml.")]
    public class CsvOptions : Options
    {
        [Option('i', "input", Required = false, HelpText = "Input file to read.")]
        public string? Input { get; set; }

        [Option('o', "output", Required = false, HelpText = "Output file to write.")]
        public string? Output { get; set; }
    }
}

