using System;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
#if configurable
using EPiServer.ServiceLocation;
using Microsoft.Extensions.DependencyInjection;
#endif

namespace MyAppNamespace
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
#if configurable
    public class MyInitializationModule : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            // Add services configurations to context.Services
        }

#else
    public class MyInitializationModule : IInitializableModule
    {
#endif
        public void Initialize(InitializationEngine context)
        {
            // Add initialization logic, this method is called once after CMS has been initialized
        }

        public void Uninitialize(InitializationEngine context)
        {
            // Add uninitialization logic
        }
    }
}
