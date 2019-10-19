//using System.Diagnostics;
//using System.Reflection;

//namespace Xplorium.Core.Utilities.Loggers
//{
//  public class EventLogLogger : Logger
//  {
//    public override void LogError(string error)
//    {
//      EventLog log = new EventLog("Application");
//      log.Source = Assembly.GetExecutingAssembly().ToString();
//      log.WriteEntry(error, EventLogEntryType.Error);
//    }
//  }
//}

