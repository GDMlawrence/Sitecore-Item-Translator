using Sitecore.Configuration;
using System;
using System.IO;
using System.Net;
using System.Runtime.Caching;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace Sitecore.SharedSource.ItemTranslator
{
	internal class MSTranslationAuthentication
	{
		private const int RefreshTokenDuration = 9;

		private const string TokenCacheKey = "TOKEN_CACHE_KEY";

		public static readonly string DatamarketAccessUri = Settings.GetSetting("MSTranslation_TokenUrl");

		private string clientId;

		private string clientSecret;

		private string tokenPostBody;

		private string tokenUrl;

		public MSTranslationAuthentication(string clientId, string clientSecret)
		{
			this.clientId = clientId;
			this.clientSecret = clientSecret;
			this.tokenPostBody = string.Format(Settings.GetSetting("MSTranslation_TokenPostBody"), HttpUtility.UrlEncode(clientId), HttpUtility.UrlEncode(clientSecret));
		}

		public MSTranslationAccessToken GetAccessToken()
		{
			ObjectCache cache = MemoryCache.Default;
			if (cache.Contains("TOKEN_CACHE_KEY", null))
			{
				return (MSTranslationAccessToken)cache["TOKEN_CACHE_KEY"];
			}
			MSTranslationAccessToken token = this.HttpPost(MSTranslationAuthentication.DatamarketAccessUri, this.tokenPostBody);
			cache.Add("TOKEN_CACHE_KEY", token, System.DateTimeOffset.UtcNow.AddMilliseconds(9.0), null);
			return token;
		}

		private MSTranslationAccessToken HttpPost(string DatamarketAccessUri, string requestDetails)
		{
			WebRequest webRequest = WebRequest.Create(DatamarketAccessUri);
			webRequest.ContentType = "application/x-www-form-urlencoded";
			webRequest.Method = "POST";
			byte[] bytes = System.Text.Encoding.ASCII.GetBytes(requestDetails);
			webRequest.ContentLength = (long)bytes.Length;
			using (System.IO.Stream outputStream = webRequest.GetRequestStream())
			{
				outputStream.Write(bytes, 0, bytes.Length);
			}
			MSTranslationAccessToken result;
			using (WebResponse webResponse = webRequest.GetResponse())
			{
				DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(MSTranslationAccessToken));
				MSTranslationAccessToken token = (MSTranslationAccessToken)serializer.ReadObject(webResponse.GetResponseStream());
				result = token;
			}
			return result;
		}
	}
}
