using YOYO.HRMS.BusinessLogic.SystemManagement;
using YOYO.HRMS.DataAccess;
using YOYO.HRMS.Core.Repository;
using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using System.Reflection;
using YOYO.HRMS.DataAccess.SystemManagement;
using YOYO.HRMS.Utility;

namespace YOYO.HRMS.Web
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildAutoFacContainer();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static IContainer BuildAutoFacContainer()
        {
            var builder = new ContainerBuilder();

            //注册Controls
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(new Assembly[] { Assembly.Load("YOYO.HRMS.Web.SystemManagement") });



            builder.RegisterType<PetaPocoDBFactory>().As<IDBFactory>().InstancePerHttpRequest();
            builder.RegisterType<PetaPocoUnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<NlogLogger>().As<ILogger>();

            builder.RegisterAssemblyTypes(typeof(LocalizedViewRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(LocalizedViewService).Assembly)
                .Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();            

            var container = builder.Build();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();     
            //container.RegisterType<IDBFactory, PetaPocoDBFactory>(new HierarchicalLifetimeManager());
            //container.RegisterType<IUnitOfWork,PetaPocoUnitOfWork>();
            //container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            //container.RegisterType<IEmployeeService, EmployeeService>();


            return container;
        }
    }
}