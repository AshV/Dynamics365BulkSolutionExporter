using System;
using System.Diagnostics;

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

                var orgService = Connect.GetOrganizationService(
                    configuration.Connection.UID,
                    configuration.Connection.PWD,
                    configuration.Connection.EndPoint,
                    logger);

                ExportSolution.ExportAllSolutions(orgService, logger, configuration);

                ExportSolution.ExecuteExtractScript(logger, configuration);
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