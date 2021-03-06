﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcSiteMapProvider;
using VirtualGallery.BusinessLogic.Categories;
using VirtualGallery.BusinessLogic.Categories.Interfaces;
using VirtualGallery.BusinessLogic.Exceptions;
using VirtualGallery.BusinessLogic.Pictures;
using VirtualGallery.BusinessLogic.Pictures.Interfaces;
using VirtualGallery.BusinessLogic.Preferences.Interfaces;
using VirtualGallery.BusinessLogic.WorkContext;
using VirtualGallery.Infrastructure.Localization;
using VirtualGallery.Web.Extensions;
using VirtualGallery.Web.Infrastructure.Filters;
using VirtualGallery.Web.Infrastructure.Presentation;
using VirtualGallery.Web.Models.Home;

namespace VirtualGallery.Web.Controllers
{
    public partial class HomeController : BaseController
    {
        private readonly ICategoryService _categoryService;

        private readonly IPictureService _pictureService;

        private readonly IPreferenceService _preferenceService;

        public HomeController(ICategoryService categoryService, 
            IPictureService pictureService,
            IPreferenceService preferenceService, 
            IWorkContext workContext)
            : base(workContext)
        {
            _categoryService = categoryService;
            _pictureService = pictureService;
            _preferenceService = preferenceService;
        }

        [MvcSiteMapNode(Title = NameConst, Key = NameConst, UpdatePriority = UpdatePriority.Absolute_100)]
        public virtual ActionResult Index()
        {
            ViewBag.AllowEdit = CurrentUser != null;
            var pref = _preferenceService.Get();

            return View(new HomePageModel
            {
                Title = Localization.Nav_Main_Title,
                Description = string.IsNullOrEmpty(pref.Intro) ? Localization.Home_Description : pref.Intro
            });
        }

        [GalleryAuthorize]
        [AjaxOnly]
        public virtual ActionResult SetIntro(HtmlModel model)
        {
            var pref = _preferenceService.Get();
            pref.Intro = model.Text;
            _preferenceService.Update(pref);

            return SuccessJson(null, true);
        }

        [AjaxOrChildActionOnly]
        public virtual ActionResult GetCategories(int page)
        {
            ViewBag.AllowEdit = CurrentUser != null;

            var model = new CategoryListModel
            {
                PageNumber = page,
                Categories = _categoryService.Get(page).ToList().Select(c => new CategoryModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Pictures = new PicturesModel
                    {
                        ContainerId = c.Id,
                        Items = c.Pictures.Where(p => !p.Sold && !p.Reserved && p.Topic).ToList().Select(p => GetPictureModel(p, p.Name)).ToList()
                    }
                    
                }).ToList()
            };

            return View(MVC.Home.Views._Categories, model);
        }

        [MvcSiteMapNode(Title = ActionNameConstants.Category, 
            Key = ActionNameConstants.Category, 
            ParentKey = NameConst,
            DynamicNodeProvider = "VirtualGallery.Web.Infrastructure.Dependency.GalleryDynamicNodeProvider, VirtualGallery.Web", 
            UpdatePriority = UpdatePriority.Absolute_050)]
        public virtual ActionResult Category(int categoryId)
        {
            var category = _categoryService.GetById(categoryId);
            if (category == null || category.Deleted)
                return RedirectToAction(MVC.Home.Index());

            ViewBag.AllowEdit = CurrentUser != null;
            var pref = _preferenceService.Get();

            var model = new CategoryPageModel
            {
                Title = category.Name,
                Description = string.IsNullOrEmpty(pref.Intro) ? Localization.Home_Description : pref.Intro,
                Category = new CategoryModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    Pictures = new PicturesModel
                    {
                        ContainerId = category.Id,
                        Items = category.Pictures.Where(p => !p.Sold && !p.Reserved).ToList().Select(p => GetPictureModel(p)).ToList()
                    }
                }
            };

