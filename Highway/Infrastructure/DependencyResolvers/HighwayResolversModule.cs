using Autofac;
using Highway.DAL.DataSource;
using Highway.DAL.Helpers;
using Highway.DAL.Repositories.TollStations;
using Highway.Repositories.TollStations;

namespace Highway.Infrastructure.DependencyResolvers
{
    public class HighwayResolversModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StubDataRepository>().As<IStubDataRepository>().SingleInstance();

            builder.RegisterType<TollStationRepository>().As<ITollStationRepository>().InstancePerDependency();
            builder.RegisterType<DateTimeProvider>().As<IDateTimeProvider>().SingleInstance();
        }
    }
}
