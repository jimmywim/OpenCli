using CommandLine;
using OpenCli.CommandLineParser.Document;
using OpenCli.Serialization;
using System.Reflection;

namespace OpenCli.CommandLine.Sample
{
    /// <summary>
    /// A sample application that uses CommandLineParser to define the possible arguments
    /// Generates a OpenCliDocument class instance, and also creates a JSON document from the generated class.
    /// </summary>
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Generate a OCS document
            var openCli = OpenCliDocumentGenerator.CreateFromAssembly(Assembly.GetExecutingAssembly());

            using IOpenCliDocumentSerializer jsonSerializer = new OpenCliDocumentJsonSerializer(openCli);
            var json = await jsonSerializer.SerializeAsync();


            // Run the actual app with the provided command line arguments, parsed with CommandLineParser
            // and available as type-safe properties of the SampleOptions class
            Parser.Default.ParseArguments<SampleOptions>(args)
                .WithParsed<SampleOptions>(o =>
                {
                    Console.WriteLine($"This command ran with the Test Option set to: {o.TestOption}");
                });
        }
    }
}
