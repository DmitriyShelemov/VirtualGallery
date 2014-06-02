using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace VirtualGallery.Web.Extensions.HtmlHelpers
{
    public static class HtmlHelperExtensions
    {
        //public static MvcHtmlString GenerateCaptcha(this HtmlHelper htmlHelper)
        //{
        //    const string RecaptchaId = "recaptcha";
        //    const string ValidationClass = RecaptchaId + "-field-validation-error";

        //    var sb = new StringBuilder();
        //    sb.Append(
        //        @"<script>function recaptchaValidationVisible(visible){ if (visible) $('." + ValidationClass
        //        + "').css('display',''); else $('." + ValidationClass + "').css('display','none'); }</script>");
        //    sb.Append(
        //        "<span class=\"" + ValidationClass + " field-validation-error\" data-valmsg-for=\"" + RecaptchaId
        //        + "\" style=\"display:none\"><span>" + "Localization.RemindPassword_WrongCaptcha" + "</span></span>");
        //    sb.Append(htmlHelper.GenerateCaptcha(RecaptchaId, "clean"));

        //    return new MvcHtmlString(sb.ToString());
        //}

        public static MvcHtmlString Loader(this HtmlHelper htmlHelper, UrlHelper url, string loaderId)
        {
            var tag = new TagBuilder("div");
            tag.AddCssClass("ajax-loader");
            if (!string.IsNullOrEmpty(loaderId))
            {
                tag.GenerateId(loaderId);
            }

            var img = new TagBuilder("img");
            img.MergeAttribute("src", url.Content("~/Content/img/ajax-loader.gif"));

			tag.InnerHtml = img.ToString(TagRenderMode.SelfClosing);

			return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString RadioButtonListFor<TModel, TValue>(
			this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, SelectList selectList, IDictionary<string, object> htmlAttributes = null, string suffix = "")
        {
            string fullHtmlFieldName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));
			return htmlHelper.RadioButtonList(fullHtmlFieldName, selectList, htmlAttributes, suffix);
        }

        public static MvcHtmlString RadioButtonList(this HtmlHelper htmlHelper, string name, SelectList list, IDictionary<string, object> htmlAttributes = null, string suffix = "")
        {
            var resultBuilder = new StringBuilder();
            int index = 0;

            var attributes = htmlAttributes ?? new Dictionary<string, object>();

            foreach (SelectListItem item in list)
            {
				string clientId = TagBuilder.CreateSanitizedId(String.Format("{0}{2}_{1}", name, index, suffix));

				attributes["id"] = clientId;
				
                MvcHtmlString radioButton = htmlHelper.RadioButton(name, item.Value, item.Selected, attributes);
				
                var labelTagBuilder = new TagBuilder("label");
                labelTagBuilder.Attributes.Add("for", clientId);
	            if (item.Text.Trim().Length > 0)
	            {
		            labelTagBuilder.SetInnerText(item.Text);
	            }
	            else
				{
					labelTagBuilder.InnerHtml = "&nbsp;";
	            }

	            resultBuilder.Append(radioButton);
				resultBuilder.Append(labelTagBuilder);
                index++;
            }
            return new MvcHtmlString(resultBuilder.ToString());
        }


        #region UploadButtonFor Html Helper
        public static MvcHtmlString UploadButtonFor<TModel, TProperty>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TProperty>> expression,
            string textHtml,
            object inputHtmlAttributes,
            object buttonHtmlAttributes,
            object wrapperHtmlAttributes)
        {
            // = 
            // = new { @class = "upload-wrapper" }
            var htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(inputHtmlAttributes);
            htmlAttributes["type"] = "file";
            MvcHtmlString normal = html.TextBoxFor(expression, htmlAttributes);
            if (normal != null)
            {
                var tagBuilder1 = new TagBuilder("div");
                tagBuilder1.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(wrapperHtmlAttributes));

                var tagBuilder2 = new TagBuilder("label");
                tagBuilder2.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(buttonHtmlAttributes));

                tagBuilder2.InnerHtml = textHtml + normal.ToHtmlString();
                tagBuilder1.InnerHtml = tagBuilder2.ToString();

                return MvcHtmlString.Create(tagBuilder1.ToString());
            }
            return null;
        }

        public static MvcHtmlString UploadButtonFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string textHtml, object inputHtmlAttributes, object buttonHtmlAttributes)
        {
            return UploadButtonFor<TModel, TProperty>(html, expression, textHtml, inputHtmlAttributes, buttonHtmlAttributes, new { @class = "upload-wrapper" });
        }

        public static MvcHtmlString UploadButtonFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string textHtml, object inputHtmlAttributes)
        {
            return UploadButtonFor<TModel, TProperty>(html, expression, textHtml, inputHtmlAttributes, new { @class = "btn btn-new btn-upload" });
        }

        public static MvcHtmlString UploadButtonFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string textHtml)
        {
            return UploadButtonFor<TModel, TProperty>(html, expression, textHtml, new { type = "file" });
        }

        public static MvcHtmlString UploadButtonFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            return UploadButtonFor<TModel, TProperty>(html, expression, "Upload File");
        }

        #endregion UploadButtonFor Html Helper
    }
}