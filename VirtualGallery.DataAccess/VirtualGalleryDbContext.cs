using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using VirtualGallery.BusinessLogic.Categories;
using VirtualGallery.BusinessLogic.Pictures;

namespace VirtualGallery.DataAccess
{
    public class VirtualGalleryDbContext : DbContext
    {
        public VirtualGalleryDbContext()
        {
        }

        public VirtualGalleryDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            LoadMappingConfigurations(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Dynamically load all configuration
        /// </summary>
        /// <param name="modelBuilder"></param>
        private void LoadMappingConfigurations(DbModelBuilder modelBuilder)
        {
            var typesToRegister =
                Assembly.GetCallingAssembly()
                        .GetTypes()
                        .Where(type => !string.IsNullOrEmpty(type.Namespace))
                        .Where(
                            type =>
                            type.BaseType != null && !type.IsAbstract && type.BaseType.IsGenericType
                            && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
        }
    }
}