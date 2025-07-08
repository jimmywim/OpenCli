using CommandLine;

namespace OpenCli.CommandLine.Sample
{
    internal class SampleOptions
    {
        [Option('t', "TestOption", Required = false, HelpText = "This is a sample option")]
        public string TestOption { get; set; }
    }
}
