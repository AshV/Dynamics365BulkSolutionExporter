using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;

namespace ExportSolution
{
 public   class Childs
    {
        static string fetchX = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
  <entity name='account'>
    <attribute name='accountid' />
    <attribute name='parentaccountid' />
    <order attribute='name' descending='false' />
    <filter type='and'>
      <condition attribute='parentaccountid' operator='eq' uitype='account' value='{0}' />
    </filter>
  </entity>
</fetch>";

        public static List<Guid> allGuid = new List<Guid>();
        public static void QueryChildAccounts(IOrganizationService service, Guid id)
        {
            var getChild = service.RetrieveMultiple(new FetchExpression(string.Format(fetchX, id)));
            if (getChild.Entities.Count > 0)
            {
                foreach (var record in getChild.Entities)
                {
                    var accId = record.GetAttributeValue<Guid>("accountid");
                    allGuid.Add(accId);
                    QueryChildAccounts(service, accId);
                }
            }
            else
            { return; }
        }

    }
}