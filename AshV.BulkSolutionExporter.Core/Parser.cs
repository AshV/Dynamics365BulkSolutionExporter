using Microsoft.Crm.Sdk.Messages;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace AshV.BulkSolutionExporter.Core
{
    public class Parser
    {
        public static Configuration ConfigurationXmlDeserializer(Logger logger, string fileName)
        {
            try
            {
                logger.Log("Starting Configuration Deserializing.");
                var xmlSerializer = new XmlSerializer(typeof(Configuration));
                using (var streamReader = new StreamReader(fileName))
                {
                    return (Configuration)xmlSerializer.Deserialize(streamReader);
                }
            }
            catch (Exception ex)
            {
                logger.Log($"Configuration Deserializing Failed. {ex.Message}");
                throw;
            }
        }

        public static string ConfigurationXmlSerializer(Logger logger, Configuration configuration)
        {
            try
            {
                logger.Log("Starting Configuration Serializing.");
                var xmlSerializer = new XmlSerializer(typeof(Configuration));
                using (var stringWriter = new StringWriter())
                {
                    using (var xmlWriter = XmlWriter.Create(stringWriter))
                    {
                        xmlSerializer.Serialize(xmlWriter, configuration);
                        return stringWriter.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Log($"Configuration Serializing Failed. {ex.Message}");
                throw;
            }
        }

        static void DummY()
        {
            var a = new ExportSolutionRequest().ExportAutoNumberingSettings;
            var b = new ExportSolutionRequest().ExportCalendarSettings;
            var c = new ExportSolutionRequest().ExportCustomizationSettings;
            var d = new ExportSolutionRequest().ExportEmailTrackingSettings;
            var e = new ExportSolutionRequest().ExportExternalApplications;
            var f = new ExportSolutionRequest().ExportGeneralSettings;
            var g = new ExportSolutionRequest().ExportIsvConfig;
            var h = new ExportSolutionRequest().ExportMarketingSettings;
            var i = new ExportSolutionRequest().ExportOutlookSynchronizationSettings;
            var j = new ExportSolutionRequest().ExportRelationshipRoles;
            var k = new ExportSolutionRequest().ExportSales;
            var l = new ExportSolutionRequest().Managed;
            var m = new ExportSolutionRequest().SolutionName;
            var n = new ExportSolutionRequest().TargetVersion;
        }
    }
}