using System;
using System.Runtime.Serialization;

namespace Sitecore.SharedSource.ItemTranslator
{
	[DataContract]
	public class MSTranslationAccessToken
	{
		[DataMember]
		public string access_token
		{
			get;
			set;
		}

		[DataMember]
		public string token_type
		{
			get;
			set;
		}

		[DataMember]
		public string expires_in
		{
			get;
			set;
		}

		[DataMember]
		public string scope
		{
			get;
			set;
		}
	}
}
