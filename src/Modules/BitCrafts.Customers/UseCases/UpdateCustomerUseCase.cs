using BitCrafts.Customers.Abstraction.Events;
using BitCrafts.Customers.Abstraction.Repositories;
using BitCrafts.Customers.Abstraction.UseCases;
using BitCrafts.Infrastructure.Abstraction.Events;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Customers.UseCases;

public class UpdateCustomerUseCase : IUpdateCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IEventAggregator _eventAggregator;
    private readonly ILogger<CreateCustomerUseCase> _logger;

    public UpdateCustomerUseCase(ICustomerRepository customerRepository,
        IEventAggregator eventAggregator,
        ILogger<CreateCustomerUseCase> logger)
    {
        _customerRepository = customerRepository;
        _eventAggregator = eventAggregator;
        _logger = logger;
        _eventAggregator.Subscribe<CustomerUpdateEventRequest>(ExecuteCreateCustomer);
    }

    private Task ExecuteCreateCustomer(CustomerUpdateEventRequest arg)
    {
        return Task.Run(async () => { await _customerRepository.UpdateAsync(arg.Customer); });
    }

    public void Dispose()
    {
        _eventAggregator.Unsubscribe<CustomerUpdateEventRequest>(ExecuteCreateCustomer);
    }
}