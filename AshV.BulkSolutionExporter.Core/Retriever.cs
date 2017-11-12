using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Collections.Generic;
using System.Linq;

namespace AshV.BulkSolutionExporter.Core
{
    public class Retriever
    {
        public static List<Solution> RetriveAllUnmanagedSolutions(IOrganizationService service, Logger logger)
        {
            var solutions = service.RetrieveMultiple(new QueryExpression("solution")
            {
                ColumnSet = new ColumnSet(true),
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression(nameof(Solution.IsManaged).ToLower(),ConditionOperator.Equal,false),
                        new ConditionExpression(nameof(Solution.IsVisible).ToLower(),ConditionOperator.Equal,true)
                    }
                }
            });

            logger.Log($"Retrieved {solutions.Entities.Count} unmanaged solution(s).");

            var solutionList = new List<Solution>();
            int counter = 0;
            solutions.Entities.ToList().ForEach(solution =>
            {
                var sol = solution.ToEntity<Solution>();
                logger.Log($"Solution {++counter} -> UniqueName[{sol.UniqueName}] FriendlyName[{sol.FriendlyName}].");
                solutionList.Add(sol);
            });

            return solutionList;
        }
    }
}