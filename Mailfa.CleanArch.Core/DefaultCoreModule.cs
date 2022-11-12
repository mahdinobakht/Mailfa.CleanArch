using Autofac;
using Mailfa.CleanArch.Core.Interfaces;
using Mailfa.CleanArch.Core.Services;

namespace Mailfa.CleanArch.Core;

public class DefaultCoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UserService>()
       .As<IUserService>().InstancePerLifetimeScope();


        builder.RegisterType<HmAccountsService>()
        .As<IHmAccountsService>().InstancePerLifetimeScope();


        builder.RegisterType<DriveService>()
     .As<IDriveService>().InstancePerLifetimeScope();


    }
}
