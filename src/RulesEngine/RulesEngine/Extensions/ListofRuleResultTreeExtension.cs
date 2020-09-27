// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using RulesEngine.Models;
using System.Collections.Generic;
using System.Linq;


namespace RulesEngine.Extensions
{
    public static class ListofRuleResultTreeExtension
    {
        public delegate void OnSuccessFunc(string eventName);
        public delegate void OnSuccessWithRuleNameFunc(string eventName, string ruleName = null);
        public delegate void OnFailureFunc();

        public static List<RuleResultTree> OnSuccess(this List<RuleResultTree> ruleResultTrees, OnSuccessFunc onSuccessFunc)
        {
            return ruleResultTrees.OnSuccess((eventName, _) => onSuccessFunc(eventName));
        }

        public static List<RuleResultTree> OnSuccess(this List<RuleResultTree> ruleResultTrees, OnSuccessWithRuleNameFunc onSuccessFunc)
        {
            var successfulRuleResult = ruleResultTrees.FirstOrDefault(ruleResult => ruleResult.IsSuccess == true);
            if (successfulRuleResult != null)
            {
                var eventName = successfulRuleResult?.FormattedSuccessEvent ??                      
                            successfulRuleResult.Rule.SuccessEvent ?? successfulRuleResult.Rule.RuleName;
                onSuccessFunc(eventName, successfulRuleResult.Rule.RuleName);
            }

            return ruleResultTrees;
        }

        public static List<RuleResultTree> OnFail(this List<RuleResultTree> ruleResultTrees, OnFailureFunc onFailureFunc)
        {
            bool allFailure = ruleResultTrees.All(ruleResult => ruleResult.IsSuccess == false);
            if (allFailure)
                onFailureFunc();
            return ruleResultTrees;
        }
    }
}
