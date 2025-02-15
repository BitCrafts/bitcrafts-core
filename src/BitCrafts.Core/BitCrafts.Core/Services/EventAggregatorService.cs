using System.Collections.Concurrent;
using BitCrafts.Core.Contracts.Events;
using BitCrafts.Core.Contracts.Services;
using BitCrafts.Core.Threading;

namespace BitCrafts.Core.Services;

public sealed class EventAggregatorService : IEventAggregatorService
{
    private readonly IParallelismService _parallelismService;
    private readonly ConcurrentDictionary<Type, List<Func<object, Task>>> _handlers = new();
    private readonly object _lock = new();
    private readonly LimitedConcurrencyTaskScheduler _taskScheduler;

    public EventAggregatorService(IParallelismService parallelismService)
    {
        _parallelismService = parallelismService;
        _taskScheduler = new LimitedConcurrencyTaskScheduler(parallelismService);
    }

    public void Subscribe<TEvent>(Func<TEvent, Task> handler) where TEvent : IEvent
    {
        lock (_lock)
        {
            if (!_handlers.ContainsKey(typeof(TEvent)))
            {
                _handlers[typeof(TEvent)] = new List<Func<object, Task>>();
            }

            _handlers[typeof(TEvent)].Add(async e => await handler((TEvent)e));
        }
    }

    public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
    {
        List<Func<object, Task>> handlers;
        lock (_lock)
        {
            if (!_handlers.ContainsKey(typeof(TEvent)))
            {
                return;
            }

            handlers = _handlers[typeof(TEvent)].ToList();
        }

        foreach (var handler in handlers)
        {
            Task.Factory.StartNew(() => handler(@event), CancellationToken.None, TaskCreationOptions.None,
                _taskScheduler);
        }
    }
}