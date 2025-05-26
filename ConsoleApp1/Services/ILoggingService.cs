using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TestProject1.Services;

namespace ConsoleApp1.Services
{
    public interface ILoggingService : IDisposable, IAsyncDisposable
    {
        void InitializeLogWriter();
        string GetLogFileName();
        Task LogAsync(string methodName, bool success, string? additionalInfo = null);
        void Dispose();
        void Dispose(bool disposing);
        ValueTask DisposeAsync();
    }
}
