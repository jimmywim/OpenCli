using System.Collections.Generic;

namespace OpenCli.Document
{
    public class OpenCliOption
    {
        public bool? Required { get; set; }
        public IList<string>? Aliases { get; set; }
        public IDictionary<string, OpenCliArgument>? Arguments { get; set; }
        public string? Group { get; set; }
        public string? Description { get; set; }
        public bool? Hidden { get; set; }
        public IDictionary<string, object>? Metadata { get; set; }
    }
}
