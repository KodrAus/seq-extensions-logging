using System;
using Xunit;
using Serilog.Parameters;
using Serilog.Events;
using Seq.Extensions.Logging;

namespace Tests.Seq.Extensions.Logging;

public class EnrichingEventTests
{
    [Fact]
    public void AddPropertyIfAbsentAddsProperties()
    {
        var enriching = new EnrichingEvent(
            new LogEvent(
                default,
                default,
                default,
                default,
                new(),
                default,
                default
            ),
            new PropertyValueConverter(int.MaxValue, int.MaxValue)
        );

        enriching.AddPropertyIfAbsent("A", false);
        enriching.AddPropertyIfAbsent("A", true);

        Assert.Equal(false, (enriching.LogEvent.Properties["A"] as ScalarValue).Value);
    }

    [Fact]
    public void AddOrUpdatePropertyAddsProperties()
    {
        var enriching = new EnrichingEvent(
            new LogEvent(
                default,
                default,
                default,
                default,
                new(),
                default,
                default
            ),
            new PropertyValueConverter(int.MaxValue, int.MaxValue)
        );

        enriching.AddOrUpdateProperty("A", false);
        enriching.AddOrUpdateProperty("A", true);

        Assert.Equal(true, (enriching.LogEvent.Properties["A"] as ScalarValue).Value);
    }
}
