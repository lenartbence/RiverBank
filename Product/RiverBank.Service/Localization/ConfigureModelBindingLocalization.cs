using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using RiverBank.Service.Resources;

namespace RiverBank.Service.Localization
{
    /// <summary>
    /// Configures custom localized error messages for model binding.
    /// </summary>
    public class ConfigureModelBindingLocalization : IConfigureOptions<MvcOptions>
    {
        private readonly IServiceScopeFactory _serviceFactory;

        public ConfigureModelBindingLocalization(IServiceScopeFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public void Configure(MvcOptions options)
        {
            using (var scope = _serviceFactory.CreateScope())
            {
                var stringLocalizerFactory = scope.ServiceProvider.GetService<IStringLocalizerFactory>();
                var localizer = stringLocalizerFactory.Create(nameof(ModelBindingMessages), typeof(ModelBindingMessages).Assembly.FullName);

                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => localizer[LocalizationResourceKeys.ValueMustNotBeNull]);
                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((_, __) => localizer[LocalizationResourceKeys.AttemptedValueIsInvalid]);
            }
        }
    }
}
