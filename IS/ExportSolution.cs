using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IS
{
    class ExportSolution
    {
        public static ExportConfiguration ParseConfigurationAndConnectToCRM(ITracingService tracingService, string fileName)
        {
            ExportConfiguration importConfig = null;
            try
            {
                tracingService.Trace("Starting Configuration Parsing.");
                XmlSerializer serializer = new XmlSerializer(typeof(ExportConfiguration));

                StreamReader reader = new StreamReader(fileName);
                importConfig = (ExportConfiguration)serializer.Deserialize(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                tracingService.Trace($"Configuration Parsing Failed. {ex.Message}");
                throw;
            }
            return importConfig;
        }

        public static async Task<bool> ExportSolutionZip(IOrganizationService orgService, ITracingService tracingService, string solutionUniqueName, bool managed, string exportPath = "")
        {
            try
            {
                tracingService.Trace($"Started exporting {solutionUniqueName}");
                var exportSolutionRequest = new ExportSolutionRequest();
                exportSolutionRequest.Managed = managed;
                exportSolutionRequest.SolutionName = solutionUniqueName;

                var exportSolutionResponse = await orgService.ExecuteAsync<ExportSolutionResponse>(exportSolutionRequest);
                byte[] exportXml = exportSolutionResponse.ExportSolutionFile;

                string filename = solutionUniqueName + (managed ? "_managed" : "") + ".zip";
                if (!string.IsNullOrEmpty(exportPath))
                    filename = Path.GetFullPath(exportPath) + '\\' + solutionUniqueName + (managed ? "_managed" : "") + ".zip";

                File.WriteAllBytes(filename, exportXml);

                tracingService.Trace($"Exported {solutionUniqueName} to {filename} successfully.");

                return true;
            }
            catch (Exception ex)
            {
                tracingService.Trace(ex.Message);
                return false;
            }
        }

        public static void ExportAllSolutions(IOrganizationService orgService, ITracingService tracingService, ExportConfiguration configuration)
        {
            var taskList = new List<Task>();
            foreach (var solution in configuration.Solutions)
            {
                taskList.Add(ExportSolutionZip(orgService, tracingService, solution.UniqueName, Convert.ToBoolean(solution.Managed), configuration.ExportPath));
            }

            Task.WaitAll(taskList.ToArray());
        }
    }
}