using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Sitecore.SharedSource.ItemTranslator.BingTranslator
{
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0"), DebuggerStepThrough, DataContract(Name = "GetTranslationsResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2")]
	[Serializable]
	public class GetTranslationsResponse : IExtensibleDataObject, INotifyPropertyChanged
	{
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		[OptionalField]
		private string FromField;

		[OptionalField]
		private string StateField;

		[OptionalField]
		private TranslationMatch[] TranslationsField;

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
		public TranslationMatch[] Translations
		{
			get
			{
				return this.TranslationsField;
			}
			set
			{
				if (!object.ReferenceEquals(this.TranslationsField, value))
				{
					this.TranslationsField = value;
					this.RaisePropertyChanged("Translations");
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
