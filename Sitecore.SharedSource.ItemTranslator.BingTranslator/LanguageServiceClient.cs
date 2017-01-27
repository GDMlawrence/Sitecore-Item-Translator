using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Sitecore.SharedSource.ItemTranslator.BingTranslator
{
	[GeneratedCode("System.ServiceModel", "4.0.0.0"), System.Diagnostics.DebuggerStepThrough]
	public class LanguageServiceClient : ClientBase<LanguageService>, LanguageService
	{
		public LanguageServiceClient()
		{
		}

		public LanguageServiceClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		public LanguageServiceClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		public LanguageServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		public LanguageServiceClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		public void AddTranslation(string appId, string originalText, string translatedText, string from, string to, int rating, string contentType, string category, string user, string uri)
		{
			base.Channel.AddTranslation(appId, originalText, translatedText, from, to, rating, contentType, category, user, uri);
		}

		public int[] BreakSentences(string appId, string text, string language)
		{
			return base.Channel.BreakSentences(appId, text, language);
		}

		public string Detect(string appId, string text)
		{
			return base.Channel.Detect(appId, text);
		}

		public string[] DetectArray(string appId, string[] texts)
		{
			return base.Channel.DetectArray(appId, texts);
		}

		public string GetAppIdToken(string appId, int minRatingRead, int maxRatingWrite, int expireSeconds)
		{
			return base.Channel.GetAppIdToken(appId, minRatingRead, maxRatingWrite, expireSeconds);
		}

		public string[] GetLanguageNames(string appId, string locale, string[] languageCodes)
		{
			return base.Channel.GetLanguageNames(appId, locale, languageCodes);
		}

		public string[] GetLanguagesForSpeak(string appId)
		{
			return base.Channel.GetLanguagesForSpeak(appId);
		}

		public string[] GetLanguagesForTranslate(string appId)
		{
			return base.Channel.GetLanguagesForTranslate(appId);
		}

		public GetTranslationsResponse GetTranslations(string appId, string text, string from, string to, int maxTranslations, TranslateOptions options)
		{
			return base.Channel.GetTranslations(appId, text, from, to, maxTranslations, options);
		}

		public string Translate(string appId, string text, string from, string to)
		{
			return base.Channel.Translate(appId, text, from, to);
		}

		public void AddTranslationArray(string appId, Translation[] translations, string from, string to, TranslateOptions options)
		{
			base.Channel.AddTranslationArray(appId, translations, from, to, options);
		}

		public GetTranslationsResponse[] GetTranslationsArray(string appId, string[] texts, string from, string to, int maxTranslations, TranslateOptions options)
		{
			return base.Channel.GetTranslationsArray(appId, texts, from, to, maxTranslations, options);
		}

		public string Speak(string appId, string text, string language, string format)
		{
			return base.Channel.Speak(appId, text, language, format);
		}

		public TranslateArrayResponse[] TranslateArray(string appId, string[] texts, string from, string to, TranslateOptions options)
		{
			return base.Channel.TranslateArray(appId, texts, from, to, options);
		}
	}
}
