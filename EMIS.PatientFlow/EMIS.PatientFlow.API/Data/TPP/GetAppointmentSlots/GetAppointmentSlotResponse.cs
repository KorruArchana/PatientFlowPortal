using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GetAppointmentSlots
{
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
	public partial class ClientIntegrationResponse
	{

		private string deviceIDField;

		private string requestUIDField;

		private string functionField;

		private string functionVersionField;

		private string responseUIDField;

		private ClientIntegrationResponseResponseClinic[] responseField;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string DeviceID
		{
			get
			{
				return this.deviceIDField;
			}
			set
			{
				this.deviceIDField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string RequestUID
		{
			get
			{
				return this.requestUIDField;
			}
			set
			{
				this.requestUIDField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string Function
		{
			get
			{
				return this.functionField;
			}
			set
			{
				this.functionField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string FunctionVersion
		{
			get
			{
				return this.functionVersionField;
			}
			set
			{
				this.functionVersionField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string ResponseUID
		{
			get
			{
				return this.responseUIDField;
			}
			set
			{
				this.responseUIDField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		[System.Xml.Serialization.XmlArrayItemAttribute("Clinic", typeof(ClientIntegrationResponseResponseClinic), Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
		public ClientIntegrationResponseResponseClinic[] Response
		{
			get
			{
				return this.responseField;
			}
			set
			{
				this.responseField = value;
			}
		}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public partial class ClientIntegrationResponseResponseClinic
	{

		private string userNameField;

		private string siteNameField;

		private string clinicTypeField;

		private ClientIntegrationResponseResponseClinicSlot[] slotField;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string UserName
		{
			get
			{
				return this.userNameField;
			}
			set
			{
				this.userNameField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string SiteName
		{
			get
			{
				return this.siteNameField;
			}
			set
			{
				this.siteNameField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string ClinicType
		{
			get
			{
				return this.clinicTypeField;
			}
			set
			{
				this.clinicTypeField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Slot", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public ClientIntegrationResponseResponseClinicSlot[] Slot
		{
			get
			{
				return this.slotField;
			}
			set
			{
				this.slotField = value;
			}
		}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public partial class ClientIntegrationResponseResponseClinicSlot
	{

		private string slotTypeField;

		private string dateTimeStartField;

		private string durationField;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string SlotType
		{
			get
			{
				return this.slotTypeField;
			}
			set
			{
				this.slotTypeField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string DateTimeStart
		{
			get
			{
				return this.dateTimeStartField;
			}
			set
			{
				this.dateTimeStartField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
		public string Duration
		{
			get
			{
				return this.durationField;
			}
			set
			{
				this.durationField = value;
			}
		}
	}

	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
	public partial class NewDataSet
	{

		private ClientIntegrationResponse[] itemsField;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("ClientIntegrationResponse")]
		public ClientIntegrationResponse[] Items
		{
			get
			{
				return this.itemsField;
			}
			set
			{
				this.itemsField = value;
			}
		}
	}
}
