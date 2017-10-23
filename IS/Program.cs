using System;
using System.Diagnostics;

namespace IS
{
    class Program
    {
        static void Main(string[] args)
        {
            var tracingService = Connect.GetTracingService("Logs");
            try
            {
                if (args.Length == 0)
                {
                    tracingService.Trace("Please pass path to configuration file in Command Line Argument");
                    return;
                }

                var configuration = ExportSolution.ParseConfigurationAndConnectToCRM(tracingService, args[0]);

                var orgService = Connect.GetOrganizationService(
                    configuration.Connection.UID,
                    configuration.Connection.PWD,
                    configuration.Connection.EndPoint,
                    tracingService);

                ExportSolution.ExportAllSolutions(orgService, tracingService, configuration);

                if (!string.IsNullOrEmpty(configuration.ExtractScript))
                    Process.Start(configuration.ExtractScript);
            }
            catch (Exception ex)
            {
                var st = new StackTrace(ex, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();

                tracingService.Trace($"Exception Log : {ex.Message}, {st}, {frame}, {line}");
            }
        }
    }
}