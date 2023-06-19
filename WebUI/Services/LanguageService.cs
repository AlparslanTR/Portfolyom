using Microsoft.Extensions.Localization;
using System.Reflection;

namespace WebUI.Services
{
    public class HakkimdaResource
    {

    }

    public class LanguageService
    {
        private readonly IStringLocalizer _localizer;

        public LanguageService(IStringLocalizerFactory localizer)
        {
            var type = typeof(HakkimdaResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = localizer.Create(nameof(HakkimdaResource), assemblyName.Name);
        }

        public LocalizedString GetKey(string key)
        {
            return _localizer[key];
        }
    }
}
