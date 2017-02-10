using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Sitecore.SharedSource.ItemTranslator.AzureCogService;

namespace Sitecore.SharedSource.ItemTranslator
{
    public class AzureTranslationService : ITranslationService
    {
        private readonly string _azureSubKey;
        private readonly LanguageServiceClient _translatorService;
        public string FromLanguage { get; set; }
        public string ToLanguage { get; set; }
        private readonly double _timeout;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureTranslationService"/> class.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="subscriptionKey">The subscription key.</param>
        /// <param name="timeout">The timeout in milliseconds.</param>
        public AzureTranslationService(string from, string to, string subscriptionKey, double timeout)
        {
            FromLanguage = from;
            ToLanguage = to;
            _azureSubKey = subscriptionKey;
            _timeout = timeout;

            _translatorService = new LanguageServiceClient();

        }

        public string Translate(string text)
        {
            Diagnostics.Log.Info("Translate: " + text, "AzureTranslationService");
            var authTokenSource = new AzureAuthToken(_azureSubKey, _timeout);
            try
            {
                var token = authTokenSource.GetAccessToken();
                return _translatorService.Translate(token, text, FromLanguage, ToLanguage, "text/html", "general",
                    string.Empty);

            }
            catch (HttpRequestException exception)
            {
                switch (authTokenSource.RequestStatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        Diagnostics.Log.Error(
                            "Request to token service is not authorized (401). Check that the Azure subscription key is valid.",
                            exception, GetType());
                        break;
                    case HttpStatusCode.Forbidden:
                        Diagnostics.Log.Error(
                            "Request to token service is not authorized (403). For accounts in the free-tier, check that the account quota is not exceeded.",
                            exception, GetType());
                        break;
                }
            }
            catch (Exception ex)
            {
                Diagnostics.Log.Error(
                    "General error",
                    ex, GetType());
            }
            return text;
        }
    }
}
