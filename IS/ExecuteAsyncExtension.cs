using Microsoft.Xrm.Sdk;
using System.Threading.Tasks;

namespace IS
{
    public static class ExecuteAsyncExtension
    {
        public async static Task<T> ExecuteAsync<T>(this IOrganizationService sdk, OrganizationRequest request) where T : OrganizationResponse
        {
            var t = Task.Factory.StartNew(() =>
            {
                var response = sdk.Execute(request) as T;
                return response;
            });

            return await t;
        }
    }
}