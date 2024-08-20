using Serilog.Core;
using Serilog.Events;

namespace Seq.Extensions.Logging;

/// <summary>
/// The input to an enricher.
/// </summary>
public sealed class EnrichingEvent
{
    internal EnrichingEvent(LogEvent logEvent, ILogEventPropertyValueFactory propertyFactory)
    {
        _propertyFactory = propertyFactory;
        LogEvent = logEvent;
    }

    ILogEventPropertyValueFactory _propertyFactory;

    internal LogEvent LogEvent { get; }

    /// <summary>
    /// Add a property to the event if not already present.
    /// </summary>
    public void AddPropertyIfAbsent(string propertyName, object propertyValue, bool serialize = false)
    {
        if (LogEvent.Properties.ContainsKey(propertyName))
            return;

        LogEvent.AddOrUpdateProperty(propertyName, _propertyFactory.CreatePropertyValue(propertyValue, serialize));
    }
}
