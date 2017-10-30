using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using System.Threading;

namespace ExportSolution
{
    static class GlobalServiceObject
    {
        public static IOrganizationService orgService { get; set; }

        public static ExportConfiguration configuration { get; set; }

        public static Logger logger { get; set; }

        public static void ReInitConnection()
        {
            GlobalServiceObject.orgService = Connect.GetOrganizationService(
                      configuration.Connection.UID,
                      configuration.Connection.PWD,
                      configuration.Connection.EndPoint,
                      logger);

            logger.Log("OrganizationService not initialized waiting for 1 second.");
            Thread.Sleep(1000);

            logger.Log("Exec WhoAmIRequest");
            var wai = orgService.Execute(new WhoAmIRequest());
            logger.Log("End WhoAmIRequest");
        }
    }
}
