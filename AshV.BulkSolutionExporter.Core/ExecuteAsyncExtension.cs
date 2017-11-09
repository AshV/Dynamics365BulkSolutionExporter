using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AshV.BulkSolutionExporter.Core
{
  static  class  ExecuteAsyncExtension
    {
        public async static Task<T> ExecuteAsync<T>(this IOrganizationService sdk, OrganizationRequest request, Logger logger) where T : OrganizationResponse
        {
            try
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
            catch (Exception ex)
            {
                var log = ex.Message + Environment.NewLine;
                foreach (var param in request.Parameters)
                {
                    log += param.Key + " : " + param.Value + Environment.NewLine;
                }
                logger.Log(log);

             

                return default(T);
            }
        }

    }
}
