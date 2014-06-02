using System;
using System.Xml;

namespace VirtualGallery.Infrastructure.Logging
{
    public interface ILogger
    {
        void ApplyCustomSettings(XmlElement customSettings);

        void WriteLog(string message, LogLevel level);

        void WriteLog(string message, Exception ex, LogLevel level);

        void WriteLogFormat(LogLevel level, string message, params object[] args);
    }
}