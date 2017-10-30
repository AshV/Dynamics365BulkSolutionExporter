
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class ExportConfiguration
{

    private ExportConfigurationConnection connectionField;

    private ExportConfigurationSolution[] solutionsField;

    private string exportPathField;

    private string extractScriptField;

    private string extractScriptArgsField;

    /// <remarks/>
    public ExportConfigurationConnection Connection
    {
        get
        {
            return this.connectionField;
        }
        set
        {
            this.connectionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Solution", IsNullable = false)]
    public ExportConfigurationSolution[] Solutions
    {
        get
        {
            return this.solutionsField;
        }
        set
        {
            this.solutionsField = value;
        }
    }

    /// <remarks/>
    public string ExportPath
    {
        get
        {
            return this.exportPathField;
        }
        set
        {
            this.exportPathField = value;
        }
    }

    /// <remarks/>
    public string ExtractScript
    {
        get
        {
            return this.extractScriptField;
        }
        set
        {
            this.extractScriptField = value;
        }
    }

    /// <remarks/>
    public string ExtractScriptArgs
    {
        get
        {
            return this.extractScriptArgsField;
        }
        set
        {
            this.extractScriptArgsField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ExportConfigurationConnection
{

    private string uIDField;

    private string pWDField;

    private string endPointField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string UID
    {
        get
        {
            return this.uIDField;
        }
        set
        {
            this.uIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string PWD
    {
        get
        {
            return this.pWDField;
        }
        set
        {
            this.pWDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string EndPoint
    {
        get
        {
            return this.endPointField;
        }
        set
        {
            this.endPointField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class ExportConfigurationSolution
{

    private string uniqueNameField;

    private bool managedField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string UniqueName
    {
        get
        {
            return this.uniqueNameField;
        }
        set
        {
            this.uniqueNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public bool Managed
    {
        get
        {
            return this.managedField;
        }
        set
        {
            this.managedField = value;
        }
    }
}

