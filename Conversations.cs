using System;

namespace CyberSecurityBot
{
    public class Conversation
    {
        public string LastSentinent { get; set; }

        private string currentTopic = "general";

        public void UpdateTopic(string newTopic)
        {
            if (!string.IsNullOrEmpty(newTopic))
                currentTopic = newTopic;
        }

        public string GetTopic()
        {
            return currentTopic;
        }
    }
}