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
                logger.Log($"Started exporting {exportSolutionRequest.SolutionName}");
                exportSolutionRequest.Managed = Convert.ToBoolean(exportSolutionRequest.Managed);

                var exportSolutionResponse = (ExportSolutionResponse)orgService.Execute(exportSolutionRequest);
                byte[] exportXml = exportSolutionResponse.ExportSolutionFile;

                var managed = exportSolutionRequest.Managed ? "_managed" : string.Empty;
                string filename = $"{exportSolutionRequest.SolutionName}{FormatVersion(solution.Version)}{managed}.zip";
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

        public static void ExportAllSolutions(IOrganizationService orgService, Logger logger, ExportConfiguration configuration)
        {
            Parallel.ForEach(configuration.Solutions, (task) =>
            {
                ExportSolutionZip(orgService, logger, new ExportSolutionRequest(), new Solution(), "root");
            });
        }

        private static string FormatVersion(string version)
        {
            return $"_{version.Replace('.', '_')}";
        }
    }
}