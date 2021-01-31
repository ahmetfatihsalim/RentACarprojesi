using RentACar5.Commons.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar5.Commons.Concrete.Loggers
{
    internal class EventLogger : LogBase
    {

        public override void Log(string message, bool isError)
        {
            lock (lockObj)
            {
                EventLog m_EventLog = new EventLog();
                m_EventLog.Source = "XBankEventLog";
                m_EventLog.WriteEntry(message);
            }
        }
    }
}
