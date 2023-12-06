using CommandLine;

namespace MyModels
{
  public class Options
  {
      [Option('i', "input", Required = false, HelpText = "Input file to read.")]
      public string? Input { get; set; }

      [Option('o', "output", Required = false, HelpText = "Output file to write.")]
      public string? Output { get; set; }

      [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
      public bool Verbose { get; set; }
  }
}

