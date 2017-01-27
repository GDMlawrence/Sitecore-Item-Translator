using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Sitecore.SharedSource.ItemTranslator.BingTranslator
{
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0"), DebuggerStepThrough, DataContract(Name = "TranslateOptions", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.MT.Web.Service.V2")]
	[Serializable]
	public class TranslateOptions : IExtensibleDataObject, INotifyPropertyChanged
	{
		[NonSerialized]
		private ExtensionDataObject extensionDataField;

		[OptionalField]
		private string CategoryField;

		[OptionalField]
		private string ContentTypeField;

		[OptionalField]
		private string ReservedFlagsField;

		[OptionalField]
		private string StateField;

		[OptionalField]
		private string UriField;

		[OptionalField]
		private string UserField;

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
		public string Category
		{
			get
			{
				return this.CategoryField;
			}
			set
			{
				if (!object.ReferenceEquals(this.CategoryField, value))
				{
					this.CategoryField = value;
					this.RaisePropertyChanged("Category");
				}
			}
		}

		[DataMember(EmitDefaultValue = false)]
		public string ContentType
		{
			get
			{
				return this.ContentTypeField;
			}
			set
			{
				if (!object.ReferenceEquals(this.ContentTypeField, value))
				{
					this.ContentTypeField = value;
					this.RaisePropertyChanged("ContentType");
				}
			}
		}

		[DataMember(EmitDefaultValue = false)]
		public string ReservedFlags
		{
			get
			{
				return this.ReservedFlagsField;
			}
			set
			{
				if (!object.ReferenceEquals(this.ReservedFlagsField, value))
				{
					this.ReservedFlagsField = value;
					this.RaisePropertyChanged("ReservedFlags");
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
		public string Uri
		{
			get
			{
				return this.UriField;
			}
			set
			{
				if (!object.ReferenceEquals(this.UriField, value))
				{
					this.UriField = value;
					this.RaisePropertyChanged("Uri");
				}
			}
		}

		[DataMember(EmitDefaultValue = false)]
		public string User
		{
			get
			{
				return this.UserField;
			}
			set
			{
				if (!object.ReferenceEquals(this.UserField, value))
				{
					this.UserField = value;
					this.RaisePropertyChanged("User");
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
