using Autofac;
using SimpleChat.BusinessLogic.Configuration;

namespace SimpleChat.BusinessLogic
{
    public sealed class BusinessLogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterTypes();
            builder.RegisterModules();
        }
    }
}
