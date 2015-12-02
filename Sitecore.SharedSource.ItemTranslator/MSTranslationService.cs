using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Web;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Threading;
using System.Runtime.Caching;

namespace Sitecore.SharedSource.ItemTranslator
{
    public class MSTranslationService : ITranslationService
    {
        string FromLanguage { get; set; }
        string ToLanguage { get; set; }

        MSTranslationAuthentication authService;
        string _translateUrl;

        public MSTranslationService(string from, string to, string clientID, string clientSecret)
        {
            this.FromLanguage = from;
            this.ToLanguage = to;

            authService = new MSTranslationAuthentication(clientID, clientSecret);
            _translateUrl = Sitecore.Configuration.Settings.GetSetting("MSTranslation_TranslateUrl");
        }

        public string Translate(string text)
        {
            Sitecore.Diagnostics.Log.Info("Translate: " + text, "MSTranslationService");
            // get token
            var token = authService.GetAccessToken();
            
            // perform translation
            return PerformTranslation(text, token);
        }

        private string PerformTranslation(string text, MSTranslationAccessToken token)
        {
            //Sitecore.Diagnostics.Log.Info("PerformTranslation: Access Token: " + token.access_token, "MSTranslationService");

            var requestUrl = string.Format(_translateUrl, HttpUtility.UrlEncode(text), this.FromLanguage, this.ToLanguage);
            var authHeader = "Bearer " + token.access_token;
            //Sitecore.Diagnostics.Log.Info("PerformTranslation: Request: " + requestUrl, "MSTranslationService");

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUrl);
            httpWebRequest.Headers.Add("Authorization", authHeader);
            WebResponse response = null;
            try
            {
                response = httpWebRequest.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    DataContractSerializer serializer = new DataContractSerializer(Type.GetType("System.String"));
                    return (string)serializer.ReadObject(stream);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }
    }

    #region Token Class
    [DataContract]
    public class MSTranslationAccessToken
    {
        [DataMember]
        public string access_token { get; set; }
        [DataMember]
        public string token_type { get; set; }
        [DataMember]
        public string expires_in { get; set; }
        [DataMember]
        public string scope { get; set; }
    }

    #endregion


    #region Token Generation
    class MSTranslationAuthentication
    {
        //Access token expires every 10 minutes. Renew it every 9 minutes only.
        private const int RefreshTokenDuration = 9;
        private const string TokenCacheKey = "TOKEN_CACHE_KEY";
        public static readonly string DatamarketAccessUri = Sitecore.Configuration.Settings.GetSetting("MSTranslation_TokenUrl");
        private string clientId;
        private string clientSecret;
        private string tokenPostBody, tokenUrl;


        public MSTranslationAuthentication(string clientId, string clientSecret)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;


            //If clientid or client secret has special characters, encode before sending request
            this.tokenPostBody = string.Format(Sitecore.Configuration.Settings.GetSetting("MSTranslation_TokenPostBody"), 
                HttpUtility.UrlEncode(clientId), HttpUtility.UrlEncode(clientSecret));

        }
        public MSTranslationAccessToken GetAccessToken()
        {
            ObjectCache cache = MemoryCache.Default;
            if (cache.Contains(TokenCacheKey))
            {
                return (MSTranslationAccessToken)cache[TokenCacheKey];
            }
            else
            {
                var token = HttpPost(DatamarketAccessUri, this.tokenPostBody);
                // add to cache
                cache.Add(TokenCacheKey, token, DateTimeOffset.UtcNow.AddMilliseconds(RefreshTokenDuration), null);
                return token;
            }
        }
        private MSTranslationAccessToken HttpPost(string DatamarketAccessUri, string requestDetails)
        {
            //Prepare OAuth request 
            WebRequest webRequest = WebRequest.Create(DatamarketAccessUri);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";
            byte[] bytes = Encoding.ASCII.GetBytes(requestDetails);
            webRequest.ContentLength = bytes.Length;
            using (Stream outputStream = webRequest.GetRequestStream())
            {
                outputStream.Write(bytes, 0, bytes.Length);
            }
            using (WebResponse webResponse = webRequest.GetResponse())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(MSTranslationAccessToken));
                //Get deserialized object from JSON stream
                MSTranslationAccessToken token = (MSTranslationAccessToken)serializer.ReadObject(webResponse.GetResponseStream());
                return token;
            }
        }



    }

    #endregion
}
