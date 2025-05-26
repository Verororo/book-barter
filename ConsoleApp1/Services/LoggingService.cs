using System;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using ConsoleApp1.Services;

namespace TestProject1.Services
{
    public class LoggingService : ILoggingService
    {
        private string _logDirectory;
        private StreamWriter? _logWriter;
        private string? _currentLogDate;
        private bool _disposed = false;

        public LoggingService()
        {
            _logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            if (!Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }
        }

        public void InitializeLogWriter()
        {
            var logFile = GetLogFileName();
            _logWriter?.Dispose();
            _logWriter = new StreamWriter(logFile, append: true) { AutoFlush = true };
        }

        public string GetLogFileName()
        {
            _currentLogDate ??= DateTime.Now.ToString("yyyy-MM-dd");
            return Path.Combine(_logDirectory, $"Logs_{_currentLogDate}.txt");
        }

        public async Task LogAsync(string methodName, bool success, string? additionalInfo = null)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(LoggingService));

            var currentDate = DateTime.Now.ToString("yyyy-MM-dd");

            if (_logWriter == null || currentDate != _currentLogDate)
            {
                _currentLogDate = currentDate;
                InitializeLogWriter();
            }

            var logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Method: {methodName}, " +
                $"Outcome: {(success ? "success" : "failure")}";
                
            if (additionalInfo != null)
            {
                logMessage += $", Additional info: {additionalInfo}";
            }
            logMessage += Environment.NewLine;

            try
            {
                if (_logWriter != null)
                {
                    await _logWriter.WriteAsync(logMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write to log file: {ex.Message}");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _logWriter?.Dispose();
                }
                _disposed = true;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                if (_logWriter != null)
                {
                    await _logWriter.FlushAsync();
                    await _logWriter.DisposeAsync();
                }
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }
    }
} 