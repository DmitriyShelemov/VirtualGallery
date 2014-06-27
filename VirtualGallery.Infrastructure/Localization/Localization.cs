
using System;
using System.Collections.Generic;
using System.Globalization;

namespace VirtualGallery.Infrastructure.Localization
{
    public static class Localization 
    {		
		private static readonly LocalizationManager _localizationManager = LocalizationManager.GetInstance();

		[Obsolete("Please, refactor all usings of Localization.Hardcoded")]
		public static string Hardcoded(string text)
		{
			return text;
		}

		public static List<LocalizationSettings> GetAvalibleLocalizations()
        {
            return _localizationManager.GetAvalibleLocalizations();
        }
	
		public static LocalizationSettings GetLocalizationByCulture(string cultureName)
		{
		    return _localizationManager.GetLocalizationByCulture(cultureName);
		}

		public static LocalizationSettings GetLocalizationByLanguageKey(LanguageKey languageKey)
		{
			return _localizationManager.GetLocalizationByLanguageKey(languageKey);
		}
		
		public static LocalizationSettings GetLocalizationByLocalizationName(string localizationName)
        {
            return _localizationManager.GetLocalizationByLocalizationName(localizationName);
        }

		public static CultureInfo GetCultureInfo(LanguageKey languageKey)
		{
		   return _localizationManager.GetCultureInfo(languageKey);
		}

		public static LocalizationSettings GetCurrentLocalization()
		{
			return _localizationManager.GetCurrentLocalization();
		}

		public static void SetCurrentLocalization(LocalizationSettings localization)
		{
			_localizationManager.SetCurrentLocalization(localization);
		}

		public static LocalizationSettings GetDefaultLocalization()
		{
			return _localizationManager.GetDefaultLocalization();
		}
		
		public static string GetString(string key)
        {
            return _localizationManager.GetString(key);            
        }		

		public static string GetString(string key, LanguageKey languageKey)
        {
            return _localizationManager.GetString(key, languageKey);            
        }	

		public static void AddTestLocalizationLanguage()
		{
			_localizationManager.AddTestLocalizationLanguage();            
		}
			
		public class Names
		{
			public const string Language_English = "Language_English";
			public const string Language_Russian = "Language_Russian";
			public const string Language_Language = "Language_Language";
			public const string Nav_Prices = "Nav_Prices";
			public const string Nav_Main = "Nav_Main";
			public const string Nav_Main_Title = "Nav_Main_Title";
			public const string Nav_ShoppingCart = "Nav_ShoppingCart";
			public const string Side_About = "Side_About";
			public const string Side_Contact = "Side_Contact";
			public const string Side_Decor = "Side_Decor";
			public const string Side_Delivery = "Side_Delivery";
			public const string Home_Description = "Home_Description";
			public const string Common_Error = "Common_Error";
			public const string Common_Error_Dangerous_Html_Message = "Common_Error_Dangerous_Html_Message";
			public const string Common_Error_Message = "Common_Error_Message";
			public const string Account_Incorrect_username_or_password = "Account_Incorrect_username_or_password";
			public const string Common_Gallery = "Common_Gallery";
			public const string Common_Password = "Common_Password";
			public const string Common_Please_Login_Again = "Common_Please_Login_Again";
			public const string Common_The_X_field_is_required = "Common_The_X_field_is_required";
			public const string Common_Unauthorized = "Common_Unauthorized";
			public const string Common_Username = "Common_Username";
			public const string RemindPassword_WrongCaptcha = "RemindPassword_WrongCaptcha";
			public const string Security_Access_is_denied = "Security_Access_is_denied";
			public const string Security_Session_has_expired = "Security_Session_has_expired";
			public const string Common_Login = "Common_Login";
			public const string Common_Logout = "Common_Logout";
			public const string Common_Error_File_Has_No_Content = "Common_Error_File_Has_No_Content";
			public const string Picture_BigFileSize = "Picture_BigFileSize";
			public const string Picture_Import = "Picture_Import";
			public const string Picture_Import_tooltip = "Picture_Import_tooltip";
			public const string Picture_SupportedTypes_Image = "Picture_SupportedTypes_Image";
			public const string Picture_TypeNotSupported_Template = "Picture_TypeNotSupported_Template";
			public const string Picture_Loading = "Picture_Loading";
			public const string Contact_Where_We_Are = "Contact_Where_We_Are";
			public const string Common_Validation_Max_Length = "Common_Validation_Max_Length";
			public const string Common_Validation_Required = "Common_Validation_Required";
			public const string Common_Send = "Common_Send";
			public const string Contact_Body = "Contact_Body";
			public const string Contact_FromEmail = "Contact_FromEmail";
			public const string Common_Validation_Email = "Common_Validation_Email";
			public const string Common_Feedback = "Common_Feedback";
			public const string Contact_Address = "Contact_Address";
			public const string Contact_Address_i = "Contact_Address_i";
			public const string Contact_Business_Hours = "Contact_Business_Hours";
			public const string Contact_Business_Hours_i = "Contact_Business_Hours_i";
			public const string Contact_Mail = "Contact_Mail";
			public const string Contact_Phone = "Contact_Phone";
			public const string Contact_Phone_i = "Contact_Phone_i";
			public const string Common_Details = "Common_Details";
			public const string Common_Name = "Common_Name";
			public const string Common_Buy = "Common_Buy";
			public const string Common_Delivery_Type = "Common_Delivery_Type";
			public const string Delivery_Type_Express = "Delivery_Type_Express";
			public const string Delivery_Type_SelfTake = "Delivery_Type_SelfTake";
		 		    	
		}

