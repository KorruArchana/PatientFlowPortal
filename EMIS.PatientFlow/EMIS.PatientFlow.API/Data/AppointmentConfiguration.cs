﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

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
public partial class AppointmentConfiguration
{

	private AppointmentConfigurationSiteListSite[] siteListField;

    private AppointmentConfigurationHolder[] holderListField;

    private AppointmentConfigurationSchedule[] scheduleListField;

    private AppointmentConfigurationSlotType[] slotTypeListField;

    /// <remarks/>
	[System.Xml.Serialization.XmlArrayItemAttribute("Site", IsNullable = false)]
	public AppointmentConfigurationSiteListSite[] SiteList
    {
        get
        {
            return this.siteListField;
        }
        set
        {
            this.siteListField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Holder", IsNullable = false)]
    public AppointmentConfigurationHolder[] HolderList
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

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Schedule", IsNullable = false)]
    public AppointmentConfigurationSchedule[] ScheduleList
    {
        get
        {
            return this.scheduleListField;
        }
        set
        {
            this.scheduleListField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("SlotType", IsNullable = false)]
    public AppointmentConfigurationSlotType[] SlotTypeList
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
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AppointmentConfigurationSiteList
{

    private AppointmentConfigurationSiteListSite siteField;

    /// <remarks/>
    public AppointmentConfigurationSiteListSite Site
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
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AppointmentConfigurationSiteListSite
{

    private int dBIDField;

    private int refIDField;

    private string nameField;

    /// <remarks/>
    public int DBID
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
    public int RefID
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
public partial class AppointmentConfigurationHolder
{

    private int dBIDField;

    private int refIDField;

    private string titleField;

    private string firstNamesField;

    private string surnameField;

    /// <remarks/>
    public int DBID
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

    /// <remarks/
    public int  RefID
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
    public string Surname
    {
        get
        {
            return this.surnameField;
        }
        set
        {
            this.surnameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AppointmentConfigurationSchedule
{

    private string nameField;

    private string startTimeField;

    private string endTimeField;

    //private byte slotLengthField;

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
    public string StartTime
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
    public string EndTime
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
    //public byte SlotLength
    //{
    //    get
    //    {
    //        return this.slotLengthField;
    //    }
    //    set
    //    {
    //        this.slotLengthField = value;
    //    }
    //}
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class AppointmentConfigurationSlotType
{

    private int idField;

    private string displayField;

    private string descriptionField;

    private string bookableField;

    private bool eMISAccessField;

    /// <remarks/>
    public int ID
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }

    /// <remarks/>
    public string Display
    {
        get
        {
            return this.displayField;
        }
        set
        {
            this.displayField = value;
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
    public string Bookable
    {
        get
        {
            return this.bookableField;
        }
        set
        {
            this.bookableField = value;
        }
    }

    /// <remarks/>
    public bool EMISAccess
    {
        get
        {
            return this.eMISAccessField;
        }
        set
        {
            this.eMISAccessField = value;
        }
    }
}
