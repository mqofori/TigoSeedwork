using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TigoSeedwork.Domain
{
    public abstract class BrokenRuleMessages
    {
        #region Abstract Methods
        protected abstract void PopulateMessages();
        #endregion
        private Dictionary<string, string> messages;


        /// <summary>
        /// error dictionary, key and value
        /// </summary>
        protected Dictionary<string, string> Messages
        {
            get { return this.messages; }
        }

        /// <summary>
        /// Get list of broken rules 
        /// </summary>
        protected BrokenRuleMessages()
        {
            this.messages = new Dictionary<string, string>();
            this.PopulateMessages();
        }

        /// <summary>
        /// Get rule description 
        /// </summary>
        /// <param name="messageKey"></param>
        /// <returns></returns>
        public string GetRuleDescription(string messageKey)
        {
            string description = string.Empty;
            if (this.messages.ContainsKey(messageKey))
            {
                description = this.messages[messageKey];
            }
            return description;
        }
    }
}
