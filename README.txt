Cybersecurity Awareness Chatbot
Welcome to the Cybersecurity Awareness Chatbot, a console-based application designed to educate users on cybersecurity best practices. This chatbot provides interactive responses to common cybersecurity topics, displays an ASCII art logo, plays a welcome audio, and remembers user-specific information across sessions.

Developed by: [Your Name or Team Name]
Last Updated: May 14, 2025
Version: 1.0

Overview
The Cybersecurity Awareness Chatbot is built to assist users in learning about topics such as passwords, phishing, malware, firewalls, VPNs, and more. It features:

A welcoming ASCII art display and audio greeting.
Interactive question-and-answer functionality with follow-up support (e.g., "tell me more").
Memory capabilities to store and recall user facts (e.g., "remember I use a VPN").
Sentiment analysis to personalize responses based on user emotions.
Persistent storage of user names, questions, and facts in text files.

Prerequisites
To run this project, ensure you have the following installed:

.NET SDK: Version 6.0 or later (recommended).
Visual Studio (optional, for development) or a compatible IDE.
NuGet Package Manager (included with Visual Studio or via CLI).

Required NuGet Package

System.Windows.Extensions (for audio playback using SoundPlayer).

Installation

Clone the Repository:
git clone https://github.com/yourusername/cybersecurity-chatbot.git
cd cybersecurity-chatbot


Install Dependencies:

Open the project in Visual Studio or use the .NET CLI to restore packages:dotnet restore


Add the System.Windows.Extensions package if not already included:dotnet add package System.Windows.Extensions




Add Audio File:

Place the HumeAI_voice-preview_Voice.wav file in the project root directory.
In Visual Studio, right-click the file in Solution Explorer, select "Properties", and set "Copy to Output Directory" to "Copy if newer".


Build the Project:
dotnet build



Usage

Run the Application:
dotnet run


Interact with the Chatbot:

The chatbot will display a loading animation, ASCII art, and play a welcome audio.
Enter your full name when prompted.
Choose "yes" to ask questions or "no" to exit.
Use commands like:
Ask about topics (e.g., "passwords", "phishing").
Follow up with "tell me more".
Store facts with "remember I use a VPN".
Recall facts with "what do you remember".
Exit with "exit".




Example Interaction:
Starting up chatbot AI...

[ASCII Art Displayed]

Welcome to the Cybersecurity Awareness Chatbot!

[HumeAI_voice-preview_Voice.wav plays]

CHAT AI-> Hello! Welcome to the Cybersecurity Awareness Bot. I'm here to help you stay safe online.
===================================================================================================
CHAT AI-> Please enter your full name:
User-> njs

===================================================================================================
CHAT AI-> Your full name is: njs
===================================================================================================

===================================================================================================
CHAT AI-> Would you like to ask any questions? (yes/no)
===================================================================================================
njs-> yes

Chat AI-> Enter your question (or 'exit' to go back to the main menu):
njs -> passwords
Chat AI-> Always use strong passwords with a mix of letters, numbers, and symbols.



Project Structure

ChatBot.cs: Main chatbot class handling startup and user interaction.
QuestionHandler.cs: Manages question processing and responses.
AudioImageHandler.cs: Handles ASCII art display and audio playback.
ChatMemory.cs: Stores and recalls user facts.
Conversation.cs: Tracks the current topic for follow-ups.
UserData.cs: Manages persistent storage of user data in files.
SentinentAnalyzer.cs: Analyzes user sentiment for personalized responses.
Program.cs: Entry point for the application.

Files Generated

users.txt: Stores user names.
user_questions.txt: Logs user questions.
user_facts.txt: Stores user facts.

Contributing
Feel free to fork this repository and submit pull requests. Suggestions for new cybersecurity topics or features are welcome!
License
This project is licensed under the MIT License - see the LICENSE file for details.
Contact
For questions or support, please open an issue on the GitHub repository or contact [your-email@example.com].
