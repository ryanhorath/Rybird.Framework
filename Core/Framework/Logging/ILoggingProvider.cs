using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rybird.Framework
{
    public interface ILoggingProvider
    {
        LogSeverityLevel MinimumSeverityLevel { get; }

        void Log(string message,
                 LogSeverityLevel severityLevel,
                 string category = null,
                 [CallerFilePath] string filePath = null,
                 [CallerMemberName] string memberName = null,
                 [CallerLineNumber] int lineNumber = 0);

        void LogFormat(string format,
                       LogSeverityLevel severityLevel,
                       string category = null,
                       [CallerFilePath] string filePath = null,
                       [CallerMemberName] string memberName = null,
                       [CallerLineNumber] int lineNumber = 0,
                       params object[] args);
    }
}
