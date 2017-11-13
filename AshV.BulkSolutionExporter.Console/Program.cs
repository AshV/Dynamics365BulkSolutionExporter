using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AshV.BulkSolutionExporter.Core;
using System.IO;

namespace AshV.BulkSolutionExporter.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = Logger.GetLogger("Log");
            var UID = "Ashish@AshishV.onMicrosoft.com";
            var PWD = "#";
            var URI = "http://AshishV.crm.dynamics.com";
            var service = Connector.GetOrganizationService(UID, PWD, URI, logger);
            var solutions = Retriever.RetriveAllUnmanagedSolutions(service, logger);

            solutions.ForEach(sol => { Configurer.AddSolution(sol); });

            var stringConfiguration = Parser.ConfigurationXmlSerializer(logger, Configurer.ExportConfiguration);

            File.WriteAllText("cfg.xml", stringConfiguration);

            Exporter.ExportAllSolutionsParallel(UID, PWD, URI, logger, Configurer.ExportConfiguration);
        }
    }
}