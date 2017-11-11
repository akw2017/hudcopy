using System.Diagnostics;

namespace Kent.Boogaart.Truss.Diagnostics
{
	//much of the code in this class is commented out to avoid compile errors, since nothing is using it and analysis rule CA1811 checks for that
	internal static class Log
	{
		private static readonly TraceSource _traceSource = new TraceSource("Kent.Boogaart.Truss");
		private const TraceEventType _performanceEventType = TraceEventType.Information;

		//public static bool IsEnabled(TraceEventType eventType)
		//{
		//    return _traceSource.Switch.ShouldTrace(eventType);
		//}

		public static void Write(TraceEventType eventType, string message)
		{
			_traceSource.TraceEvent(eventType, 0, message);
		}

		//public static void Write(TraceEventType eventType, int eventId, string message)
		//{
		//    _traceSource.TraceEvent(eventType, eventId, message);
		//}

		public static void Write(TraceEventType eventType, string format, params object[] args)
		{
			_traceSource.TraceEvent(eventType, 0, format, args);
		}

		//public static void Write(TraceEventType eventType, int eventId, string format, params object[] args)
		//{
		//    _traceSource.TraceEvent(eventType, eventId, format, args);
		//}

		//public static void WriteData(TraceEventType eventType, params object[] data)
		//{
		//    _traceSource.TraceData(eventType, 0, data);
		//}

		//public static void WriteData(TraceEventType eventType, int eventId, params object[] data)
		//{
		//    _traceSource.TraceData(eventType, eventId, data);
		//}

		//public static IDisposable Performance(string message)
		//{
		//    if (!IsEnabled(_performanceEventType))
		//    {
		//        return DummyPerformanceEntry.Instance;
		//    }
		//    else
		//    {
		//        return new PerformanceEntry(message);
		//    }
		//}

		//public static IDisposable Performance(string format, params object[] args)
		//{
		//    return Performance(string.Format(CultureInfo.InvariantCulture, format, args));
		//}

		//used when performance logging is turned off, to increase performance
		//private sealed class DummyPerformanceEntry : IDisposable
		//{
		//    public static readonly IDisposable Instance = new DummyPerformanceEntry();

		//    private DummyPerformanceEntry()
		//    {
		//    }

		//    public void Dispose()
		//    {
		//    }
		//}

		//used when performance logging is turned on
		//private sealed class PerformanceEntry : IDisposable
		//{
		//    private readonly string _message;
		//    private readonly Stopwatch _stopwatch;
		//    private bool _disposed;

		//    public PerformanceEntry(string message)
		//    {
		//        _message = message;
		//        _stopwatch = Stopwatch.StartNew();
		//    }

		//    public void Dispose()
		//    {
		//        if (!_disposed)
		//        {
		//            Log.Write(_performanceEventType, "Performance for '{0}': {1} ({2}ms)", _message, _stopwatch.Elapsed, _stopwatch.ElapsedMilliseconds);
		//            _disposed = true;
		//        }
		//    }
		//}
	}
}