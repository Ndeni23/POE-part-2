using System;
using System.Collections.Generic;

namespace CyberSecurityBot
{
    public class ChatMemory
    {
        private List<string> facts = new List<string>();

        public void StoreFact(string fact)
        {
            facts.Add(fact);
        }

        public string RecallFacts()
        {
            return facts.Count == 0 ? "I don't remember anything yet." : string.Join("; ", facts);
        }
    }
}