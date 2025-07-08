using OpenCli.Document;
using System;
using System.Threading.Tasks;

namespace OpenCli.Serialization
{
    public class OpenCliDocumentJsonSerializer : IOpenCliDocumentSerializer, IDisposable
    {
        private OpenCliDocument document;

        public OpenCliDocumentJsonSerializer(OpenCliDocument document)
        {

            this.document = document;
        }

        public void Dispose()
        {
        }

        public async Task<string> SerializeAsync()
        {
            var options = new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };

            return System.Text.Json.JsonSerializer.Serialize(this.document, options);
        }
    }
}
