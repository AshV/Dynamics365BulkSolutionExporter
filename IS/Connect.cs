using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Diagnostics;
using System.IO;
using System.ServiceModel.Description;

namespace IS
{
    public class Connect
    {
        static bool isFileTraceLogOn = false;
        static string filePath = "";

        static Connect()
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
        }

        public static IOrganizationService GetOrganizationService(string userName, string password, string orgServiceUri, ITracingService tracingService = null)
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
                if (tracingService != null)
                    tracingService.Trace(log);
                else
                {
                    Console.WriteLine(log);
                    Console.ReadKey();
                }
                return null;
            }
        }

        public static ITracingService GetTracingService()
        {
            return new FakeTracingService();
        }

        public static ITracingService GetTracingService(string fileToWriteTracelogs)
        {
            isFileTraceLogOn = true;
            filePath = Path.GetFullPath(fileToWriteTracelogs + "_" + Path.GetRandomFileName() + ".txt");
            return new FakeTracingService(filePath);
        }

        static void OnProcessExit(object sender, EventArgs e)
        {
            if (isFileTraceLogOn)
            {
                Console.WriteLine($"Your tracing logs has been written to {filePath}");
                Process.Start(filePath);
            }
        }
    }
}