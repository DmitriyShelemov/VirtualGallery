﻿using System.Web.Hosting;
using System.Web.Routing;
using Autofac;
using Autofac.Builder;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MvcSiteMapProvider.Loader;
using MvcSiteMapProvider.Web.Mvc;
using MvcSiteMapProvider.Xml;
using VirtualGallery.BusinessLogic;
using VirtualGallery.BusinessLogic.Categories;
using VirtualGallery.BusinessLogic.Categories.Interfaces;
using VirtualGallery.BusinessLogic.EMail;
using VirtualGallery.BusinessLogic.EMail.Interfaces;
using VirtualGallery.BusinessLogic.Orders;
using VirtualGallery.BusinessLogic.Orders.Interfaces;
using VirtualGallery.BusinessLogic.Pictures;
using VirtualGallery.BusinessLogic.Pictures.Interfaces;
using VirtualGallery.BusinessLogic.Preferences;
using VirtualGallery.BusinessLogic.Preferences.Interfaces;
using VirtualGallery.BusinessLogic.StoredFiles;
using VirtualGallery.BusinessLogic.StoredFiles.Interfaces;
using VirtualGallery.BusinessLogic.UnitOfWork;
using VirtualGallery.BusinessLogic.WorkContext;
using VirtualGallery.DataAccess;
using VirtualGallery.DataAccess.Repository;
using VirtualGallery.DataAccess.Repository.Categories;
using VirtualGallery.DataAccess.Repository.Orders;
using VirtualGallery.DataAccess.Repository.Pictures;
using VirtualGallery.DataAccess.Repository.Preferences;
using VirtualGallery.DataAccess.Repository.StoredFiles;
using VirtualGallery.DataAccess.UnitOfWork;
using VirtualGallery.Web.Infrastructure.DataAccess;
using VirtualGallery.Web.Infrastructure.Dependency.Autofac.Modules;
using VirtualGallery.Web.Infrastructure.WorkContext;

namespace VirtualGallery.Web.Infrastructure.Dependency
{
    public class GalleryDependencyResolver : IDependencyResolver
    {
        private readonly IDependencyResolver _dependencyResolver;

        public GalleryDependencyResolver(bool useBuiltInResolver = true)
        {
            if (useBuiltInResolver)
                _dependencyResolver = new AutofacDependencyResolver(BuildContainer());
        }

        public IContainer Container
        {
            get;
            private set;
        }

        public virtual object GetService(Type serviceType)
        {
            return _dependencyResolver.GetService(serviceType);
        }

        public virtual IEnumerable<object> GetServices(Type serviceType)
        {
            return _dependencyResolver.GetServices(serviceType);
        }

        protected virtual void RegisterEndpoints(ContainerBuilder builder)
        {
            // Controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
        }

        protected virtual void RegisterServices(ContainerBuilder builder)
        {
            // WorkContext
            InstancePerRequest(builder.RegisterType<WebWorkContext>().As<IWorkContext>());
            
            // Services
            InstancePerRequest(builder.RegisterType<PreferenceService>().As<IPreferenceService>());
            InstancePerRequest(builder.RegisterType<CategoryService>().As<ICategoryService>());
            InstancePerRequest(builder.RegisterType<PictureService>().As<IPictureService>());
            InstancePerRequest(builder.RegisterType<ShoppingCartService>().As<IShoppingCartService>());
            InstancePerRequest(builder.RegisterType<FileStorage>().As<IFileStorage>());

            // DataAccess
            InstancePerRequest(builder.RegisterType<WebDbContextProvider>().As<IDbContextProvider>());
            InstancePerRequest(builder.RegisterType<UnitOfWorkFactory>().As<IUnitOfWorkFactory>());

            // Reposity
            InstancePerRequest(builder.RegisterGeneric(typeof(BaseRepository<,>)).As(typeof(IBaseRepository<,>)));
            InstancePerRequest(builder.RegisterType<CategoryRepository>().As<ICategoryRepository>());
            InstancePerRequest(builder.RegisterType<PictureRepository>().As<IPictureRepository>());
            InstancePerRequest(builder.RegisterType<PreferenceRepository>().As<IPreferenceRepository>());
            InstancePerRequest(builder.RegisterType<StoredFileRepository>().As<IStoredFileRepository>());
            InstancePerRequest(builder.RegisterType<OrderRepository>().As<IOrderRepository>());
            InstancePerRequest(builder.RegisterType<LotRepository>().As<ILotRepository>());

            //Mailing
            SingleInstance(builder.RegisterType<MessageQueue>().As<IMessageQueue>());
            SingleInstance(builder.RegisterType<SmtpClientFactory>().As<ISmtpClientFactory>());
            SingleInstance(builder.RegisterType<MailBox>().As<IMailBox>());
        }

        protected virtual void SingleInstance<TLimit, TActivatorData, TStyle>(IRegistrationBuilder<TLimit, TActivatorData, TStyle> registrationBuilder)
        {
            registrationBuilder.SingleInstance();
        }

        protected virtual void InstancePerRequest<TLimit, TActivatorData, TStyle>(IRegistrationBuilder<TLimit, TActivatorData, TStyle> registrationBuilder)
        {
            registrationBuilder.InstancePerHttpRequest();
        }

        protected IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();
            RegisterEndpoints(builder);
            RegisterServices(builder);
            // Register modules
            builder.RegisterModule(new MvcSiteMapProviderModule()); // Required
            builder.RegisterModule(new MvcModule()); // Required by MVC. Typically already part of your setup (double check the contents of the module).

            Container = builder.Build();
            // Setup global sitemap loader (required)
            MvcSiteMapProvider.SiteMaps.Loader = Container.Resolve<ISiteMapLoader>();
            //SiteMapLoader
            return Container;
        }
    }
}