using System;
using System.CodeDom.Compiler;
using System.ServiceModel;

namespace Sitecore.SharedSource.ItemTranslator.BingTranslator
{
	[GeneratedCode("System.ServiceModel", "4.0.0.0"), ServiceContract(Namespace = "http://api.microsofttranslator.com/V2", ConfigurationName = "BingTranslator.LanguageService")]
	public interface LanguageService
	{
		[OperationContract(Action = "http://api.microsofttranslator.com/V2/LanguageService/AddTranslation", ReplyAction = "http://api.microsofttranslator.com/V2/LanguageService/AddTranslationResponse")]
		void AddTranslation(string appId, string originalText, string translatedText, string from, string to, int rating, string contentType, string category, string user, string uri);

		[OperationContract(Action = "http://api.microsofttranslator.com/V2/LanguageService/BreakSentences", ReplyAction = "http://api.microsofttranslator.com/V2/LanguageService/BreakSentencesResponse")]
		int[] BreakSentences(string appId, string text, string language);

		[OperationContract(Action = "http://api.microsofttranslator.com/V2/LanguageService/Detect", ReplyAction = "http://api.microsofttranslator.com/V2/LanguageService/DetectResponse")]
		string Detect(string appId, string text);

		[OperationContract(Action = "http://api.microsofttranslator.com/V2/LanguageService/DetectArray", ReplyAction = "http://api.microsofttranslator.com/V2/LanguageService/DetectArrayResponse")]
		string[] DetectArray(string appId, string[] texts);

		[OperationContract(Action = "http://api.microsofttranslator.com/V2/LanguageService/GetAppIdToken", ReplyAction = "http://api.microsofttranslator.com/V2/LanguageService/GetAppIdTokenResponse")]
		string GetAppIdToken(string appId, int minRatingRead, int maxRatingWrite, int expireSeconds);

		[OperationContract(Action = "http://api.microsofttranslator.com/V2/LanguageService/GetLanguageNames", ReplyAction = "http://api.microsofttranslator.com/V2/LanguageService/GetLanguageNamesResponse")]
		string[] GetLanguageNames(string appId, string locale, string[] languageCodes);

		[OperationContract(Action = "http://api.microsofttranslator.com/V2/LanguageService/GetLanguagesForSpeak", ReplyAction = "http://api.microsofttranslator.com/V2/LanguageService/GetLanguagesForSpeakResponse")]
		string[] GetLanguagesForSpeak(string appId);

		[OperationContract(Action = "http://api.microsofttranslator.com/V2/LanguageService/GetLanguagesForTranslate", ReplyAction = "http://api.microsofttranslator.com/V2/LanguageService/GetLanguagesForTranslateResponse")]
		string[] GetLanguagesForTranslate(string appId);

		[OperationContract(Action = "http://api.microsofttranslator.com/V2/LanguageService/GetTranslations", ReplyAction = "http://api.microsofttranslator.com/V2/LanguageService/GetTranslationsResponse")]
		GetTranslationsResponse GetTranslations(string appId, string text, string from, string to, int maxTranslations, TranslateOptions options);

		[OperationContract(Action = "http://api.microsofttranslator.com/V2/LanguageService/Translate", ReplyAction = "http://api.microsofttranslator.com/V2/LanguageService/TranslateResponse")]
		string Translate(string appId, string text, string from, string to);

		[OperationContract(Action = "http://api.microsofttranslator.com/V2/LanguageService/AddTranslationArray", ReplyAction = "http://api.microsofttranslator.com/V2/LanguageService/AddTranslationArrayResponse")]
		void AddTranslationArray(string appId, Translation[] translations, string from, string to, TranslateOptions options);

		[OperationContract(Action = "http://api.microsofttranslator.com/V2/LanguageService/GetTranslationsArray", ReplyAction = "http://api.microsofttranslator.com/V2/LanguageService/GetTranslationsArrayResponse")]
		GetTranslationsResponse[] GetTranslationsArray(string appId, string[] texts, string from, string to, int maxTranslations, TranslateOptions options);

		[OperationContract(Action = "http://api.microsofttranslator.com/V2/LanguageService/Speak", ReplyAction = "http://api.microsofttranslator.com/V2/LanguageService/SpeakResponse")]
		string Speak(string appId, string text, string language, string format);

		[OperationContract(Action = "http://api.microsofttranslator.com/V2/LanguageService/TranslateArray", ReplyAction = "http://api.microsofttranslator.com/V2/LanguageService/TranslateArrayResponse")]
		TranslateArrayResponse[] TranslateArray(string appId, string[] texts, string from, string to, TranslateOptions options);
	}
}
