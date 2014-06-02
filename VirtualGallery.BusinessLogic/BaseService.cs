using System;

namespace VirtualGallery.BusinessLogic
{
	public class BaseService
	{
        #region Validation

        protected static void EnsureValidId(int id, string argumentName)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }

		protected static void EnsureValidId(long id, string argumentName)
		{
			if (id <= 0)
			{
				throw new ArgumentOutOfRangeException(argumentName);
			}
		}


        protected static void EnsureNotEmpty(string str, string argumentName)
        {
            if (String.IsNullOrEmpty(str))
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }

        protected static void EnsureNotNull(object obj, string argumentName)
        {
            if (obj == null)
            {
                throw new ArgumentOutOfRangeException(argumentName);
            }
        }

		#endregion //Validation
	}
}
