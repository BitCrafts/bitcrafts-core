using System.Collections.Concurrent;
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Infrastructure.Abstraction.Threading;
using BitCrafts.Infrastructure.Threading;

namespace BitCrafts.Infrastructure.Events;

public sealed class EventAggregator : IEventAggregator
{
    private class EventHandlerWrapper
    {
        public Func<object, Task> Handler { get; }

        public EventHandlerWrapper(Func<object, Task> handler)
        {
            Handler = handler;
        }

        public override bool Equals(object obj)
        {
            if (obj is EventHandlerWrapper other)
            {
                return Handler == other.Handler;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Handler.GetHashCode();
        }
    }


    private readonly IParallelism _parallelismService;
    private readonly ConcurrentDictionary<Type, List<EventHandlerWrapper>> _handlers = new();
    private readonly object _lock = new();
    private readonly LimitedThreadScheduler _taskScheduler;

    public EventAggregator(IParallelism parallelismService)
    {
        _parallelismService = parallelismService;
        _taskScheduler = new LimitedThreadScheduler(parallelismService);
    }

    public void Subscribe<TEvent>(Func<TEvent, Task> handler) where TEvent : IEvent
    {
        lock (_lock)
        {
            if (!_handlers.ContainsKey(typeof(TEvent)))
            {
                _handlers[typeof(TEvent)] = new List<EventHandlerWrapper>();
            }

            _handlers[typeof(TEvent)].Add(new EventHandlerWrapper(async e => await handler((TEvent)e)));
        }
    }

    public void Unsubscribe<TEvent>(Func<TEvent, Task> handler) where TEvent : IEvent
    {
        lock (_lock)
        {
            if (_handlers.ContainsKey(typeof(TEvent)))
            {
                var handlerToRemove = _handlers[typeof(TEvent)].FirstOrDefault(h =>
                    h.Handler ==
                    (Func<object, Task>)(async e => await handler((TEvent)e)));

                if (handlerToRemove != null)
                {
                    _handlers[typeof(TEvent)].Remove(handlerToRemove);
                }
            }
        }
    }


    public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
    {
        List<EventHandlerWrapper> handlers;
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
            Task.Factory.StartNew(() => handler.Handler(@event), CancellationToken.None, TaskCreationOptions.None,
                _taskScheduler);
        }
    }
}