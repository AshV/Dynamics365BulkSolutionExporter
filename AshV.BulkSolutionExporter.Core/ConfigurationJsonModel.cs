
public class Rootobject
{
    public Configuration Configuration { get; set; }
}

public class Configuration
{
    public Exportsolutionrequest[] ExportSolutionRequest { get; set; }
}

public class Exportsolutionrequest
{
    public string SolutionName { get; set; }
    public string Managed { get; set; }
    public string ExportAutoNumberingSettings { get; set; }
    public string ExportCalendarSettings { get; set; }
    public string ExportCustomizationSettings { get; set; }
    public string ExportEmailTrackingSettings { get; set; }
    public string ExportExternalApplications { get; set; }
    public string ExportGeneralSettings { get; set; }
    public string ExportIsvConfig { get; set; }
    public string ExportMarketingSettings { get; set; }
    public string ExportOutlookSynchronizationSettings { get; set; }
    public string ExportSales { get; set; }
    public string TargetVersion { get; set; }
    public string ExportRelationshipRoles { get; set; }
}