            return View(model);
        }

        [GalleryAuthorize]
        [AjaxOnly]
        public virtual ActionResult AddCategory(string name)
        {
            var category = new Category { Name = name };
            _categoryService.Add(category);

            return SuccessJson(category.Id);
        }

        [GalleryAuthorize]
        [AjaxOnly]
        public virtual ActionResult EditCategory(int id, string name)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
                return FailedJson("Category not found");

            category.Name = name;
            _categoryService.Update(category);

            return SuccessJson(category.Id);
        }

        [GalleryAuthorize]
        [AjaxOnly]
        public virtual ActionResult DropCategory(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
                return FailedJson("Category not found");

            _categoryService.Remove(category);

            return SuccessJson(category.Id);
        }

        [GalleryAuthorize]
        [AjaxOnly]
        public virtual ActionResult MoveCategory(int id, bool up)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
                return FailedJson("Category not found");

            _categoryService.Move(category, up);

            return SuccessJson(category.Id);
        }

        [GalleryAuthorize]
        [HttpPost]
        public virtual ActionResult Upload(int categoryId, PictureUploadButtonModel model)
        {
                if (model.File != null && model.File.ContentLength <= 0)
                    return FailedJson(Localization.Common_Error_File_Has_No_Content, fixUploadIE: true);

                if (model.File != null && model.File.ContentLength > Picture.MaxFileSize)
                    return FailedJson(string.Format(Localization.Picture_BigFileSize, model.File.FileName), fixUploadIE: true);

                var savedPicture = _pictureService.Add(categoryId, model.File);

                return SuccessJson(
                            new List<PictureModel> { GetPictureModel(savedPicture) }, true);
        }

        [GalleryAuthorize]
        [AjaxOnly]
        public virtual ActionResult TopPicture(int id, bool top)
        {
            var picture = _pictureService.GetById(id);
            if (picture == null)
                return FailedJson("Picture not found");

            picture.Topic = top;
            _pictureService.Update(picture);

            return SuccessJson(picture.Id);
        }

        [AjaxOnly]
        public virtual ActionResult PreviewPicture(int id)
        {
            var picture = _pictureService.GetById(id);
            if (picture == null)
                throw new LocalizedValidationException("Picture not found");

            ViewBag.AllowEdit = CurrentUser != null;
            var model = GetPictureModel(picture);
            model.PriceDollar = Convert.ToInt32(picture.PriceDollar);
            model.PriceEuro = Convert.ToInt32(picture.PriceEuro);
            model.PriceRouble = Convert.ToInt32(picture.PriceRouble);
            return View(MVC.Home.Views._PicturePreviewDialog, model);
        }

        [GalleryAuthorize]
        [AjaxOnly]
        [HttpPost]
        public virtual ActionResult EditPicture(PictureModel model)
        {
            if (model == null)
                return FailedJson("Picture not found");

            var picture = _pictureService.GetById(model.Id);
            if (picture == null)
                return FailedJson("Picture not found");
            if (string.IsNullOrEmpty(model.Name))
                return FailedJson("Picture name is not valid");

            picture.Name = model.Name;
            picture.Description = model.Description;
            picture.Details = model.Details;
            picture.PriceRouble = model.PriceRouble;
            picture.PriceEuro = model.PriceEuro;
            picture.PriceDollar = model.PriceDollar;
            _pictureService.Update(picture);

            ViewBag.AllowEdit = CurrentUser != null;

            return View(MVC.Home.Views._Picture, GetPictureModel(picture));
        }

        private static PictureModel GetPictureModel(Picture p, string desc = null)
        {
            return new PictureModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = desc ?? p.Description,
                Details = p.Details,
                Price = GetPicturePrice(p),
                ThumbnailUrl = p.Thumbnail.ResolveFilePath(),
                OriginalUrl = p.File.ResolveFilePath(),
                Topic = p.Topic
            };
        }

        private static string GetPicturePrice(Picture p)
        {
            switch (Localization.GetCurrentLocalization().LanguageKey)
            {
                case LanguageKey.English:
                    return string.Format("{0:C}", p.PriceEuro);
                case LanguageKey.TestLocalization:
                    return string.Format("{0:C}", p.PriceDollar);
                case LanguageKey.Russian:
                default:
                    return string.Format("{0:C}", p.PriceRouble);
            }
        }
    }
}
