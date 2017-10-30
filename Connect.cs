using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Diagnostics;
using System.IO;
using System.ServiceModel.Description;

namespace ExportSolution
{
    public class Connect
    {
        static string filePath = "";

        static Connect()
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
        }

        public static IOrganizationService GetOrganizationService(string userName, string password, string orgServiceUri, Logger logger)
        {
            try
            {
                ClientCredentials credentials = new ClientCredentials();
                credentials.UserName.UserName = userName;
                credentials.UserName.Password = password;

                OrganizationServiceProxy proxy = new OrganizationServiceProxy(new Uri(orgServiceUri), null, credentials, null);
                proxy.EnableProxyTypes();
                return proxy;
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

        static void OnProcessExit(object sender, EventArgs e)
        {
            Console.WriteLine($"Your tracing logs has been written to {filePath}");
            Process.Start(filePath);
        }
    }
}