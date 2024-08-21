using System;
using Serilog.Parameters;
using Serilog.Events;
using Seq.Extensions.Logging;
using Xunit;

namespace Tests.Seq.Extensions.Logging;

public class EnricherTests
{
    [Fact]
    public void EnrichersAreAppliedInOrder()
    {
        var evt = new LogEvent(
            default,
            default,
            default,
            default,
            new(),
            default,
            default
        );

        new Enricher([
            (evt) => evt.AddPropertyIfAbsent("A", 1),
            (evt) => evt.AddPropertyIfAbsent("A", 2),
            (evt) => evt.AddOrUpdateProperty("B", 1),
            (evt) => evt.AddOrUpdateProperty("B", 2),
        ])
        .Enrich(
            evt,
            new PropertyValueConverter(int.MaxValue, int.MaxValue)
        );

        Assert.Equal(1, (evt.Properties["A"] as ScalarValue).Value);
        Assert.Equal(2, (evt.Properties["B"] as ScalarValue).Value);
    }

    [Fact]
    public void FailingEnricherIsHandled()
    {
        var evt = new LogEvent(
            default,
            default,
            default,
            default,
            new(),
            default,
            default
        );

        new Enricher([
            (evt) => evt.AddOrUpdateProperty("A", 1),
            _ => throw new Exception("Enricher Failed"),
            (evt) => evt.AddOrUpdateProperty("A", 2),
        ])
        .Enrich(
            evt,
            new PropertyValueConverter(int.MaxValue, int.MaxValue)
        );

        Assert.Equal(2, (evt.Properties["A"] as ScalarValue).Value);
    }
}