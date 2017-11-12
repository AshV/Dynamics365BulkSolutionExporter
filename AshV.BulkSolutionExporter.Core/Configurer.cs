using System;
using System.Collections.Generic;
using System.Text;

namespace AshV.BulkSolutionExporter.Core
{
    public class Configurer
    {
        public static Configuration ExportConfiguration
        {
            get
            {
                exportConfiguration.ExportSolutionRequest = requestList.ToArray();
                return exportConfiguration;
            }
            set
            {
                exportConfiguration = value;
            }
        }

        private static List<ConfigurationExportSolutionRequest> requestList = new List<ConfigurationExportSolutionRequest>();

        private static Configuration exportConfiguration = new Configuration();

        public static void AddSolution(Solution solution)
        {
            requestList.Add(new ConfigurationExportSolutionRequest
            {
                SolutionName = solution.UniqueName,
                Managed = true
            });
            requestList.Add(new ConfigurationExportSolutionRequest
            {
                SolutionName = solution.UniqueName,
                Managed = false
            });
        }
    }
}