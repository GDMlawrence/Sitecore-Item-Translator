using Sitecore.Diagnostics;
using Sitecore.SharedSource.ItemTranslator.BingTranslator;
using System;

namespace Sitecore.SharedSource.ItemTranslator
{
	internal class BingTranslateService : ITranslationService
	{
		private readonly LanguageServiceClient client = new LanguageServiceClient();

		private string BingApplicationId
		{
			get;
			set;
		}

		private string FromLanguage
		{
			get;
			set;
		}

		private string ToLanguage
		{
			get;
			set;
		}

		public BingTranslateService(string from, string to, string bingApplicationId)
		{
			this.FromLanguage = from;
			this.ToLanguage = to;
			this.BingApplicationId = bingApplicationId;
		}

		public string Translate(string text)
		{
			Log.Info("Translated: " + text, "BingService");
			return this.client.Translate(this.BingApplicationId, text, this.FromLanguage, this.ToLanguage);
		}
	}
}
