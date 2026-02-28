namespace SubSnap.API.Middleware.Correlation;

/*
 * Assegna automaticamente un Id ad ogni richiesta, lo include nella response e lo rende disponibile nei log. Utile per tracciare le richieste in scenari complessi (e.g. microservizi)!! xk senza vedi in debug i log di 1000 richieste che arrivano, con questo Id puoi filtrare e vedere solo i log relativi a quella richiesta specifica!!
 * e.g. nei logs vedi ora '[CorrelationId=a12f-88dd] User registered' 
 */
public sealed class CorrelationIdMiddleware
{
    private const string Header = "X-Correlation-Id";

    private readonly RequestDelegate _next;
    private readonly ILogger<CorrelationIdMiddleware> _logger;

    public CorrelationIdMiddleware(
        RequestDelegate next,
        ILogger<CorrelationIdMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var correlationId =
            context.Request.Headers[Header].FirstOrDefault()
            ?? Guid.NewGuid().ToString();

        context.Response.Headers[Header] = correlationId;

        using (_logger.BeginScope(
            new Dictionary<string, object>
            {
                ["CorrelationId"] = correlationId
            }))
        {
            await _next(context);
        }
    }
}