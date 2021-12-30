using Autofac;
using Microsoft.EntityFrameworkCore;
using SimpleChat.DataAccess.DBContext;
using SimpleChat.DataAccess.Repository;
using SimpleChat.DataAccess.Repository.Base;

namespace SimpleChat.DataAccess
{
    public sealed class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterGeneric(typeof(EFRepository<,>)).As(typeof(IRepository<,>)).InstancePerDependency();
            builder.RegisterType<SimpleChatContext>().As<SimpleChatContext>()
                .WithParameter("options",
                new DbContextOptionsBuilder<SimpleChatContext>()
                .UseInMemoryDatabase(databaseName: "SimpleChatTestDb").Options)
                .InstancePerDependency();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerDependency();
        }
    }
}
