using System;
using System.Runtime.CompilerServices;

namespace GothongApp.Services.Predefined
{
    public class LogConsole
    {
        public LogConsole() { }

        public static void AsyncOutput(Object parent, string args, [CallerMemberName] string caller = null, [CallerLineNumber] int lineNumber = 0)
        {
            System.Diagnostics.Debug.WriteLine(parent + " >>> " + caller + " >>> line: " + lineNumber + " >>> " + args);
        }
    }
}
