using System;

namespace CyberSecurityBot
{
    public class ChatBot
    {
        private string name;
        private AudioImageHandler mediaHandler;
        private QuestionHandler questionHandler;
        private ChatMemory memory;

        public ChatBot()
        {
            mediaHandler = new AudioImageHandler();
            memory = new ChatMemory();
            questionHandler = new QuestionHandler(memory);
        }

        public void Run()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            ShowLoading();
            mediaHandler.DisplayLogo();
            mediaHandler.PlayWelcomeAudio();
            WelcomeUser();
            Menu();
        }

        private void ShowLoading(int seconds = 3)
        {
            Console.Write("\n Starting up chatbot AI");
            for (int i = 0; i < seconds; i++)
            {
                Console.Write(".");
                System.Threading.Thread.Sleep(1000);
            }
            Console.WriteLine();
        }

        private void WelcomeUser()
        {
            Console.WriteLine("\n===================================================================================================");
            Console.WriteLine("CHAT AI-> Hello! Welcome to the Cybersecurity Awareness Bot. I'm here to help you stay safe online.");
            Console.WriteLine("===================================================================================================");
            Console.WriteLine("Chat AI-> Please enter your full name:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("User-> ");
            name = Console.ReadLine()?.Trim() ?? "User";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n===================================================================================================");
            Console.WriteLine("Chat AI-> Your full name is: " + name);
            Console.WriteLine("===================================================================================================");
            UserData.SaveUserDetails(name);
        }

        private void Menu()
        {
            while (true)
            {
                Console.WriteLine("\n===================================================================================================");
                Console.WriteLine("Chat AI-> Would you like to ask any questions? (yes/no)");
                Console.WriteLine("===================================================================================================");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(name + "-> ");
                string answer = Console.ReadLine()?.ToLower();
                Console.ForegroundColor = ConsoleColor.Cyan;

                switch (answer)
                {
                    case "yes":
                        questionHandler.HandleQuestions(name, UserData);
                        break;
                    case "no":
                        Console.WriteLine("\n===================================================================================================");
                        Console.WriteLine("That's a shame. Feel free to come back if you have questions.");
                        Console.WriteLine("===================================================================================================");
                        return;
                    default:
                        Console.WriteLine("\n===================================================================================================");
                        Console.WriteLine("Invalid input! Please enter 'yes' or 'no'.");
                        Console.WriteLine("===================================================================================================");
                        break;
                }
            }
        }
    }
}