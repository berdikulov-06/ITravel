using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITravel.Service.Extensions
{
    public static class HeaderExtensions
    {
        public static (string username, string password) GetBasicCredentials(this string authHeader)
        {
            string encoded = authHeader.Replace("Basic ", "").Trim();
            Encoding encoding = Encoding.GetEncoding("iso-8859-1"); string[] credentials = encoding.GetString(Convert.FromBase64String(encoded)).Split(':');
            return (credentials[0], credentials[1]);
        }
    }
}
