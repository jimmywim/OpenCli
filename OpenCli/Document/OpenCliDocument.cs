using System.Collections.Generic;

namespace OpenCli.Document
{
    public class OpenCliDocument
    {
        public string OpenCli { get; set; }
        public OpenCliInfo Info { get; set; }
        public OpenCliConventions Conventions { get; set; }
        public IDictionary<string, OpenCliArgument> Arguments { get; set; }
        public IDictionary<string, OpenCliOption> Options { get; set; }
        public IDictionary<string, OpenCliCommand> Commands { get; set; }
        public IList<OpenCliExitCode> ExitCodes { get; set; }
        public IList<string> Examples { get; set; }
        public IDictionary<string, object> Metadata { get; set; }
    }
}
