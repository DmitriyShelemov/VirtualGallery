using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace VirtualGallery.Web.Extensions
{
    public static class EnumExtensions
    {
        public static SelectList ToSelectList<TEnum>(
            this TEnum enumObj, string titleFormat = "{0}", IEnumerable<TEnum> exceptValues = null, bool numeric = false) where TEnum : struct
        {
	        IEnumerable<TEnum> values;
	        var fields = GetFields(exceptValues, out values);

	        var items = from value in values
                        from field in fields
                        let descriptionAttribute =
                            field.GetCustomAttributes(typeof(DescriptionAttribute), true)
                                 .OfType<DescriptionAttribute>()
                                 .FirstOrDefault()
                        let description =
							(descriptionAttribute != null) ? descriptionAttribute.Description : value.ToString()
						let numericVal = (int)field.GetValue(value)
                        where value.ToString() == field.Name
                        select new
                                   {
									   Id = numeric ? (object)numericVal : (object)value,
                                       Name = string.Format(titleFormat, description)
                                   };
            return new SelectList(items, "Id", "Name", enumObj);
        }

	    public static IEnumerable<KeyValuePair<int, string>> ToLocalizedList<TEnum>(
			this TEnum enumObj, string titleFormat = "{0}", IEnumerable<TEnum> exceptValues = null) where TEnum : struct
		{
			IEnumerable<TEnum> values;
			var fields = GetFields(exceptValues, out values);

			var items = from value in values
			            from field in fields
			            let descriptionAttribute =
				            field.GetCustomAttributes(typeof (DescriptionAttribute), true)
				                 .OfType<DescriptionAttribute>()
				                 .FirstOrDefault()
			            let description =
				            (descriptionAttribute != null) ? descriptionAttribute.Description : value.ToString()
			            where value.ToString() == field.Name
			            select new KeyValuePair<int, string>(Convert.ToInt32(value), description);
			return items;
		}

		public static string ToLocalizedString<TEnum>(this TEnum enumObj)
		{
			var enumType = typeof(TEnum);
			var field = enumType.GetField(enumObj.ToString());

			var descriptionAttribute = field.GetCustomAttributes(typeof (DescriptionAttribute), true)
			     .OfType<DescriptionAttribute>()
			     .FirstOrDefault();

			return descriptionAttribute != null ? descriptionAttribute.Description : enumObj.ToString();
		}


		public static TEnum Parse<TEnum>(this TEnum enumObj, string value) where TEnum : struct
		{
			return (TEnum)Enum.Parse(enumObj.GetType(), value);
		}

		private static IEnumerable<FieldInfo> GetFields<TEnum>(IEnumerable<TEnum> exceptValues, out IEnumerable<TEnum> values)
		{
			var enumType = typeof(TEnum);
			var fields = enumType.GetFields(BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public);
			values = Enum.GetValues(enumType).OfType<TEnum>();
			if (exceptValues != null)
			{
				values = values.Except(exceptValues);
			}
			return fields;
		}
    }
}