using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;
using System.Drawing;
using System.Collections;

namespace CyberSecurityBot
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the chatbot and start execution
            ChatBot chatbot = new ChatBot();
            chatbot.Run();
        }
    }
}
