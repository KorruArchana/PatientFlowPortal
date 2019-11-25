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
    public partial class PCSUserDetails
    {
        // 
        // This source code was auto-generated by xsd, Version=4.0.30319.33440.
        // 


        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.e-mis.com/emisopen/MedicalRecord", IsNullable = false)]
		public partial class UserDetails
        {

            private UserDetailsPerson personField;

            /// <remarks/>
            public UserDetailsPerson Person
            {
                get
                {
                    return this.personField;
                }
                set
                {
                    this.personField = value;
                }
            }
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
		public partial class UserDetailsPerson
        {

            private ushort dBIDField;

            private ushort refIDField;

            private string gUIDField;

            private string mnemonicField;

            private ushort pCSUserField;

            private string activeEMISUserField;

            private string titleField;

            private string firstNamesField;

            private string lastNameField;

            private UserDetailsPersonCategory categoryField;

            private string nationalCodeField;

            private uint prescriptionCodeField;

            private uint gmcCodeField;

            private string scriptsField;

            private string consulterField;

            private string contractStartField;

            private object contractEndField;

            private string doctorField;

            private string locumField;

            private string registrarField;

            private string maternityField;

            private string childSurveillanceField;

            private string contraceptiveField;

            private string twentyFourHourField;

            private string minorSurgeryField;

            private UserDetailsPersonResonsibleHA resonsibleHAField;

            private string contractualRelationshipField;

            private object telephone1Field;

            private object telephone2Field;

            private object faxField;

            private string emailField;

            private UserDetailsPersonRole roleField;

            /// <remarks/>
            public ushort DBID
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
            public ushort RefID
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
            public string GUID
            {
                get
                {
                    return this.gUIDField;
                }
                set
                {
                    this.gUIDField = value;
                }
            }

            /// <remarks/>
            public string Mnemonic
            {
                get
                {
                    return this.mnemonicField;
                }
                set
                {
                    this.mnemonicField = value;
                }
            }

            /// <remarks/>
            public ushort PCSUser
            {
                get
                {
                    return this.pCSUserField;
                }
                set
                {
                    this.pCSUserField = value;
                }
            }

            /// <remarks/>
            public string ActiveEMISUser
            {
                get
                {
                    return this.activeEMISUserField;
                }
                set
                {
                    this.activeEMISUserField = value;
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

            /// <remarks/>
            public UserDetailsPersonCategory Category
            {
                get
                {
                    return this.categoryField;
                }
                set
                {
                    this.categoryField = value;
                }
            }

            /// <remarks/>
            public string NationalCode
            {
                get
                {
                    return this.nationalCodeField;
                }
                set
                {
                    this.nationalCodeField = value;
                }
            }

			/// <remarks/>
			//public uint PrescriptionCode
			//{
			//	get
			//	{
			//		return this.prescriptionCodeField;
			//	}
			//	set
			//	{
			//		this.prescriptionCodeField = value;
			//	}
			//}

			/// <remarks/>
			public uint GmcCode
			{
				get
				{
					return this.gmcCodeField;
				}
				set
				{
					this.gmcCodeField = value;
				}
			}

			/// <remarks/>
			public string Scripts
            {
                get
                {
                    return this.scriptsField;
                }
                set
                {
                    this.scriptsField = value;
                }
            }

            /// <remarks/>
            public string Consulter
            {
                get
                {
                    return this.consulterField;
                }
                set
                {
                    this.consulterField = value;
                }
            }

            /// <remarks/>
            public string ContractStart
            {
                get
                {
                    return this.contractStartField;
                }
                set
                {
                    this.contractStartField = value;
                }
            }

            /// <remarks/>
            public object ContractEnd
            {
                get
                {
                    return this.contractEndField;
                }
                set
                {
                    this.contractEndField = value;
                }
            }

            /// <remarks/>
            public string Doctor
            {
                get
                {
                    return this.doctorField;
                }
                set
                {
                    this.doctorField = value;
                }
            }

            /// <remarks/>
            public string Locum
            {
                get
                {
                    return this.locumField;
                }
                set
                {
                    this.locumField = value;
                }
            }

            /// <remarks/>
            public string Registrar
            {
                get
                {
                    return this.registrarField;
                }
                set
                {
                    this.registrarField = value;
                }
            }

            /// <remarks/>
            public string Maternity
            {
                get
                {
                    return this.maternityField;
                }
                set
                {
                    this.maternityField = value;
                }
            }

            /// <remarks/>
            public string ChildSurveillance
            {
                get
                {
                    return this.childSurveillanceField;
                }
                set
                {
                    this.childSurveillanceField = value;
                }
            }

            /// <remarks/>
            public string Contraceptive
            {
                get
                {
                    return this.contraceptiveField;
                }
                set
                {
                    this.contraceptiveField = value;
                }
            }

            /// <remarks/>
            public string TwentyFourHour
            {
                get
                {
                    return this.twentyFourHourField;
                }
                set
                {
                    this.twentyFourHourField = value;
                }
            }

            /// <remarks/>
            public string MinorSurgery
            {
                get
                {
                    return this.minorSurgeryField;
                }
                set
                {
                    this.minorSurgeryField = value;
                }
            }

            /// <remarks/>
            public UserDetailsPersonResonsibleHA ResonsibleHA
            {
                get
                {
                    return this.resonsibleHAField;
                }
                set
                {
                    this.resonsibleHAField = value;
                }
            }

            /// <remarks/>
            public string ContractualRelationship
            {
                get
                {
                    return this.contractualRelationshipField;
                }
                set
                {
                    this.contractualRelationshipField = value;
                }
            }

            /// <remarks/>
            public object Telephone1
            {
                get
                {
                    return this.telephone1Field;
                }
                set
                {
                    this.telephone1Field = value;
                }
            }

            /// <remarks/>
            public object Telephone2
            {
                get
                {
                    return this.telephone2Field;
                }
                set
                {
                    this.telephone2Field = value;
                }
            }

            /// <remarks/>
            public object Fax
            {
                get
                {
                    return this.faxField;
                }
                set
                {
                    this.faxField = value;
                }
            }

            /// <remarks/>
            public string Email
            {
                get
                {
                    return this.emailField;
                }
                set
                {
                    this.emailField = value;
                }
            }

            /// <remarks/>
            public UserDetailsPersonRole Role
            {
                get
                {
                    return this.roleField;
                }
                set
                {
                    this.roleField = value;
                }
            }
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
        public partial class UserDetailsPersonCategory
        {

            private byte dBIDField;

            private byte refIDField;

            private string mnemonicField;

            private string descriptionField;

            /// <remarks/>
            public byte DBID
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
            public byte RefID
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
            public string Mnemonic
            {
                get
                {
                    return this.mnemonicField;
                }
                set
                {
                    this.mnemonicField = value;
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
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
        public partial class UserDetailsPersonResonsibleHA
        {

            private byte dBIDField;

            private byte refIDField;

            /// <remarks/>
            public byte DBID
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
            public byte RefID
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
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
        public partial class UserDetailsPersonRole
        {

            private byte dBIDField;

            private byte refIDField;

            private UserDetailsPersonRoleApplication applicationField;

            private UserDetailsPersonRoleTeam teamField;

            private UserDetailsPersonRoleLocation locationField;

            /// <remarks/>
            public byte DBID
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
            public byte RefID
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
            public UserDetailsPersonRoleApplication Application
            {
                get
                {
                    return this.applicationField;
                }
                set
                {
                    this.applicationField = value;
                }
            }

            /// <remarks/>
            public UserDetailsPersonRoleTeam Team
            {
                get
                {
                    return this.teamField;
                }
                set
                {
                    this.teamField = value;
                }
            }

            /// <remarks/>
            public UserDetailsPersonRoleLocation Location
            {
                get
                {
                    return this.locationField;
                }
                set
                {
                    this.locationField = value;
                }
            }
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
        public partial class UserDetailsPersonRoleApplication
        {

            private byte dBIDField;

            private byte refIDField;

            private string applicationMnemonicField;

            private string applicationNameField;

            /// <remarks/>
            public byte DBID
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
            public byte RefID
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
            public string ApplicationMnemonic
            {
                get
                {
                    return this.applicationMnemonicField;
                }
                set
                {
                    this.applicationMnemonicField = value;
                }
            }

            /// <remarks/>
            public string ApplicationName
            {
                get
                {
                    return this.applicationNameField;
                }
                set
                {
                    this.applicationNameField = value;
                }
            }
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
        public partial class UserDetailsPersonRoleTeam
        {

            private byte dBIDField;

            private byte refIDField;

            /// <remarks/>
            public byte DBID
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
            public byte RefID
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
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.e-mis.com/emisopen/MedicalRecord")]
        public partial class UserDetailsPersonRoleLocation
        {

            private uint dBIDField;

            private uint refIDField;

            /// <remarks/>
            public uint DBID
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
            public uint RefID
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
        }
    }
}
