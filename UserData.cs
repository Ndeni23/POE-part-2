using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CyberSecurityBot
{
    public static class UserData
    {
        private static readonly string usersFile = "users.txt";
        private static readonly string questionsFile = "user_questions.txt";
        private static readonly string factsFile = "user_facts.txt";

        public static void SaveUserDetails(string userName)
        {
            try
            {
                if (!File.Exists(usersFile))
                    File.WriteAllText(usersFile, userName + Environment.NewLine);
                else
                {
                    var existing = File.ReadAllLines(usersFile);
                    if (!existing.Contains(userName, StringComparer.OrdinalIgnoreCase))
                        File.AppendAllText(usersFile, userName + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving user: {ex.Message}");
            }
        }

        public static List<string> LoadUserNames
        {
            get
            {
                try
                {
                    return File.Exists(usersFile)
                        ? File.ReadAllLines(usersFile).ToList()
                        : new List<string>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading user names: {ex.Message}");
                    return new List<string>();
                }
            }
        }

        internal static void SaveUserQuestion(string question)
        {
            try
            {
                File.AppendAllLines(questionsFile, new[] { question });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving question: {ex.Message}");
            }
        }

        internal static void SaveUserFact(string fact)
        {
            try
            {
                File.AppendAllLines(factsFile, new[] { fact });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving fact: {ex.Message}");
            }
        }

        public static List<string> LoadUserQuestions()
        {
            try
            {
                return File.Exists(questionsFile)
                    ? File.ReadAllLines(questionsFile).ToList()
                    : new List<string>();
            }
            catch
            {
                return new List<string>();
            }
        }
    }
}