using Autofac;
using AutoMapper;
using SimpleChat.BusinessLogic.Services;
using SimpleChat.BusinessLogic.Services.Interfaces;

namespace SimpleChat.BusinessLogic.Configuration
{
    internal static class RegisterTypesExtension
    {
        public static void RegisterTypes(this ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>().InstancePerDependency();
            builder.RegisterType<TypeMapper>().As<Profile>();
        }
    }
}
