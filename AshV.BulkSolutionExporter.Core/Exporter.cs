using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AshV.BulkSolutionExporter.Core
{
    public class Exporter
    {
        private static bool ExportSolutionZip(IOrganizationService orgService, Logger logger, ExportSolutionRequest exportSolutionRequest, string exportPath = "")
        {
            try
            {
                logger.Log($"Started exporting {exportSolutionRequest.SolutionName} - {(exportSolutionRequest.Managed ? "Managed" : "Unmanaged")}");
                exportSolutionRequest.Managed = Convert.ToBoolean(exportSolutionRequest.Managed);

                var exportSolutionResponse = (ExportSolutionResponse)orgService.Execute(exportSolutionRequest);
                byte[] exportXml = exportSolutionResponse.ExportSolutionFile;

                var managed = exportSolutionRequest.Managed ? "_managed" : string.Empty;
                string filename = $"{exportSolutionRequest.SolutionName}{managed}.zip";
                //  string filename = $"{exportSolutionRequest.SolutionName}{FormatVersion(solution.Version)}{managed}.zip";

                if (!string.IsNullOrEmpty(exportPath))
                    filename = $"{Path.GetFullPath(exportPath)}\\{filename}";

                File.WriteAllBytes(filename, exportXml);

                logger.Log($"Exported {exportSolutionRequest.SolutionName} to {filename} successfully.");

                return true;
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
                return false;
            }
        }

        public static void ExportAllSolutionsParallel(string userName, string password, string loginUri, Logger logger, Configuration configuration)
        {
            Parallel.ForEach(configuration.ExportSolutionRequest, (task) =>
            {
                ExportSolutionZip(Connector.GetOrganizationService(userName, password, loginUri, logger), logger, new ExportSolutionRequest
                {
                    SolutionName = task.SolutionName,
                    Managed = task.Managed
                });
            });
        }

        private static string FormatVersion(string version)
        {
            return $"_{version.Replace('.', '_')}";
        }
    }
}