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
            var solutions = Retriever.RetriveAllUnmanagedSolutions(
                Connector.GetOrganizationService("Ashish@AshishV.onMicrosoft.com", "p w d", "http://AshishV.crm.dynamics.com", logger),
                logger);

            solutions.ForEach(sol => { Configurer.AddSolution(sol); });

            var stringConfiguration = Parser.ConfigurationXmlSerializer(logger, Configurer.ExportConfiguration);

            File.WriteAllText("cfg.xml", stringConfiguration);
        }
    }
}