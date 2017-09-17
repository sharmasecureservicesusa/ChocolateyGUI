﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Chocolatey" file="SerilogLogger.cs">
//   Copyright 2014 - Present Rob Reynolds, the maintainers of Chocolatey, and RealDimensions Software, LLC
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ChocolateyGui
{
    using System;
    using chocolatey.infrastructure.logging;
    using Models;
    using Serilog;

    public class SerilogLogger : ILog
    {
        private readonly ILogger _logger;
        private Action<LogMessage> _interceptor;
        private string _context;

        public SerilogLogger(ILogger logger)
        {
            _logger = logger;
        }

        public IDisposable Intercept(Action<LogMessage> interceptor)
        {
            return new InterceptMessages(this, interceptor);
        }

        public void InitializeFor(string loggerName)
        {
            _context = loggerName;
        }

        public void Debug(string message, params object[] formatting)
        {
            _logger.Debug(message, formatting);

            var logMessage = new LogMessage
            {
                Context = _context,
                LogLevel = LogLevel.Debug,
                Message = string.Format(message, formatting)
            };

            _interceptor?.Invoke(logMessage);
        }

        public void Debug(Func<string> message)
        {
            _logger.Debug(message());

            var logMessage = new LogMessage
            {
                Context = _context,
                LogLevel = LogLevel.Debug,
                Message = message()
            };

            _interceptor?.Invoke(logMessage);
        }

        public void Info(string message, params object[] formatting)
        {
            _logger.Information(message, formatting);

            var logMessage = new LogMessage
            {
                Context = _context,
                LogLevel = LogLevel.Info,
                Message = string.Format(message, formatting)
            };

            _interceptor?.Invoke(logMessage);
        }

        public void Info(Func<string> message)
        {
            _logger.Information(message());

            var logMessage = new LogMessage
            {
                Context = _context,
                LogLevel = LogLevel.Info,
                Message = message()
            };

            _interceptor?.Invoke(logMessage);
        }

        public void Warn(string message, params object[] formatting)
        {
            _logger.Warning(message, formatting);

            var logMessage = new LogMessage
            {
                Context = _context,
                LogLevel = LogLevel.Warn,
                Message = string.Format(message, formatting)
            };

            _interceptor?.Invoke(logMessage);
        }

        public void Warn(Func<string> message)
        {
            _logger.Warning(message());

            var logMessage = new LogMessage
            {
                Context = _context,
                LogLevel = LogLevel.Warn,
                Message = message()
            };

            _interceptor?.Invoke(logMessage);
        }

        public void Error(string message, params object[] formatting)
        {
            _logger.Error(message, formatting);

            var logMessage = new LogMessage
            {
                Context = _context,
                LogLevel = LogLevel.Error,
                Message = string.Format(message, formatting)
            };

            _interceptor?.Invoke(logMessage);
        }

        public void Error(Func<string> message)
        {
            _logger.Error(message());

            var logMessage = new LogMessage
            {
                Context = _context,
                LogLevel = LogLevel.Error,
                Message = message()
            };

            _interceptor?.Invoke(logMessage);
        }

        public void Fatal(string message, params object[] formatting)
        {
            _logger.Fatal(message, formatting);

            var logMessage = new LogMessage
            {
                Context = _context,
                LogLevel = LogLevel.Fatal,
                Message = string.Format(message, formatting)
            };

            _interceptor?.Invoke(logMessage);
        }

        public void Fatal(Func<string> message)
        {
            _logger.Fatal(message());

            var logMessage = new LogMessage
            {
                Context = _context,
                LogLevel = LogLevel.Fatal,
                Message = message()
            };

            _interceptor?.Invoke(logMessage);
        }

        public class InterceptMessages : IDisposable
        {
            private readonly SerilogLogger _logger;

            public InterceptMessages(SerilogLogger logger, Action<LogMessage> interceptor)
            {
                _logger = logger;
                logger._interceptor = interceptor;
            }

            public void Dispose()
            {
                _logger._interceptor = null;
            }
        }
    }
}