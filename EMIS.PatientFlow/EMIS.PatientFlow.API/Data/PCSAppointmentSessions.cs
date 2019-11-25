﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMIS.PatientFlow.API.Data
{
    public partial class PCSAppointmentSessions
    {
        // 
        // This source code was auto-generated by xsd, Version=4.0.30319.33440.
        // 

		
        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class AppointmentSessionList
        {

            private AppointmentSessionListAppointmentSession[] appointmentSessionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("AppointmentSession")]
            public AppointmentSessionListAppointmentSession[] AppointmentSession
            {
                get
                {
                    return this.appointmentSessionField;
                }
                set
                {
                    this.appointmentSessionField = value;
                }
            }
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class AppointmentSessionListAppointmentSession
        {

            private ulong dBIDField;

            private string nameField;

            private string dateField;

            private System.DateTime startTimeField;

            private System.DateTime endTimeField;

            private string slotLengthField;

            private AppointmentSessionSlotTypeList[] slotTypeListField;

            private AppointmentSessionListAppointmentSessionSite siteField;

            private AppointmentSessionListAppointmentSessionHolderList holderListField;

            /// <remarks/>
            public ulong DBID
            {
                get
                {
                    return this.dBIDField;
                }
                set
                {
                    this.dBIDField = value;
                }
            }

            /// <remarks/>
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }

            /// <remarks/>
            public string Date
            {
                get
                {
                    return this.dateField;
                }
                set
                {
                    this.dateField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "time")]
            public System.DateTime StartTime
            {
                get
                {
                    return this.startTimeField;
                }
                set
                {
                    this.startTimeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(DataType = "time")]
            public System.DateTime EndTime
            {
                get
                {
                    return this.endTimeField;
                }
                set
                {
                    this.endTimeField = value;
                }
            }

            /// <remarks/>
            public string SlotLength
            {
                get
                {
                    return this.slotLengthField;
                }
                set
                {
                    this.slotLengthField = value;
                }
            }

			/// <remarks/>
			[System.Xml.Serialization.XmlArrayItemAttribute("SlotType", IsNullable = false)]
			public AppointmentSessionSlotTypeList[] SlotTypeList
            {
                get
                {
                    return this.slotTypeListField;
                }
                set
                {
                    this.slotTypeListField = value;
                }
            }

            /// <remarks/>
            public AppointmentSessionListAppointmentSessionSite Site
            {
                get
                {
                    return this.siteField;
                }
                set
                {
                    this.siteField = value;
                }
            }

            /// <remarks/>
            public AppointmentSessionListAppointmentSessionHolderList HolderList
            {
                get
                {
                    return this.holderListField;
                }
                set
                {
                    this.holderListField = value;
                }
            }
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class AppointmentSessionListAppointmentSessionSlotTypeList
        {

            private AppointmentSessionSlotTypeList slotTypeField;

            /// <remarks/>
            public AppointmentSessionSlotTypeList SlotType
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
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class AppointmentSessionSlotTypeList
		{

            private string typeIDField;

            private string descriptionField;

            private byte totalField;

            private byte bookedField;

            private byte blockedField;

            private byte embargoedField;

            private byte availableField;

            /// <remarks/>
            public string TypeID
            {
                get
                {
                    return this.typeIDField;
                }
                set
                {
                    this.typeIDField = value;
                }
            }

            /// <remarks/>
            public string Description
            {
                get
                {
                    return this.descriptionField;
                }
                set
                {
                    this.descriptionField = value;
                }
            }

            /// <remarks/>
            public byte Total
            {
                get
                {
                    return this.totalField;
                }
                set
                {
                    this.totalField = value;
                }
            }

            /// <remarks/>
            public byte Booked
            {
                get
                {
                    return this.bookedField;
                }
                set
                {
                    this.bookedField = value;
                }
            }

            /// <remarks/>
            public byte Blocked
            {
                get
                {
                    return this.blockedField;
                }
                set
                {
                    this.blockedField = value;
                }
            }

            /// <remarks/>
            public byte Embargoed
            {
                get
                {
                    return this.embargoedField;
                }
                set
                {
                    this.embargoedField = value;
                }
            }

            /// <remarks/>
            public byte Available
            {
                get
                {
                    return this.availableField;
                }
                set
                {
                    this.availableField = value;
                }
            }
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class AppointmentSessionListAppointmentSessionSite
        {

            private long dBIDField;

            private string nameField;

            /// <remarks/>
            public long DBID
            {
                get
                {
                    return this.dBIDField;
                }
                set
                {
                    this.dBIDField = value;
                }
            }

            /// <remarks/>
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class AppointmentSessionListAppointmentSessionHolderList
        {

            private AppointmentSessionListAppointmentSessionHolderListHolder holderField;

            /// <remarks/>
            public AppointmentSessionListAppointmentSessionHolderListHolder Holder
            {
                get
                {
                    return this.holderField;
                }
                set
                {
                    this.holderField = value;
                }
            }
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class AppointmentSessionListAppointmentSessionHolderListHolder
        {

            private ulong dBIDField;

            private ulong refIDField;

            private string titleField;

            private string firstNamesField;

            private string lastNameField;

            /// <remarks/>
            public ulong DBID
            {
                get
                {
                    return this.dBIDField;
                }
                set
                {
                    this.dBIDField = value;
                }
            }

            /// <remarks/>
            public ulong RefID
            {
                get
                {
                    return this.refIDField;
                }
                set
                {
                    this.refIDField = value;
                }
            }

            /// <remarks/>
            public string Title
            {
                get
                {
                    return this.titleField;
                }
                set
                {
                    this.titleField = value;
                }
            }

            /// <remarks/>
            public string FirstNames
            {
                get
                {
                    return this.firstNamesField;
                }
                set
                {
                    this.firstNamesField = value;
                }
            }

            /// <remarks/>
            public string LastName
            {
                get
                {
                    return this.lastNameField;
                }
                set
                {
                    this.lastNameField = value;
                }
            }
        }
    }
}