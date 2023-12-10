using Autofac.Integration.WebApi;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using BikesApp.Controllers.API;
using bikeStoreDb;

namespace BikesApp.App_Start
{
    public class IOCConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //register
            builder.RegisterType<hellolog>().As<ILog>();
            builder.RegisterType<bikeStoreDbService>().As<IDatabase>();
            builder.RegisterType<BikesDBAccessLayer>().AsSelf();
            var container = builder.Build();
            var resolve = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolve;
        }
    }
}