		public static string Language_English
		{
			get{ return GetString(Names.Language_English);}
		}
		public static string Language_Russian
		{
			get{ return GetString(Names.Language_Russian);}
		}
		public static string Language_Language
		{
			get{ return GetString(Names.Language_Language);}
		}
		public static string Nav_Prices
		{
			get{ return GetString(Names.Nav_Prices);}
		}
		public static string Nav_Main
		{
			get{ return GetString(Names.Nav_Main);}
		}
		public static string Nav_Main_Title
		{
			get{ return GetString(Names.Nav_Main_Title);}
		}
		public static string Nav_ShoppingCart
		{
			get{ return GetString(Names.Nav_ShoppingCart);}
		}
		public static string Side_About
		{
			get{ return GetString(Names.Side_About);}
		}
		public static string Side_Contact
		{
			get{ return GetString(Names.Side_Contact);}
		}
		public static string Side_Decor
		{
			get{ return GetString(Names.Side_Decor);}
		}
		public static string Side_Delivery
		{
			get{ return GetString(Names.Side_Delivery);}
		}
		public static string Home_Description
		{
			get{ return GetString(Names.Home_Description);}
		}
		public static string Common_Error
		{
			get{ return GetString(Names.Common_Error);}
		}
		public static string Common_Error_Dangerous_Html_Message
		{
			get{ return GetString(Names.Common_Error_Dangerous_Html_Message);}
		}
		public static string Common_Error_Message
		{
			get{ return GetString(Names.Common_Error_Message);}
		}
		public static string Account_Incorrect_username_or_password
		{
			get{ return GetString(Names.Account_Incorrect_username_or_password);}
		}
		public static string Common_Gallery
		{
			get{ return GetString(Names.Common_Gallery);}
		}
		public static string Common_Password
		{
			get{ return GetString(Names.Common_Password);}
		}
		public static string Common_Please_Login_Again
		{
			get{ return GetString(Names.Common_Please_Login_Again);}
		}
		public static string Common_The_X_field_is_required
		{
			get{ return GetString(Names.Common_The_X_field_is_required);}
		}
		public static string Common_Unauthorized
		{
			get{ return GetString(Names.Common_Unauthorized);}
		}
		public static string Common_Username
		{
			get{ return GetString(Names.Common_Username);}
		}
		public static string RemindPassword_WrongCaptcha
		{
			get{ return GetString(Names.RemindPassword_WrongCaptcha);}
		}
		public static string Security_Access_is_denied
		{
			get{ return GetString(Names.Security_Access_is_denied);}
		}
		public static string Security_Session_has_expired
		{
			get{ return GetString(Names.Security_Session_has_expired);}
		}
		public static string Common_Login
		{
			get{ return GetString(Names.Common_Login);}
		}
		public static string Common_Logout
		{
			get{ return GetString(Names.Common_Logout);}
		}
		public static string Common_Error_File_Has_No_Content
		{
			get{ return GetString(Names.Common_Error_File_Has_No_Content);}
		}
		public static string Picture_BigFileSize
		{
			get{ return GetString(Names.Picture_BigFileSize);}
		}
		public static string Picture_Import
		{
			get{ return GetString(Names.Picture_Import);}
		}
		public static string Picture_Import_tooltip
		{
			get{ return GetString(Names.Picture_Import_tooltip);}
		}
		public static string Picture_SupportedTypes_Image
		{
			get{ return GetString(Names.Picture_SupportedTypes_Image);}
		}
		public static string Picture_TypeNotSupported_Template
		{
			get{ return GetString(Names.Picture_TypeNotSupported_Template);}
		}
		public static string Picture_Loading
		{
			get{ return GetString(Names.Picture_Loading);}
		}
		public static string Contact_Where_We_Are
		{
			get{ return GetString(Names.Contact_Where_We_Are);}
		}
		public static string Common_Validation_Max_Length
		{
			get{ return GetString(Names.Common_Validation_Max_Length);}
		}
		public static string Common_Validation_Required
		{
			get{ return GetString(Names.Common_Validation_Required);}
		}
		public static string Common_Send
		{
			get{ return GetString(Names.Common_Send);}
		}
		public static string Contact_Body
		{
			get{ return GetString(Names.Contact_Body);}
		}
		public static string Contact_FromEmail
		{
			get{ return GetString(Names.Contact_FromEmail);}
		}
		public static string Common_Validation_Email
		{
			get{ return GetString(Names.Common_Validation_Email);}
		}
		public static string Common_Feedback
		{
			get{ return GetString(Names.Common_Feedback);}
		}
		public static string Contact_Address
		{
			get{ return GetString(Names.Contact_Address);}
		}
		public static string Contact_Address_i
		{
			get{ return GetString(Names.Contact_Address_i);}
		}
		public static string Contact_Business_Hours
		{
			get{ return GetString(Names.Contact_Business_Hours);}
		}
		public static string Contact_Business_Hours_i
		{
			get{ return GetString(Names.Contact_Business_Hours_i);}
		}
		public static string Contact_Mail
		{
			get{ return GetString(Names.Contact_Mail);}
		}
		public static string Contact_Phone
		{
			get{ return GetString(Names.Contact_Phone);}
		}
		public static string Contact_Phone_i
		{
			get{ return GetString(Names.Contact_Phone_i);}
		}
		public static string Common_Details
		{
			get{ return GetString(Names.Common_Details);}
		}
		public static string Common_Name
		{
			get{ return GetString(Names.Common_Name);}
		}
		public static string Common_Buy
		{
			get{ return GetString(Names.Common_Buy);}
		}
		public static string Common_Delivery_Type
		{
			get{ return GetString(Names.Common_Delivery_Type);}
		}
		public static string Delivery_Type_Express
		{
			get{ return GetString(Names.Delivery_Type_Express);}
		}
		public static string Delivery_Type_SelfTake
		{
			get{ return GetString(Names.Delivery_Type_SelfTake);}
		}
		 		    	
	}
}
