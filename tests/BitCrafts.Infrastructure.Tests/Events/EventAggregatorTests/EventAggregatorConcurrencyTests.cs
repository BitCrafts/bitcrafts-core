using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Infrastructure.Events;
using BitCrafts.Infrastructure.Threading;

namespace BitCrafts.Infrastructure.Tests.Events.EventAggregatorTests;

[TestClass]
public class EventAggregatorConcurrencyTests
{
    private IEventAggregator _eventAggregator;

    [TestInitialize]
    public void Setup()
    {
        var parallelismService = new Parallelism();
        _eventAggregator = new EventAggregator(parallelismService);
    }

    [TestMethod]
    public void Publish_FromMultipleThreads_ExecutesCorrectNumberOfHandlers()
    {
        var callCount = 0;
        var testEvent = new TestEvent();

        _eventAggregator.Subscribe<TestEvent>(async evnt =>
        {
            Interlocked.Increment(ref callCount);
            await Task.CompletedTask;
        });

        var threads = new List<Thread>();
        for (var i = 0; i < 50; i++) threads.Add(new Thread(() => { _eventAggregator.Publish(testEvent); }));

        foreach (var thread in threads) thread.Start();

        foreach (var thread in threads) thread.Join();

        Task.Delay(200).Wait();

        Assert.AreEqual(50, callCount);
    }

    [TestMethod]
    public void Subscribe_Publish_Simultaneously_HandlesConcurrencyCorrectly()
    {
        var testEvent = new TestEvent();
        var callCount = 0;
        var subscriptionsAdded = 0;
        var threads = new List<Thread>();

        for (var i = 0; i < 25; i++)
            threads.Add(new Thread(() =>
            {
                _eventAggregator.Subscribe<TestEvent>(async evnt =>
                {
                    Interlocked.Increment(ref callCount);
                    await Task.CompletedTask;
                });
                Interlocked.Increment(ref subscriptionsAdded);
            }));

        for (var i = 0; i < 25; i++) threads.Add(new Thread(() => { _eventAggregator.Publish(testEvent); }));

        foreach (var thread in threads) thread.Start();

        foreach (var thread in threads) thread.Join();

        Task.Delay(500).Wait();

        Assert.AreEqual(25, subscriptionsAdded);

        Assert.IsTrue(callCount >= 1);
    }
}