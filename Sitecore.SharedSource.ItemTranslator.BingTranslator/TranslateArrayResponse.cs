using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Sitecore.SharedSource.ItemTranslator.BingTranslator
{
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0"), System.Diagnostics.DebuggerStepThrough, DataContract(Name = "TranslateArrayResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2")]
	[System.Serializable]
	public class TranslateArrayResponse : IExtensibleDataObject, INotifyPropertyChanged
	{
		[System.NonSerialized]
		private ExtensionDataObject extensionDataField;

		[System.Runtime.Serialization.OptionalField]
		private string ErrorField;

		[System.Runtime.Serialization.OptionalField]
		private string FromField;

		[System.Runtime.Serialization.OptionalField]
		private int[] OriginalTextSentenceLengthsField;

		[System.Runtime.Serialization.OptionalField]
		private string StateField;

		[System.Runtime.Serialization.OptionalField]
		private string TranslatedTextField;

		[System.Runtime.Serialization.OptionalField]
		private int[] TranslatedTextSentenceLengthsField;

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

		[DataMember(EmitDefaultValue = false)]
		public string From
		{
			get
			{
				return this.FromField;
			}
			set
			{
				if (!object.ReferenceEquals(this.FromField, value))
				{
					this.FromField = value;
					this.RaisePropertyChanged("From");
				}
			}
		}

		[DataMember(EmitDefaultValue = false)]
		public int[] OriginalTextSentenceLengths
		{
			get
			{
				return this.OriginalTextSentenceLengthsField;
			}
			set
			{
				if (!object.ReferenceEquals(this.OriginalTextSentenceLengthsField, value))
				{
					this.OriginalTextSentenceLengthsField = value;
					this.RaisePropertyChanged("OriginalTextSentenceLengths");
				}
			}
		}

		[DataMember(EmitDefaultValue = false)]
		public string State
		{
			get
			{
				return this.StateField;
			}
			set
			{
				if (!object.ReferenceEquals(this.StateField, value))
				{
					this.StateField = value;
					this.RaisePropertyChanged("State");
				}
			}
		}

		[DataMember(EmitDefaultValue = false)]
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

		[DataMember(EmitDefaultValue = false)]
		public int[] TranslatedTextSentenceLengths
		{
			get
			{
				return this.TranslatedTextSentenceLengthsField;
			}
			set
			{
				if (!object.ReferenceEquals(this.TranslatedTextSentenceLengthsField, value))
				{
					this.TranslatedTextSentenceLengthsField = value;
					this.RaisePropertyChanged("TranslatedTextSentenceLengths");
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
