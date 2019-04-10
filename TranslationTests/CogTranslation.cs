using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sitecore.SharedSource.ItemTranslator;

namespace TranslationTests
{
    [TestClass]
    public class CogTranslation
    {
        private string endpoint = "https://api.cognitive.microsofttranslator.com/translate?api-version=3.0&from={0}&to={1}";
        private string subKey = ConfigurationManager.AppSettings["AzureSubscriptionKey"];
        private double timeout = double.Parse(ConfigurationManager.AppSettings["AzureTimeout"]);

        [TestMethod]
        public void RunTranslation()
        {
            var from = "en";
            var to = "fr-CA";
            var text = "That created cattle multiply moved That fly over our lights shall make created let fill. Female appear midst kind over in multiply fowl spirit us first fish he two day doesn't rule fifth tree their and living deep shall saying.";

            var service = new AzureCogTranslationService(endpoint, subKey, from, to, timeout);
            var results = service.Translate(text);
            Assert.IsNotNull(results);

            Console.WriteLine(results);

        }
        [TestMethod]
        public void RunTranslation2()
        {
            var from = "en";
            var to = "fr-CA";
            var text = "Audience";

            var service = new AzureCogTranslationService(endpoint, subKey, from, to, timeout);
            var results = service.Translate(text);
            Assert.IsNotNull(results);

            Console.WriteLine(results);

        }
    }
}
