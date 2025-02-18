using BitCrafts.Customers.Abstraction.Events;
using BitCrafts.Customers.Abstraction.Repositories;
using BitCrafts.Customers.Abstraction.UseCases;
using BitCrafts.Infrastructure.Abstraction.Events;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Customers.UseCases;

public sealed class CreateCustomerUseCase : ICreateCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IEventAggregator _eventAggregator;
    private readonly ILogger<CreateCustomerUseCase> _logger;

    public CreateCustomerUseCase(ICustomerRepository customerRepository,
        IEventAggregator eventAggregator,
        ILogger<CreateCustomerUseCase> logger)
    {
        _customerRepository = customerRepository;
        _eventAggregator = eventAggregator;
        _logger = logger;
        _eventAggregator.Subscribe<CustomerCreateEvent>(ExecuteCreateCustomer);
    }

    private Task ExecuteCreateCustomer(CustomerCreateEvent arg)
    {
        return Task.Run(async () => { await _customerRepository.AddAsync(arg.Customer); });
    }

    public void Dispose()
    {
        _eventAggregator.Unsubscribe<CustomerCreateEvent>(ExecuteCreateCustomer);
    }
}