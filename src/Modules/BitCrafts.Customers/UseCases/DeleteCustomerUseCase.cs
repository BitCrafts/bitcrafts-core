using BitCrafts.Customers.Abstraction.Events;
using BitCrafts.Customers.Abstraction.Repositories;
using BitCrafts.Customers.Abstraction.UseCases;
using BitCrafts.Infrastructure.Abstraction.Events;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Customers.UseCases;

public sealed class DeleteCustomerUseCase : IDeleteCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IEventAggregator _eventAggregator;
    private readonly ILogger<CreateCustomerUseCase> _logger;

    public DeleteCustomerUseCase(ICustomerRepository customerRepository,
        IEventAggregator eventAggregator,
        ILogger<CreateCustomerUseCase> logger)
    {
        _customerRepository = customerRepository;
        _eventAggregator = eventAggregator;
        _logger = logger;
        _eventAggregator.Subscribe<CustomerDeleteEvent>(ExecuteCreateCustomer);
    }

    private Task ExecuteCreateCustomer(CustomerDeleteEvent arg)
    {
        return Task.Run(async () => { await _customerRepository.DeleteAsync(arg.Customer.Id); });
    }

    public void Dispose()
    {
        _eventAggregator.Unsubscribe<CustomerDeleteEvent>(ExecuteCreateCustomer);
    }
}