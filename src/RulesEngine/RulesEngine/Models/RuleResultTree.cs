﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using RulesEngine.HelperFunctions;
using System.Collections.Generic;

namespace RulesEngine.Models
{
    /// <summary>
    /// Rule result class with child result heirarchy
    /// </summary>
    public class RuleResultTree
    {
        /// <summary>
        /// Gets or sets the rule.
        /// </summary>
        /// <value>
        /// The rule.
        /// </value>
        public Rule Rule { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is success; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the child result.
        /// </summary>
        /// <value>
        /// The child result.
        /// </value>
        public IEnumerable<RuleResultTree> ChildResults { get; set; }

        /// <summary>
        /// Gets or sets the input object
        /// </summary>
        public object Input { get; set; }

        /// <summary>
        /// This method will return all the error and warning messages to caller
        /// </summary>
        /// <returns>RuleResultMessage</returns>
        public RuleResultMessage GetMessages()
        {
            RuleResultMessage ruleResultMessage = new RuleResultMessage();

            Helpers.ToResultTreeMessages(this, ref ruleResultMessage);

            return ruleResultMessage;
        }
    }

    /// <summary>
    /// This class will hold the error messages
    /// </summary>
    public class RuleResultMessage
    {
        /// <summary>
        /// Constructor will innitilaze the List 
        /// </summary>
        public RuleResultMessage()
        {
            ErrorMessages = new List<string>();
            WarningMessages = new List<string>();
        }

        /// <summary>
        /// This will hold the list of error messages
        /// </summary>
        public List<string> ErrorMessages { get; set; }

        /// <summary>
        /// This will hold the list of warning messages
        /// </summary>
        public List<string> WarningMessages { get; set; }
    }
}