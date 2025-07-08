using CommandLine;

namespace OpenCli.CommandLine.Sample
{
    /// <summary>
    /// A sample of the CommandLineParser options definition
    /// More examples here: https://github.com/commandlineparser/commandline/tree/master?tab=readme-ov-file#c-examples
    /// </summary>
    internal class SampleOptions
    {
        [Option('t', "TestOption", Required = false, HelpText = "This is a sample option")]
        public string TestOption { get; set; } = string.Empty;
    }
}
