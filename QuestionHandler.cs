using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberSecurityBot
{
    public class QuestionHandler
    {
        private List<string> ignore = new List<string>();
        private Dictionary<string, List<string>> keywordGroups = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> responses = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> usedResponses = new Dictionary<string, List<string>>();
        private ChatMemory memory;
        private Conversation conversation;

        public delegate string ResponseSelector(List<string> responseList, List<string> usedList);
        private ResponseSelector selectResponse = (responseList, usedList) =>
        {
            var availableResponses = responseList.Where(r => !usedList.Contains(r)).ToList();
            if (!availableResponses.Any())
            {
                usedList.Clear();
                availableResponses = responseList.ToList();
            }
            var response = availableResponses[new Random().Next(availableResponses.Count)];
            usedList.Add(response);
            return response;
        };

        public QuestionHandler(ChatMemory memory)
        {
            this.memory = memory;
            this.conversation = new Conversation();
            StoreIgnore();
            StoreKeywordGroups();
            StoreReplies();
        }

        public void HandleQuestions(string userName)
        {
            string lastQuestion = "";

            while (true)
            {
                Console.WriteLine("Chat AI-> Enter your question (or 'exit' to go back to the main menu):");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(userName + " -> ");
                string question = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Cyan;

                if (string.IsNullOrWhiteSpace(question))
                {
                    Console.WriteLine("Chat AI-> Please ask a question.");
                    continue;
                }

                question = question.Trim();
                lastQuestion = question;
                UserData.SaveUserQuestion(question);

                if (question.ToLower() == "exit")
                {
                    Console.WriteLine("Goodbye! " + userName);
                    return;
                }

                if (question.ToLower().StartsWith("remember"))
                {
                    if (question.Length <= "remember".Length || question.Substring("remember".Length).Trim().Length == 0)
                    {
                        Console.WriteLine("Chat AI-> Please tell me what to remember. For example, 'remember I use a VPN'.");
                        continue;
                    }
                    string fact = question.Substring("remember".Length).Trim();
                    memory.StoreFact(fact);
                    UserData.SaveUserFact(fact); // Removed incorrect assignment to 'object value'
                    Console.WriteLine("Chat AI-> Got it! I’ll remember that.");
                    continue;
                }

                if (question.ToLower().Contains("what do you remember"))
                {
                    string recall = memory.RecallFacts();
                    Console.WriteLine($"Chat AI-> {recall}");
                    continue;
                }

                if (question.ToLower().Contains("how are you"))
                {
                    Console.WriteLine("Chat AI-> I'm doing great, thanks for asking! How can I help you with cybersecurity today?");
                    continue;
                }

                if (question.ToLower().Contains("what is my name"))
                {
                    Console.WriteLine($"Chat AI-> Your name is {userName}! How can I assist you today?");
                    continue;
                }

                if (question.ToLower().Contains("what did i ask"))
                {
                    if (string.IsNullOrEmpty(lastQuestion))
                    {
                        Console.WriteLine("Chat AI-> You haven't asked anything yet!");
                    }
                    else
                    {
                        Console.WriteLine($"Chat AI-> You last asked: '{lastQuestion}'. How can I help you now?");
                    }
                    continue;
                }

                string response = GetResponse(question, userName);
                Console.WriteLine($"Chat AI-> {response}");
            }
        }

        private string DetectTopic(string input)
        {
            input = input.ToLower().Trim();

            foreach (var group in keywordGroups)
            {
                foreach (var keyword in group.Value)
                {
                    if (input.Contains(keyword))
                    {
                        conversation.UpdateTopic(group.Key);
                        return group.Key;
                    }
                }
            }

            if (input.Contains("more") || input.Contains("continue") || input.Contains("what else") ||
                input.Contains("go on") || input.Contains("details") || input.Contains("keep going") ||
                input.Contains("tell me more"))
            {
                string currentTopic = conversation.GetTopic();
                if (currentTopic != "general" && responses.ContainsKey(currentTopic))
                {
                    return currentTopic;
                }
            }

            return "default";
        }

        private string GetResponse(string input, string userName)
        {
            input = input.ToLower().Trim();
            string topic = DetectTopic(input);
            string sentinent = SentinentAnalyzer.AnalyzeSentinent(input);

            if (!usedResponses.ContainsKey(topic))
            {
                usedResponses[topic] = new List<string>();
            }

            var responseList = responses[topic];
            string response = selectResponse(responseList, usedResponses[topic]);

            if (sentinent == "worried")
            {
                response = $"I understand you're feeling {sentinent}, {userName}. Here's some advice: {response}";
            }
            else if (sentinent == "positive")
            {
                response = $"I'm glad you're feeling {sentinent}, {userName}! {response}";
            }

            string facts = memory.RecallFacts();
            if (!string.IsNullOrEmpty(facts) && facts != "I don't remember anything yet.")
            {
                foreach (var fact in facts.Split(new[] { "; " }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (fact.ToLower().Contains("vpn") && topic != "vpn")
                    {
                        return $"Hey {userName}, you mentioned VPN earlier. Using a VPN can add an extra layer of protection!";
                    }
                    if (fact.ToLower().Contains("privacy") && topic != "privacy")
                    {
                        return $"Hey {userName}, I remember you're interested in privacy. Would you like to know more about protecting your data?";
                    }
                }
            }

            return response;
        }

        private void StoreKeywordGroups()
        {
            keywordGroups.Add("password", new List<string> { "password", "credentials", "login" });
            keywordGroups.Add("phishing", new List<string> { "phishing", "scam", "fraud", "spoof" });
            keywordGroups.Add("malware", new List<string> { "malware", "virus", "trojan", "spyware", "ransomware" });
            keywordGroups.Add("firewall", new List<string> { "firewall", "network security", "port" });
            keywordGroups.Add("vpn", new List<string> { "vpn", "virtual private network" });
            keywordGroups.Add("encryption", new List<string> { "encryption", "encrypt", "secure data" });
            keywordGroups.Add("2fa", new List<string> { "2fa", "two-factor", "multi-factor", "authentication" });
            keywordGroups.Add("antivirus", new List<string> { "antivirus", "anti-virus", "security software" });
            keywordGroups.Add("social engineering", new List<string> { "social engineering", "manipulation", "deception" });
            keywordGroups.Add("cybersecurity", new List<string> { "cybersecurity", "cyber security", "online safety" });
            keywordGroups.Add("hacking", new List<string> { "hacking", "hack", "ethical hacking", "cybercrime" });
        }

        private void StoreReplies()
        {
            responses.Add("password", new List<string>
            {
                "Always use strong passwords with a mix of letters, numbers, and symbols.",
                "Consider using a password manager to keep track of your credentials.",
                "Avoid using easily guessable information in your passwords."
            });
            responses.Add("phishing", new List<string>
            {
                "Be cautious of emails or messages asking for personal details; verify the sender.",
                "Always verify the sender's email address before clicking on links.",
                "If an email seems suspicious, don't click on any links or attachments."
            });
            responses.Add("malware", new List<string>
            {
                "Avoid downloading files from unknown sources to prevent malware infections.",
                "Never open suspicious attachments, and always back up your important files.",
                "Regularly scan your system for malware."
            });
            responses.Add("firewall", new List<string>
            {
                "Firewalls help block unauthorized access to your network.",
                "Ensure your firewall is enabled and properly configured.",
                "Regularly check your firewall settings to maintain security."
            });
            responses.Add("vpn", new List<string>
            {
                "A VPN encrypts your internet traffic, keeping your online activity private.",
                "Use a reputable VPN service to enhance your online security.",
                "VPNs are especially useful when using public Wi-Fi networks."
            });
            responses.Add("encryption", new List<string>
            {
                "Encryption helps protect your sensitive data from unauthorized access.",
                "Use HTTPS websites to ensure encrypted communication.",
                "Consider encrypting your emails for added security."
            });
            responses.Add("2fa", new List<string>
            {
                "Two-Factor Authentication adds an extra layer of security to your accounts.",
                "Enable 2FA on all your important accounts whenever possible.",
                "Use an authenticator app or SMS codes for 2FA."
            });
            responses.Add("antivirus", new List<string>
            {
                "Keep your antivirus software updated to protect against threats.",
                "Install reputable antivirus software and keep it updated.",
                "Regularly run antivirus scans to detect potential threats."
            });
            responses.Add("social engineering", new List<string>
            {
                "Cybercriminals use manipulation to steal confidential information, stay alert.",
                "Be wary of unsolicited requests for personal information.",
                "Educate yourself about common social engineering tactics."
            });
            responses.Add("cybersecurity", new List<string>
            {
                "Cybersecurity is the practice of protecting systems and data from digital attacks.",
                "Stay informed about the latest cybersecurity threats and best practices.",
                "Regularly update your software to protect against vulnerabilities."
            });
            responses.Add("hacking", new List<string>
            {
                "Ethical hackers help organizations secure their systems, but illegal hacking is a crime.",
                "Protect your accounts to prevent unauthorized access by hackers.",
                "Learn about ethical hacking to better understand cybersecurity."
            });
            responses.Add("default", new List<string>
            {
                "I couldn't find an answer. Try rephrasing your question.",
                "Let's focus on cybersecurity topics. Can you rephrase your question?",
                "I can help with topics like phishing, passwords, and safe browsing."
            });
        }

        private void StoreIgnore()
        {
            ignore.AddRange(new List<string>
            {
                "is", "the", "a", "an", "what", "how", "why", "when", "where", "does", "do",
                "are", "can", "could", "should", "would", "i", "me", "my", "you", "your", "we", "our"
            });
        }
    }
}