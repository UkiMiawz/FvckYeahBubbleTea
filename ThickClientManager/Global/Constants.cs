using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThickClientManager.Global
{
    public class Constants
    {
        public const string JsonMediaType = "application/json";

        #region API addresses

        public const string BaseTeaAddress = "/api/baseTea/";
        public const string CustomerAddress = "/api/customer/";
        public const string FlavorAddress = "/api/flavor/";
        public const string TeaSizeAddress = "/api/size/";
        public const string ToppingAddress = "/api/topping/";
        
        #endregion

        #region Error Messages

        public const string GeneralError = "An error occured";
        public const string NameError = "Please input name";
        public const string PriceError = "Please input price";
        public const string NumberError = "Please input number for price";
        public const string UriError = "Invalid API link";

        #endregion

    }
}
