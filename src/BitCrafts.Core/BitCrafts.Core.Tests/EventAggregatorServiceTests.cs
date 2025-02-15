using BitCrafts.Core.Contracts.Services;
using BitCrafts.Core.Services;

namespace BitCrafts.Core.Tests;

[TestClass]
public class EventAggregatorTests
{
    private IEventAggregatorService _eventAggregator;

    [TestInitialize]
    public void Setup()
    {
        var parallelismService = new ParallelismService();
        _eventAggregator = new EventAggregatorService(parallelismService);
    }

    [TestMethod]
    public async Task Publish_WithNoSubscribers_DoesNotThrowException()
    {
        var testEvent = new TestEvent();

        // Act & Assert
        await Task.Run(() => _eventAggregator.Publish(testEvent));
    }

    [TestMethod]
    public void Subscribe_ThenPublish_CallsSubscribedHandler()
    {
        var testEvent = new TestEvent();
        var handlerCalled = false;

        _eventAggregator.Subscribe<TestEvent>(async evnt =>
        {
            handlerCalled = true;
            await Task.CompletedTask;
        });

        _eventAggregator.Publish(testEvent);
        Task.Delay(50).Wait();

        Assert.IsTrue(handlerCalled);
    }

    [TestMethod]
    public void Subscribe_MultipleTimes_Publish_CallsAllHandlers()
    {
        var testEvent = new TestEvent();
        var handler1Called = false;
        var handler2Called = false;

        _eventAggregator.Subscribe<TestEvent>(async evnt =>
        {
            handler1Called = true;
            await Task.CompletedTask;
        });

        _eventAggregator.Subscribe<TestEvent>(async evnt =>
        {
            handler2Called = true;
            await Task.CompletedTask;
        });

        _eventAggregator.Publish(testEvent);
        Task.Delay(50).Wait();

        Assert.IsTrue(handler1Called);
        Assert.IsTrue(handler2Called);
    }

    [TestMethod]
    public void Publish_WithDifferentEventTypes_DoesNotCallIncorrectHandlers()
    {
        var handlerCalled = false;

        _eventAggregator.Subscribe<TestEvent>(async evnt =>
        {
            handlerCalled = true;
            await Task.CompletedTask;
        });

        _eventAggregator.Publish(new AnotherTestEvent());
        Task.Delay(50).Wait();

        Assert.IsFalse(handlerCalled);
    }

    [TestMethod]
    public void Subscribe_MultipleHandlersForSameEvent_AllHandlersAreCalled()
    {
        var testEvent = new TestEvent();
        var handler1Called = false;
        var handler2Called = false;

        _eventAggregator.Subscribe<TestEvent>(async evnt =>
        {
            handler1Called = true;
            await Task.CompletedTask;
        });

        _eventAggregator.Subscribe<TestEvent>(async evnt =>
        {
            handler2Called = true;
            await Task.CompletedTask;
        });

        _eventAggregator.Publish(testEvent);
        Task.Delay(50).Wait();

        Assert.IsTrue(handler1Called);
        Assert.IsTrue(handler2Called);
    }
}