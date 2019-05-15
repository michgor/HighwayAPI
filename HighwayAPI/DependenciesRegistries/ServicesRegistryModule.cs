using Autofac;
using Microsoft.Extensions.DependencyInjection;

namespace HighwayAPI.DependenciesRegistries
{
    public class ServicesRegistryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }
    }
}
