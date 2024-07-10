using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Template.OtelEnabler.Helpers;

namespace servicebusmetricsenabler.Controllers;

[ApiController]
[Route("[controller]")]
public class OtelController : ControllerBase
{
    private readonly ILogger<OtelController> _logger;
    private readonly OtelMetricsService _otelMetricsService;

    public OtelController(ILogger<OtelController> logger, OtelMetricsService otelMetricsService)
    {
        _logger = logger;
        _otelMetricsService = otelMetricsService;
    }

    [HttpGet]
    public Dictionary<string, object> Get() 
    {

        return _otelMetricsService.Get();
    }

    [HttpGet("Healthcheck")]
    public Dictionary<string, bool> Healthcheck() 
    {
        var result = new Dictionary<string, bool>();

        // testing scriptingengine
        string script = @"// Args is a global variable
var result = Args.Count;
// Return results to caller
return result;";
        var scriptResult = ScriptExecutioner.ExecuteCsxFromString<int>(script, "a", "b");
        
        result.Add("ScriptingEngine", scriptResult == 2);

        return result;
    }
}