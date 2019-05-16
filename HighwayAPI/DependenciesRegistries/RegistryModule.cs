using Autofac;
using Highway.Infrastructure.DependencyResolvers;

namespace Highway.API.DependenciesRegistries
{
    public class RegistryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<HighwayResolversModule>();
        }
    }
}
