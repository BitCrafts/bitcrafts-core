using BitCrafts.Customers.Abstraction.Events;
using BitCrafts.Customers.Abstraction.Repositories;
using BitCrafts.Customers.Abstraction.UseCases;
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.UseCases.Abstraction;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Customers.UseCases;

public sealed class AssignCustomerGroupUseCase :
    BaseUseCase<CustomerGroupAssignEvent, CustomerGroupAssignedEvent>,
    IAssignCustomerGroupUseCase
{
    private readonly ICustomerGroupRepository _customerGroupRepository;
    private readonly ICustomerRepository _customerRepository;

    public AssignCustomerGroupUseCase(ILogger<BaseUseCase<CustomerGroupAssignEvent, CustomerGroupAssignedEvent>> logger,
        IEventAggregator eventAggregator, ICustomerGroupRepository customerGroupRepository,
        ICustomerRepository customerRepository)
        : base(logger, eventAggregator)
    {
        _customerGroupRepository = customerGroupRepository;
        _customerRepository = customerRepository;
    }

    protected override async Task ExecuteCoreAsync(CustomerGroupAssignEvent createEvent)
    {
        var customerGroupId = createEvent.CustomerGroupId;
        await _customerRepository.AssignCustomerToGroup(createEvent.CustomerId, createEvent.CustomerGroupId);
    }
}