using Sitecore.Configuration;
using Sitecore.Diagnostics;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Web;

namespace Sitecore.SharedSource.ItemTranslator
{
	public class MSTranslationService : ITranslationService
	{
		private MSTranslationAuthentication authService;

		private string _translateUrl;

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

		public MSTranslationService(string from, string to, string clientID, string clientSecret)
		{
			this.FromLanguage = from;
			this.ToLanguage = to;
			this.authService = new MSTranslationAuthentication(clientID, clientSecret);
			this._translateUrl = Settings.GetSetting("MSTranslation_TranslateUrl");
		}

		public string Translate(string text)
		{
			Log.Info("Translate: " + text, "MSTranslationService");
			MSTranslationAccessToken token = this.authService.GetAccessToken();
			return this.PerformTranslation(text, token);
		}

		private string PerformTranslation(string text, MSTranslationAccessToken token)
		{
			string requestUrl = string.Format(this._translateUrl, HttpUtility.UrlEncode(text), this.FromLanguage, this.ToLanguage);
			string authHeader = "Bearer " + token.access_token;
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUrl);
			httpWebRequest.Headers.Add("Authorization", authHeader);
			WebResponse response = null;
			string result;
			try
			{
				response = httpWebRequest.GetResponse();
				using (System.IO.Stream stream = response.GetResponseStream())
				{
					DataContractSerializer serializer = new DataContractSerializer(System.Type.GetType("System.String"));
					result = (string)serializer.ReadObject(stream);
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				if (response != null)
				{
					response.Close();
				}
			}
			return result;
		}
	}
}
