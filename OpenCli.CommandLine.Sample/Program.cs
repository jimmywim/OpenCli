using CommandLine;
using OpenCli.CommandLineParser.Document;
using OpenCli.Serialization;
using System.Reflection;

namespace OpenCli.CommandLine.Sample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Generate a OCS document

            var openCli = OpenCliDocumentGenerator.CreateFromAssembly(Assembly.GetExecutingAssembly());

            using IOpenCliDocumentSerializer jsonSerializer = new OpenCliDocumentJsonSerializer(openCli);
            var json = await jsonSerializer.SerializeAsync();

            Parser.Default.ParseArguments<SampleOptions>(args)
                .WithParsed<SampleOptions>(o =>
                {
                    Console.WriteLine($"This command ran with the Test Option set to: {o.TestOption}");
                });
        }
    }
}
