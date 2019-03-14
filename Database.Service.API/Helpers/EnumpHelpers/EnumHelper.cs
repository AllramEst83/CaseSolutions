using Aerende.Service.API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Service.API.Helpers.EnumpHelper
{
    public static class EnumHelper
    {
        public static T GetEnumValue<T>(string value) where  T : struct, IConvertible
        {
            if (string.IsNullOrEmpty(value) ||!typeof(T).IsEnum)
            {
                //Handel error later
                throw new Exception("Type given must be an Enum");
            }

            try
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            catch (Exception)
            {

                return default(T);
            }
            
        } 
    }
}
