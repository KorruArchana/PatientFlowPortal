namespace EMIS.PatientFlow.API.Data
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class BookedAppointment
    {

        private BookedAppointmentAppointmentInformation appointmentInformationField;

        private BookedAppointmentPatientInforamtion patientInforamtionField;

        /// <remarks/>
        public BookedAppointmentAppointmentInformation AppointmentInformation
        {
            get
            {
                return this.appointmentInformationField;
            }
            set
            {
                this.appointmentInformationField = value;
            }
        }

        /// <remarks/>
        public BookedAppointmentPatientInforamtion PatientInforamtion
        {
            get
            {
                return this.patientInforamtionField;
            }
            set
            {
                this.patientInforamtionField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class BookedAppointmentAppointmentInformation
    {

        private long sessionIdField;

        private long slotIdField;

        private string slotDateTimeField;

        private string endDateTimeField;

        private object reasonField;

        private object bookingNotesField;

        private int slotTypeIdField;

        /// <remarks/>
        public long SessionId
        {
            get
            {
                return this.sessionIdField;
            }
            set
            {
                this.sessionIdField = value;
            }
        }

        /// <remarks/>
        public long SlotId
        {
            get
            {
                return this.slotIdField;
            }
            set
            {
                this.slotIdField = value;
            }
        }

        /// <remarks/>
        public string SlotDateTime
        {
            get
            {
                return this.slotDateTimeField;
            }
            set
            {
                this.slotDateTimeField = value;
            }
        }

        /// <remarks/>
        public string EndDateTime
        {
            get
            {
                return this.endDateTimeField;
            }
            set
            {
                this.endDateTimeField = value;
            }
        }

        /// <remarks/>
        public object Reason
        {
            get
            {
                return this.reasonField;
            }
            set
            {
                this.reasonField = value;
            }
        }

        /// <remarks/>
        public object BookingNotes
        {
            get
            {
                return this.bookingNotesField;
            }
            set
            {
                this.bookingNotesField = value;
            }
        }

        /// <remarks/>
        public int SlotTypeId
        {
            get
            {
                return this.slotTypeIdField;
            }
            set
            {
                this.slotTypeIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class BookedAppointmentPatientInforamtion
    {

        private int patientIdField;

        private string lEDField;

        private int patientNumberField;

        private string dOBField;

        private string nameField;

        private string hasPatientWarningsField;

        private ulong actionPerformedOnField;

        /// <remarks/>
        public int PatientId
        {
            get
            {
                return this.patientIdField;
            }
            set
            {
                this.patientIdField = value;
            }
        }

        /// <remarks/>
        public string LED
        {
            get
            {
                return this.lEDField;
            }
            set
            {
                this.lEDField = value;
            }
        }

        /// <remarks/>
        public int PatientNumber
        {
            get
            {
                return this.patientNumberField;
            }
            set
            {
                this.patientNumberField = value;
            }
        }

        /// <remarks/>
        public string DOB
        {
            get
            {
                return this.dOBField;
            }
            set
            {
                this.dOBField = value;
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
        public string HasPatientWarnings
        {
            get
            {
                return this.hasPatientWarningsField;
            }
            set
            {
                this.hasPatientWarningsField = value;
            }
        }

        /// <remarks/>
        public ulong actionPerformedOn
        {
            get
            {
                return this.actionPerformedOnField;
            }
            set
            {
                this.actionPerformedOnField = value;
            }
        }
    }

    }

