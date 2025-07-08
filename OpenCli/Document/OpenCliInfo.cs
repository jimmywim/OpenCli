namespace OpenCli.Document
{
    public class OpenCliInfo
    {

        public string Title { get; set; } = string.Empty;
        public string? Version { get; set; }
        public string? Description { get; set; }
        public OpenCliContact? Contact { get; set; }
        public OpenCliLicense? License { get; set; }
    }
}
