using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;


namespace Sitecore.SharedSource.ItemTranslator
{
    public class AzureCogTranslationService : ITranslationService
    {
        public string EndPoint { get; }

        public string SubscriptionKey { get; }

        public string From { get; }

        public string To { get; }

        public double Timeout { get; }

        public AzureCogTranslationService(string endPoint, string subscriptionKey, string @from, string to, double timeout)
        {
            EndPoint = endPoint;
            SubscriptionKey = subscriptionKey;
            From = @from;
            To = to;
            Timeout = timeout;
        }

        public string Translate(string text)
        {
            // https://api.cognitive.microsofttranslator.com/translate?api-version=3.0&from={0}&to={1}
            var serviceEndpoint = string.Format(EndPoint, From, To);
            var transBody = new object[] {new {Text = @text}};
            var requestBody = JsonConvert.SerializeObject(transBody);
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(serviceEndpoint);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);
                client.Timeout = TimeSpan.FromMilliseconds(Timeout);
                var response = client.SendAsync(request).Result;
                var jsonResponse = response.Content.ReadAsStringAsync().Result;
                if (string.IsNullOrEmpty(jsonResponse)) return text;
                var data = JsonConvert.DeserializeObject<List<TranslationResponse>>(jsonResponse);
                return data.FirstOrDefault()?.Translations.Select(d => d.Text).FirstOrDefault();

            }

        }
    }

    public class TranslationResponse
    {
        public List<TranslationBody> Translations { get; set; }

    }

    public class TranslationBody
    {
        public string Text { get; set; }
        public string To { get; set; }
    }
}
