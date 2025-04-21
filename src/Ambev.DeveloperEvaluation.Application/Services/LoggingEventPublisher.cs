using Ambev.DeveloperEvaluation.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Services;

public class LoggingEventPublisher : IEventPublisher
{
    private readonly ILogger<LoggingEventPublisher> _logger;

    public LoggingEventPublisher(ILogger<LoggingEventPublisher> logger)
    {
        _logger = logger;
    }

    public Task Publish<TEvent>(TEvent @event) where TEvent : class
    {
        _logger.LogInformation("Event Published: {EventType} - {EventData}",
            @event.GetType().Name,
            JsonSerializer.Serialize(@event));

        return Task.CompletedTask;
    }
}