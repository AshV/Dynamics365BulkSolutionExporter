using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ExportSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = Connect.GetLoggingService("Logs");
            try
            {
                if (args.Length == 0)
                {
                    logger.Log("Please pass path to configuration file in Command Line Argument");
                    return;
                }

                var configuration = ExportSolution.ParseConfigurationAndConnectToCRM(logger, args[0]);

                GlobalServiceObject.configuration = configuration;

                GlobalServiceObject.logger = logger;

                GlobalServiceObject.orgService = Connect.GetOrganizationService(
                    configuration.Connection.UID,
                    configuration.Connection.PWD,
                    configuration.Connection.EndPoint,
                    logger);


                Childs.QueryChildAccounts(GlobalServiceObject.orgService, new Guid("{8435CDE7-ABC0-E711-A826-000D3AB48443}"));


                var sols = GlobalServiceObject.orgService.RetrieveMultiple(new QueryExpression("solution")
                {
                    ColumnSet = new ColumnSet(true),
                    Criteria = new FilterExpression
                    {
                        Conditions =
                        {
                            new ConditionExpression(nameof(Solution.IsManaged).ToLower(),ConditionOperator.Equal,false)
                        }
                    }
                });

                var exportSolutionData = new List<ExportConfigurationSolution>();

                sols.Entities.ToList().ForEach(sol =>
                {
                    var s = sol.ToEntity<Solution>();
                    if (!s.UniqueName.Contains("Active")
                    && !s.UniqueName.Contains("Default")
                    && !s.UniqueName.Contains("Basic"))
                    {
                        exportSolutionData.Add(new ExportConfigurationSolution
                        {
                            UniqueName = s.UniqueName,
                            Version = s.Version,
                            Managed = "false"
                        });
                    }
                });

                ExportSolution.ExportAllSolutions(
                    GlobalServiceObject.orgService,
                    logger,
                    new ExportConfiguration()
                    {
                        Connection = configuration.Connection,
                        Solutions = exportSolutionData.ToArray()
                    });


                //  ExportSolution.ExportAllSolutions(orgService, logger, configuration);

                //   ExportSolution.ExecuteExtractScript(logger, configuration);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();

                logger.Log($"Exception Log : {ex.Message}, {st}, {frame}, {line}");
            }
        }
    }
}