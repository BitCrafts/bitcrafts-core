using BitCrafts.Customers.Abstraction;
using BitCrafts.Customers.Abstraction.Repositories;
using BitCrafts.Customers.Abstraction.UseCases;
using BitCrafts.Customers.Repositories;
using BitCrafts.Customers.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Customers;

public sealed class CustomerModule : ICustomerModule
{
    public string Name => "Customers";

    public void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<ICustomerRepository, CustomerRepository>();
        services.AddTransient<ICustomerGroupRepository, CustomerGroupRepository>();
        services.AddTransient<ICreateCustomerUseCase, CreateCustomerUseCase>();
        services.AddTransient<IUpdateCustomerUseCase, UpdateCustomerUseCase>();
        services.AddTransient<IDeleteCustomerUseCase, DeleteCustomerUseCase>();
    }
}