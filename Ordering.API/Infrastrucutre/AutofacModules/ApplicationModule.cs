using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using EventBusCommon.Abstractions;

namespace Ordering.API.Infrastrucutre.AutofacModules
{
    public class ApplicationModule
        : Autofac.Module
    {

        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }

        protected override void Load(ContainerBuilder builder)
        {

            //builder.Register(c => new OrderQueries(QueriesConnectionString))
            //    .As<IOrderQueries>()
            //    .InstancePerLifetimeScope();

            //builder.RegisterType<BuyerRepository>()
            //    .As<IBuyerRepository>()
            //    .InstancePerLifetimeScope();

            //builder.RegisterType<OrderRepository>()
            //    .As<IOrderRepository>()
            //    .InstancePerLifetimeScope();

            //builder.RegisterType<RequestManager>()
            //    .As<IRequestManager>()
            //    .InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(typeof(CreateOrderCommandHandler).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

        }
    }
}
