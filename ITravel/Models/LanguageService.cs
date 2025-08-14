using Microsoft.Extensions.Localization;
using System.Reflection;

namespace ITravel.Models
{
    public class LanguageService
    {
        private readonly IStringLocalizer _loacalizer;

        public LanguageService(IStringLocalizerFactory factory)
        {
            var type = typeof(ShareResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _loacalizer = factory.Create("Resource", assemblyName.Name);
        }

        public LocalizedString GetKey(string key)
        {
            return _loacalizer[key];
        }
    }
}
