using Quartz;
using System.Diagnostics.Metrics;
using Azure.Messaging.ServiceBus.Administration;

public class OtelWorker : IJob
{
    private readonly OtelMetricsService _otelMetricsService;
    private readonly ILogger<OtelWorker> _logger;
    private readonly Meter _meter;

    public OtelWorker(ILogger<OtelWorker> logger, IMeterFactory meterFactory, OtelMetricsService otelMetricsService)
    {
        _meter = meterFactory.Create(GLOBALS.OTELMETRICSMETERNAME);
        _otelMetricsService = otelMetricsService;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogDebug("Started OtelWorker.Execute worker");

        var dict = new Dictionary<string, object>();

        // multidimensional gauge
        // _meter.CreateObservableGauge<long>($"queue.{queue.Name}.messages", () => {
        //     return
        //     [
        //         // pretend these measurements were read from a real queue somewhere
        //         new Measurement<long>(queueDetails.ActiveMessageCount, new KeyValuePair<string,object?>("count", "active")),
        //         new Measurement<long>(queueDetails.DeadLetterMessageCount, new KeyValuePair<string,object?>("count", "deadletter")),
        //         new Measurement<long>(queueDetails.TotalMessageCount, new KeyValuePair<string,object?>("count", "total")),
        //     ];
        // });            

        // one-dimensional gauge
        // var totalmessagecount = _meter.CreateObservableGauge<long>($"topic.{topic.Name}.numberofsubscribers", () => topicDetails.SubscriptionCount, "count", "Number of active messages");

        _otelMetricsService.Set(dict);
    }
}       