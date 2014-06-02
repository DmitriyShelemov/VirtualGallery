using System;
using System.Configuration;
using System.Xml;
using log4net;
using log4net.Config;

namespace VirtualGallery.Infrastructure.Logging
{
    public class Logger : ILogger
    {
        #region Static Fields

        private static Lazy<ILogger> _logger = new Lazy<ILogger>(() => new Logger());

        #endregion

        #region Fields

        private readonly ILog _log;

        #endregion

        #region Constructors and Destructors

        private Logger()
        {
            _log = LogManager.GetLogger(typeof(Logger));
            try
            {
                XmlConfigurator.Configure();
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException(
                    "Could not configure log4net. Did you forget to include <log4net> section into configuration file?", 
                    ex);
            }
        }

        #endregion

        #region Public Properties

        public static ILogger Instance
        {
            get
            {
                return _logger.Value;
            }

            set
            {
                _logger = new Lazy<ILogger>(() => value);
            }
        }

        #endregion

        #region Public Methods and Operators

        public void ApplyCustomSettings(XmlElement customSettings)
        {
            XmlConfigurator.Configure(customSettings);
        }

        public void WriteLog(string message, LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    {
                        _log.Debug(message);
                        break;
                    }

                case LogLevel.Error:
                    {
                        _log.Error(message);
                        break;
                    }

                case LogLevel.Fatal:
                    {
                        _log.Fatal(message);
                        break;
                    }

                case LogLevel.Info:
                    {
                        _log.Info(message);
                        break;
                    }

                case LogLevel.Warn:
                    {
                        _log.Warn(message);
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }

        public void WriteLog(string message, Exception ex, LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    {
                        _log.Debug(message, ex);
                        break;
                    }

                case LogLevel.Error:
                    {
                        _log.Error(message, ex);
                        break;
                    }

                case LogLevel.Fatal:
                    {
                        _log.Fatal(message, ex);
                        break;
                    }

                case LogLevel.Info:
                    {
                        _log.Info(message, ex);
                        break;
                    }

                case LogLevel.Warn:
                    {
                        _log.Warn(message, ex);
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }

        public void WriteLogFormat(LogLevel level, string message, params object[] args)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    {
                        _log.DebugFormat(message, args);
                        break;
                    }

                case LogLevel.Error:
                    {
                        _log.ErrorFormat(message, args);
                        break;
                    }

                case LogLevel.Fatal:
                    {
                        _log.FatalFormat(message, args);
                        break;
                    }

                case LogLevel.Info:
                    {
                        _log.InfoFormat(message, args);
                        break;
                    }

                case LogLevel.Warn:
                    {
                        _log.WarnFormat(message, args);
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }

        #endregion
    }
}