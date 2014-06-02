using Autofac;
using Autofac.Builder;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using VirtualGallery.BusinessLogic;
using VirtualGallery.BusinessLogic.Categories;
using VirtualGallery.BusinessLogic.Categories.Interfaces;
using VirtualGallery.BusinessLogic.EMail;
using VirtualGallery.BusinessLogic.EMail.Interfaces;
using VirtualGallery.BusinessLogic.Pictures;
using VirtualGallery.BusinessLogic.Pictures.Interfaces;
using VirtualGallery.BusinessLogic.StoredFiles;
using VirtualGallery.BusinessLogic.StoredFiles.Interfaces;
using VirtualGallery.BusinessLogic.UnitOfWork;
using VirtualGallery.BusinessLogic.WorkContext;
using VirtualGallery.DataAccess;
using VirtualGallery.DataAccess.Repository;
using VirtualGallery.DataAccess.Repository.Categories;
using VirtualGallery.DataAccess.Repository.Pictures;
using VirtualGallery.DataAccess.Repository.StoredFiles;
using VirtualGallery.DataAccess.UnitOfWork;
using VirtualGallery.Web.Infrastructure.DataAccess;
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
            InstancePerRequest(builder.RegisterType<CategoryService>().As<ICategoryService>());
            InstancePerRequest(builder.RegisterType<PictureService>().As<IPictureService>());
            InstancePerRequest(builder.RegisterType<FileStorage>().As<IFileStorage>());

            // DataAccess
            InstancePerRequest(builder.RegisterType<WebDbContextProvider>().As<IDbContextProvider>());
            InstancePerRequest(builder.RegisterType<UnitOfWorkFactory>().As<IUnitOfWorkFactory>());

            // Reposity
            InstancePerRequest(builder.RegisterGeneric(typeof(BaseRepository<,>)).As(typeof(IBaseRepository<,>)));
            InstancePerRequest(builder.RegisterType<CategoryRepository>().As<ICategoryRepository>());
            InstancePerRequest(builder.RegisterType<PictureRepository>().As<IPictureRepository>());
            InstancePerRequest(builder.RegisterType<StoredFileRepository>().As<IStoredFileRepository>());

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
            Container = builder.Build();

            return Container;
        }
    }
}