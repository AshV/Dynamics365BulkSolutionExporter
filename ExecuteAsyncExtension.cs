using Microsoft.Xrm.Sdk;
using System.Threading;
using System.Threading.Tasks;

namespace ExportSolution
{
    public static class ExecuteAsyncExtension
    {
        public async static Task<T> ExecuteAsync<T>(this IOrganizationService sdk, OrganizationRequest request,Logger logger) where T : OrganizationResponse
        {
            if (sdk == null)
            {
                logger.Log("OrganizationService not initialized waiting for 1 second.");
                Thread.Sleep(1000);
            }

            var t = Task.Factory.StartNew(() =>
            {
                var response = sdk.Execute(request) as T;
                return response;
            });

            return await t;
        }
    }
}