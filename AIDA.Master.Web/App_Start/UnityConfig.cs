using AIDA.Master.Infrastucture.Data;
using AIDA.Master.Service.Identities;
using Microsoft.AspNet.Identity;
using Radyalabs.Core.Repository;
using System;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Unity.Lifetime;

namespace AIDA.Master.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    //public static class UnityConfig
    //{
    //    #region Unity Container
    //    private static Lazy<IUnityContainer> container =
    //      new Lazy<IUnityContainer>(() =>
    //      {
    //          var container = new UnityContainer();
    //          RegisterTypes(container);
    //          return container;
    //      });

    //    /// <summary>
    //    /// Configured Unity Container.
    //    /// </summary>
    //    public static IUnityContainer Container => container.Value;
    //    #endregion

    //    /// <summary>
    //    /// Registers the type mappings with the Unity container.
    //    /// </summary>
    //    /// <param name="container">The unity container to configure.</param>
    //    /// <remarks>
    //    /// There is no need to register concrete types such as controllers or
    //    /// API controllers (unless you want to change the defaults), as Unity
    //    /// allows resolving a concrete type even if it was not previously
    //    /// registered.
    //    /// </remarks>
    //    public static void RegisterTypes(IUnityContainer container)
    //    {
    //        // NOTE: To load from web.config uncomment the line below.
    //        // Make sure to add a Unity.Configuration to the using statements.
    //        // container.LoadConfiguration();

    //        // TODO: Register your type's mappings here.
    //        // container.RegisterType<IProductRepository, ProductRepository>();
    //    }
    //}

    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            //container.RegisterType<IUnitOfWork, UnitOfWork<_AIDAEntities>>(new HierarchicalLifetimeManager(), new InjectionConstructor("_AIDAEntities"));
            container.RegisterType<IUserStore<IdentityUser, Guid>, UserStore>(new TransientLifetimeManager());
            //container.RegisterType<RoleStore>(new TransientLifetimeManager());


            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

    }
}