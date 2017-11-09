using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AshV.BulkSolutionExporter.Core
{
    class Connector
    {
        static string filePath = "";


        public static IOrganizationService GetOrganizationService(string userName, string password, string orgServiceUri, Logger logger)
        {
            try
            {
                // Connect to the CRM web service using a connection string.
                var conn = new CrmServiceClient($@"Url=https://AshishV.crm.dynamics.com; Username={userName}; Password={password}; authtype=Office365");

                // Cast the proxy client to the IOrganizationService interface.
                return conn.OrganizationWebProxyClient != null ? conn.OrganizationWebProxyClient : (IOrganizationService)conn.OrganizationServiceProxy;
            }
            catch (Exception ex)
            {
                string log = "Error while connecting to CRM " + ex.Message;
                logger.Log(log);
                return null;
            }
        }

        public static Logger GetLoggingService(string fileToWriteTracelogs)
        {
            filePath = Path.GetFullPath(fileToWriteTracelogs + "_" + Path.GetRandomFileName() + ".txt");
            return Logger.GetLogger(filePath);
        }
    }
}