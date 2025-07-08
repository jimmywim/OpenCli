using System.Collections.Generic;

namespace OpenCli.Document
{
    public class OpenCliArgument
    {
        public bool Required { get; set; }
        public int Ordinal { get; set; }
        public OpenCliArity Arity { get; set; }
        public IList<string> AcceptedValues { get; set; }
        public string Group { get; set; }
        public string Description { get; set; }
        public bool Hidden { get; set; }
        public IDictionary<string, string> Metadata { get; set; }
    }
}
