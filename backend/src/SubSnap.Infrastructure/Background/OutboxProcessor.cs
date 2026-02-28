using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SubSnap.Infrastructure.Persistence.Context;
using System.Text.Json;

namespace SubSnap.Infrastructure.Background;

public sealed class OutboxProcessor : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMediator _mediator;

    public OutboxProcessor(
        IServiceScopeFactory scopeFactory,
        IMediator mediator)
    {
        _scopeFactory = scopeFactory;
        _mediator = mediator;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();

            var db = scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();

            var messages = await db.OutboxMessages
                .Where(x => x.ProcessedOnUtc == null)
                .Take(20)
                .ToListAsync(stoppingToken);

            foreach (var msg in messages)
            {
                var type = Type.GetType(msg.Type)!;

                var domainEvent =
                    JsonSerializer.Deserialize(
                        msg.Payload, type);

                await _mediator.Publish(
                    (INotification)domainEvent!,
                    stoppingToken);

                msg.ProcessedOnUtc = DateTime.UtcNow;
            }

            await db.SaveChangesAsync(stoppingToken);

            await Task.Delay(2000, stoppingToken);
        }
    }
}
