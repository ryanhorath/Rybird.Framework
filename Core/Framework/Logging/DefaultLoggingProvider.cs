using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Rybird.Framework
{
    public class DefaultLoggingProvider : ILoggingProvider
    {
        public DefaultLoggingProvider(LogSeverityLevel minimumSeverityLevel = LogSeverityLevel.Debug)
        {
            MinimumSeverityLevel = minimumSeverityLevel;
        }

        public LogSeverityLevel MinimumSeverityLevel { get; private set; }

        public void Log(string message, LogSeverityLevel severityLevel, string category = null,
            [CallerFilePath] string filePath = "",
            [CallerMemberName] string memberName = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            if ((int)severityLevel >= (int)MinimumSeverityLevel)
            {
                Debug.WriteLine(severityLevel + ":  " + message + ", File: " + filePath + ", MemberName: " + memberName + ", LineNumber: " + lineNumber);
            }
        }

        public void LogFormat(string format, LogSeverityLevel severityLevel, string category = null,
            [CallerFilePath] string filePath = "",
            [CallerMemberName] string memberName = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            Log(string.Format(format, args), severityLevel, category);
        }
    }
}
