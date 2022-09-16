using Autofac;

using Service.Concretes;
using Service.Interfaces;

namespace BuildingBlocks.Modules;

public class MyApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerDependency();

        //builder.RegisterGeneric(typeof(ValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));
    }
}
