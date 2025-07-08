using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenCli.Serialization
{
    public interface IOpenCliDocumentSerializer : IDisposable
    {
        Task<string> SerializeAsync();
    }
}
