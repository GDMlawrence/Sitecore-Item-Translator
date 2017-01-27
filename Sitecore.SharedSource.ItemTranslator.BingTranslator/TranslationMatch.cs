using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Sitecore.SharedSource.ItemTranslator.BingTranslator
{
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0"), System.Diagnostics.DebuggerStepThrough, DataContract(Name = "TranslationMatch", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2")]
	[System.Serializable]
	public class TranslationMatch : IExtensibleDataObject, INotifyPropertyChanged
	{
		[System.NonSerialized]
		private ExtensionDataObject extensionDataField;

		private int CountField;

		[System.Runtime.Serialization.OptionalField]
		private string ErrorField;

		private int MatchDegreeField;

		[System.Runtime.Serialization.OptionalField]
		private string MatchedOriginalTextField;

		private int RatingField;

		private string TranslatedTextField;

		public event PropertyChangedEventHandler PropertyChanged;

		[Browsable(false)]
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		[DataMember(IsRequired = true)]
		public int Count
		{
			get
			{
				return this.CountField;
			}
			set
			{
				if (!this.CountField.Equals(value))
				{
					this.CountField = value;
					this.RaisePropertyChanged("Count");
				}
			}
		}

		[DataMember(EmitDefaultValue = false)]
		public string Error
		{
			get
			{
				return this.ErrorField;
			}
			set
			{
				if (!object.ReferenceEquals(this.ErrorField, value))
				{
					this.ErrorField = value;
					this.RaisePropertyChanged("Error");
				}
			}
		}

		[DataMember(IsRequired = true)]
		public int MatchDegree
		{
			get
			{
				return this.MatchDegreeField;
			}
			set
			{
				if (!this.MatchDegreeField.Equals(value))
				{
					this.MatchDegreeField = value;
					this.RaisePropertyChanged("MatchDegree");
				}
			}
		}

		[DataMember(EmitDefaultValue = false)]
		public string MatchedOriginalText
		{
			get
			{
				return this.MatchedOriginalTextField;
			}
			set
			{
				if (!object.ReferenceEquals(this.MatchedOriginalTextField, value))
				{
					this.MatchedOriginalTextField = value;
					this.RaisePropertyChanged("MatchedOriginalText");
				}
			}
		}

		[DataMember(IsRequired = true)]
		public int Rating
		{
			get
			{
				return this.RatingField;
			}
			set
			{
				if (!this.RatingField.Equals(value))
				{
					this.RatingField = value;
					this.RaisePropertyChanged("Rating");
				}
			}
		}

		[DataMember(IsRequired = true)]
		public string TranslatedText
		{
			get
			{
				return this.TranslatedTextField;
			}
			set
			{
				if (!object.ReferenceEquals(this.TranslatedTextField, value))
				{
					this.TranslatedTextField = value;
					this.RaisePropertyChanged("TranslatedText");
				}
			}
		}

		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
