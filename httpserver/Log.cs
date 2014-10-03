using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    class Log
    {
        const string Source = "vores server";
        const string sLog = "Application";

        public static void WriteInfo(string message)
        {
            if (!EventLog.SourceExists(Source))
            {
                EventLog.CreateEventSource(Source, sLog);
            }
            string machineName = "."; // this computer
            using (EventLog log = new EventLog(sLog, machineName, Source))
            {
                log.WriteEntry(message, EventLogEntryType.Information);
            }
        }
    }
}
