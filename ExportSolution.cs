using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ExportSolution
{
    class ExportSolution
    {
        public static ExportConfiguration ParseConfigurationAndConnectToCRM(Logger logger, string fileName)
        {
            ExportConfiguration importConfig = null;
            try
            {
                logger.Log("Starting Configuration Parsing.");
                XmlSerializer serializer = new XmlSerializer(typeof(ExportConfiguration));

                StreamReader reader = new StreamReader(fileName);
                importConfig = (ExportConfiguration)serializer.Deserialize(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                logger.Log($"Configuration Parsing Failed. {ex.Message}");
                throw;
            }
            return importConfig;
        }

        private static async Task<bool> ExportSolutionZip(IOrganizationService orgService, Logger logger, ExportConfigurationSolution solution, string exportPath = "")
        {
            try
            {
                logger.Log($"Started exporting {solution.UniqueName}");
                var exportSolutionRequest = new ExportSolutionRequest();
                exportSolutionRequest.Managed = Convert.ToBoolean(solution.Managed);
                exportSolutionRequest.SolutionName = solution.UniqueName;

                var exportSolutionResponse = await orgService.ExecuteAsync<ExportSolutionResponse>(exportSolutionRequest, logger);

                // var exportSolutionResponse = (ExportSolutionResponse)orgService.Execute(exportSolutionRequest);
                byte[] exportXml = exportSolutionResponse.ExportSolutionFile;

                string filename = $"{solution.UniqueName}{FormatVersion(solution.Version)}.zip";
                if (!string.IsNullOrEmpty(exportPath))
                    filename = Path.GetFullPath(exportPath) + '\\' + filename;

                File.WriteAllBytes(filename, exportXml);

                logger.Log($"Exported {solution.UniqueName} to {filename} successfully.");

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
            var taskList = new List<Task>();
            foreach (var solution in configuration.Solutions)
            {
                taskList.Add(ExportSolutionZip(orgService, logger, solution, configuration.ExportPath));
            }

            Task.WaitAll(taskList.ToArray());
        }

        private static string FormatVersion(string version)
        {
            return $"_{version.Replace('.', '_')}";
        }

        public static void ExecuteExtractScript(Logger logger, ExportConfiguration configuration)
        {
            if (string.IsNullOrEmpty(configuration.ExtractScript))
                return;

            var process = new ProcessStartInfo
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = configuration.ExtractScript
            };
            if (!string.IsNullOrEmpty(configuration.ExtractScriptArgs))
            {
                process.Arguments = configuration.ExtractScriptArgs;
            }

            logger.Log(Process.Start(process).StandardOutput.ReadToEnd());
        }
    }
}