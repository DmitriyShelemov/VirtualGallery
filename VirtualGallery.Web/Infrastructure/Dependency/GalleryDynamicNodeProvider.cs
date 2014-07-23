using System.Collections.Generic;
using System.Web.Mvc;
using MvcSiteMapProvider;
using VirtualGallery.BusinessLogic.Categories.Interfaces;
using VirtualGallery.Web.Controllers;

namespace VirtualGallery.Web.Infrastructure.Dependency
{
    public class GalleryDynamicNodeProvider: DynamicNodeProviderBase
    {
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode node)
        {
            switch (node.Key)
            {
                case HomeController.ActionNameConstants.Category:
                    var categoryService = DependencyResolver.Current.GetService(typeof(ICategoryService)) as ICategoryService;
                    // Create a node for each product
                    foreach (var category in categoryService.Get(0))
                    {
                        var dynamicNode = new DynamicNode("Category_" + category.Id, category.Name);
                        // Preserve our route parameter explicitly
                        dynamicNode.RouteValues.Add("categoryId", category.Id);

                        yield return dynamicNode;
                    }
                    break;
            }

        }
    }
}