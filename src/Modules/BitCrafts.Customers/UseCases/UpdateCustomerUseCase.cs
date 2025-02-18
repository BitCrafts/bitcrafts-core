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
        _eventAggregator.Subscribe<CustomerUpdateEvent>(ExecuteCreateCustomer);
    }

    private Task ExecuteCreateCustomer(CustomerUpdateEvent arg)
    {
        return Task.Run(async () => { await _customerRepository.UpdateAsync(arg.NewCustomer); });
    }

    public void Dispose()
    {
        _eventAggregator.Unsubscribe<CustomerUpdateEvent>(ExecuteCreateCustomer);
    }
}