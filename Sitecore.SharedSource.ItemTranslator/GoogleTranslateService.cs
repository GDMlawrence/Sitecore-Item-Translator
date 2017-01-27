using Google.API.Translate;
using System;

namespace Sitecore.SharedSource.ItemTranslator
{
	internal class GoogleTranslateService : ITranslationService
	{
		private readonly TranslateClient client = new TranslateClient("http://google.com");

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

		public GoogleTranslateService(string from, string to)
		{
			this.FromLanguage = from;
			this.ToLanguage = to;
		}

		public string Translate(string text)
		{
			return this.client.Translate(text, this.FromLanguage, this.ToLanguage);
		}
	}
}
