using Autofac;
using SimpleChat.DataAccess;

namespace SimpleChat.BusinessLogic.Configuration
{
    internal static class RegisterModulesExtension
    {
        public static void RegisterModules(this ContainerBuilder builder)
        {            
            builder.RegisterModule<DataAccessModule>();
        }
    }
}
