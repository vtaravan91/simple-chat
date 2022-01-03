using Autofac;
using AutoMapper;
using SimpleChat.BusinessLogic.Configuration;

namespace SimpleChat.BusinessLogic
{
    public sealed class BusinessLogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(ctx => new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(new TypeMapper());
            })).AsSelf().Keyed<MapperConfiguration>(nameof(BusinessLogicModule)).SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var configuration = context.Resolve<MapperConfiguration>();

                return configuration.CreateMapper(context.Resolve);
            }).As<IMapper>().InstancePerLifetimeScope();

            builder.RegisterTypes();
            builder.RegisterModules();
        }
    }
}
