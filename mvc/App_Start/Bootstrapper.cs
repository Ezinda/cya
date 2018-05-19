using Autofac;
using Autofac.Integration.Mvc;
using ceya.Application.Service;
using ceya.Domain.Repository;
using ceya.Domain.Service;
using ceya.Infrastructure.DataAccess;
using ceya.Infrastructure.Repository;
using ceya.Infrastructure.Service;
using mvc.Mappings;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace mvc
{
    public static class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacContainer();
            
            // Configuracion de AutoMapper
            AutoMapperConfiguration.Configure();
        }

        private static void SetAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // UoW y Factory
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<ConnectionStringFactory>().As<IConnectionStringFactory>().SingleInstance();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerRequest();
            builder.RegisterType<contactoService>().As<IContactoService>().InstancePerRequest();

            // Repositorios y Servicios
            builder.RegisterAssemblyTypes(typeof(ArchivoRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(ArchivoService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();

            // Forms Authentication, actualmente no se usa
            // builder.RegisterAssemblyTypes(typeof(DefaultFormsAuthentication).Assembly)
            //     .Where(t => t.Name.EndsWith("Authentication"))
            //     .AsImplementedInterfaces().InstancePerRequest();

            // 
            // builder.Register(c => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new SocialGoalEntities())))
            //     .As<UserManager<ApplicationUser>>().InstancePerHttpRequest();

            builder.RegisterFilterProvider();
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}