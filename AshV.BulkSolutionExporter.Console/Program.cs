using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AshV.BulkSolutionExporter.Core;

namespace AshV.BulkSolutionExporter.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var sol = Retriever.RetriveAllUnmanagedSolutions(
                Connector.GetOrganizationService("Ashish@AshishV.onMicrosoft.com", "", "http://AshishV.crm.dynamics.com", Logger.GetLogger("Log")),
                Logger.GetLogger("Log"));
        }
    }
}