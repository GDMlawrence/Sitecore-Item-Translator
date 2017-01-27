using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Sitecore.SharedSource.ItemTranslator.BingTranslator
{
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0"), DebuggerStepThrough, DataContract(Name = "Translation", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2")]
	[Serializable]
	public class Translation : IExtensibleDataObject, INotifyPropertyChanged
	{
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		private string OriginalTextField;

		[OptionalField]
		private int RatingField;

		[OptionalField]
		private int SequenceField;

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
		public string OriginalText
		{
			get
			{
				return this.OriginalTextField;
			}
			set
			{
				if (!object.ReferenceEquals(this.OriginalTextField, value))
				{
					this.OriginalTextField = value;
					this.RaisePropertyChanged("OriginalText");
				}
			}
		}

		[DataMember(EmitDefaultValue = false)]
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

		[DataMember(EmitDefaultValue = false)]
		public int Sequence
		{
			get
			{
				return this.SequenceField;
			}
			set
			{
				if (!this.SequenceField.Equals(value))
				{
					this.SequenceField = value;
					this.RaisePropertyChanged("Sequence");
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
